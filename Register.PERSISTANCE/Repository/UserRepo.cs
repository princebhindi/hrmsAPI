using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Register.APPLICATION;
using Register.APPLICATION.DTO;
using Register.APPLICATION.Interface;
using Register.DOMAIN.Entities;
using Register.PERSISTANCE.Context;

namespace Register.PERSISTANCE.Repository
{
    public class UserRepo : IUserRepo
    {
        private readonly ApllicationDbContext context;
        private readonly IMapper mapper;

        public UserRepo(ApllicationDbContext _context, IMapper _mapper)
        {
            context = _context;
            mapper = _mapper;
        }

        public async Task<UserRegister> LoginUser(Register.DOMAIN.Entities.UserLogin user)
        {
            var users = context.Users.FirstOrDefault(x => x.UserName == user.UserName);
            if (users == null) return null;

            var passwordHasher = new PasswordHasher<object>();
            var result = passwordHasher.VerifyHashedPassword(
                null,
                users.Password,
                user.Password
            );

            if (result == PasswordVerificationResult.Success)
            {
                return users;
            }
            return null;
        }

        public async Task<bool> RegisterUser(UserRegisterDto user)
        {
            var entitydata = mapper.Map<UserRegister>(user);
            await context.Users.AddAsync(entitydata);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<UserRegister>> GetAllUsersAsync()
        {
            return await context.Users.ToListAsync();
        }

        public async Task<UserRegister?> GetUserByIdAsync(Guid id)
        {
            return await context.Users.FindAsync(id);
        }

        public async Task<bool> UpdateUserAsync(UserRegister user)
        {
            var existingUser = await context.Users.FindAsync(user.Id);
            if (existingUser == null) return false;

            existingUser.UserName = user.UserName;
            existingUser.Name = user.Name;
            existingUser.LastName = user.LastName;
            existingUser.Role = user.Role;
            existingUser.IsActive = user.IsActive;
            existingUser.OnUpdate = DateTime.Now;

            // If a new password is provided and it is different from the old hashed password, hash and save it.
            if (!string.IsNullOrEmpty(user.Password) && existingUser.Password != user.Password)
            {
                var passwordHasher = new PasswordHasher<object>();
                existingUser.Password = passwordHasher.HashPassword(null, user.Password);
            }

            context.Users.Update(existingUser);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            var user = await context.Users.FindAsync(id);
            if (user == null) return false;

            context.Users.Remove(user);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
