using DotnetBackend.DTOs.UserDTOs;
using DotnetBackend.Models;
using exam_webapi.DTOs.UserDTOs;
using exam_webapi.Models;

namespace exam_webapi.Services.UserServices
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