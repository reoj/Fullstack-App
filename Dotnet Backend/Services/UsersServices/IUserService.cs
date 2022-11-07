using DotnetBackend.Models;
using exam_webapi.DTOs.UserDTOs;
using exam_webapi.Models;

namespace exam_webapi.Services.UserServices
{
    public interface IUserService
    {
        Task<ServiceResponse<User>> CreateUser(CreateUserDTO nwUser);
        Task<ServiceResponse<User>> GetUser(int id);
        Task<ServiceResponse<User>> UpdateUser(UpdateUserDTO nwUser);
        Task<ServiceResponse<User>> DeleteUser(int id);
    }
}