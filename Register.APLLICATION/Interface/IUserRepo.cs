using System;
using System.Collections.Generic;
using System.Text;
using Register.APPLICATION.DTO;
using Register.DOMAIN.Entities;

namespace Register.APPLICATION.Interface
{
    public interface IUserRepo
    {
        public Task<bool> RegisterUser (UserRegisterDto user);

        public Task<UserRegister> LoginUser(Register.DOMAIN.Entities.UserLogin user);

        public Task<IEnumerable<UserRegister>> GetAllUsersAsync();
        public Task<UserRegister?> GetUserByIdAsync(Guid id);
        public Task<bool> UpdateUserAsync(UserRegister user);
        public Task<bool> DeleteUserAsync(Guid id);
    }
}
