using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Register.APPLICATION.Command;
using Register.APPLICATION.DTO;
using Register.APPLICATION.Queries;

namespace Register.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeDocumentsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IWebHostEnvironment _env;

        public EmployeeDocumentsController(IMediator mediator, IWebHostEnvironment env)
        {
            _mediator = mediator;
            _env = env;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllEmployeeDocuments([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var res = await _mediator.Send(new GetAllEmployeeDocumentsQuery(pageNumber, pageSize));
            return Ok(res);
        }

        [HttpGet("count")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetEmployeeDocumentCount()
        {
            var res = await _mediator.Send(new GetEmployeeDocumentCountQuery());
            return Ok(res);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeDocumentById(Guid id)
        {
            var res = await _mediator.Send(new GetEmployeeDocumentByIdQuery(id));
            if (!res.Sucess) return NotFound(res);
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployeeDocument([FromForm] Guid empId, [FromForm] string documentType, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest(new { sucess = false, message = "File is required." });
            }

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".pdf" };
            var extension = Path.GetExtension(file.FileName).ToLower();

            if (!allowedExtensions.Contains(extension))
            {
                return BadRequest(new { sucess = false, message = "Only Image (JPG, JPEG, PNG) and PDF files are allowed." });
            }

            // Create uploads folder inside wwwroot
            var uploadsFolder = Path.Combine(_env.ContentRootPath, "wwwroot", "uploads", "documents");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var uniqueFileName = $"{Guid.NewGuid()}{extension}";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var requestScheme = Request.Scheme;
            var requestHost = Request.Host.Value;
            var documentDto = new EmployeeDocumentDto
            {
                EmpId = empId,
                DocumentType = documentType,
                DocumentName = file.FileName,
                DocumentPath = $"{requestScheme}://{requestHost}/uploads/documents/{uniqueFileName}"
            };

            var res = await _mediator.Send(new AddEmployeeDocumentCommand(documentDto));
            return Ok(res);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEmployeeDocument([FromForm] Guid id, [FromForm] Guid empId, [FromForm] string documentType, IFormFile? file, [FromForm] string? existingPath)
        {
            string? documentPath = existingPath;
            string? documentName = null;

            if (file != null && file.Length > 0)
            {
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".pdf" };
                var extension = Path.GetExtension(file.FileName).ToLower();

                if (!allowedExtensions.Contains(extension))
                {
                    return BadRequest(new { sucess = false, message = "Only Image (JPG, JPEG, PNG) and PDF files are allowed." });
                }

                var uploadsFolder = Path.Combine(_env.ContentRootPath, "wwwroot", "uploads", "documents");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var uniqueFileName = $"{Guid.NewGuid()}{extension}";
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                var requestScheme = Request.Scheme;
                var requestHost = Request.Host.Value;
                documentPath = $"{requestScheme}://{requestHost}/uploads/documents/{uniqueFileName}";
                documentName = file.FileName;
            }

            var documentDto = new EmployeeDocumentDto
            {
                Id = id,
                EmpId = empId,
                DocumentType = documentType,
                DocumentName = documentName ?? (string.IsNullOrEmpty(existingPath) ? "Document" : Path.GetFileName(existingPath)),
                DocumentPath = documentPath,
                OnUpdate = DateTime.Now
            };

            var res = await _mediator.Send(new UpdateEmployeeDocumentCommand(documentDto));
            if (!res.Sucess) return NotFound(res);
            return Ok(res);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteEmployeeDocument(Guid id)
        {
            var res = await _mediator.Send(new DeleteEmployeeDocumentCommand(id));
            if (!res.Sucess) return NotFound(res);
            return Ok(res);
        }
    }
}
