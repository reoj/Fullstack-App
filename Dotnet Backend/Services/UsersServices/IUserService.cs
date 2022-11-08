using DotnetBackend.DTOs;
using DotnetBackend.Models;

namespace DotnetBackend.Services.UserServices
{
    /// <summary>
    /// Contract for User Service
    /// </summary>
    public interface IUserService
    {
        Task<ServiceResponse<User>> CreateUser(CreateUserDTO nwUser);
        Task<ServiceResponse<GetUserDTO>> GetUser(int id);
        Task<ServiceResponse<List<GetUserDTO>>> GetAllUsers();
        Task<ServiceResponse<GetUserDTO>> UpdateUser(UpdateUserDTO nwUser);
        Task<ServiceResponse<GetUserDTO>> DeleteUser(int id);
    }
}