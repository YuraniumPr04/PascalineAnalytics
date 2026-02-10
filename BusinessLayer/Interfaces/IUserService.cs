using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.DTOs;

namespace BusinessLayer.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetAllUsersAsync();
        Task<UserDTO> GetUserByIdAsync(string id);

        Task CreateUserAsync(UserDTO userDto);
        Task UpdateUserAsync(UserDTO userDto);
        Task DeleteUserAsync(string id);

        Task<IEnumerable<string>> GetAllRolesAsync();

        Task<UserDTO> AuthenticateAsync(string email, string password);
    }
}
