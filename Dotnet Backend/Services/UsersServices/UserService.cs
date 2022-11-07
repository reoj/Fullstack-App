using DotnetBackend.Models;
using DotnetBackend.Services;
using exam_webapi.DTOs.UserDTOs;
using exam_webapi.Models;
using exam_webapi.Repositories;
using Microsoft.EntityFrameworkCore;
using users_items_backend.Context;

namespace exam_webapi.Services.UserServices
{
    public class UserService :IUserService
    {
        public DataContext _repo { get; set; }

        #region Constructor
        public UserService(DataContext context)
        {
            _repo = context;
        } 
        #endregion

        public async Task<ServiceResponse<User>> CreateUser(CreateUserDTO nwUser)
        {
            async Task<User> rawCreation(object obj)
            {
                var current = (CreateUserDTO)obj;
                User nw = new()
                {
                    Name = current.Name,
                    Phone = current.Phone,
                    Email = current.Email,
                    UserType = (UserType_Enum)current.UserType
                };
                var added = _repo.Users.AddAsync(nw);
                _ = await _repo.SaveChangesAsync();
                return ServiceHelper<User>.NoNullsAccepted(await _repo.Users.FirstOrDefaultAsync(u => u.Phone == nw.Phone));
            }
            return await ServiceHelper<User>.ActionHandler(rawCreation, nwUser); 
        }

        public async Task<ServiceResponse<User>> GetUser(int id)
        {
            async Task<User> rawGetter(object obj)
            {
                var idn = (int)obj;
                var requested =await _repo.Users.FirstOrDefaultAsync(u => u.Id == idn);
                return ServiceHelper<User>.NoNullsAccepted(requested);
            }
            return await ServiceHelper<User>.ActionHandler(rawGetter, id);
        }
        
        public async Task<ServiceResponse<List<User>>> GetAllUsers()
        {
            return new ServiceResponse<List<User>>()
            {
                Body = await _repo.Users.ToListAsync(),
            };
        }
        
        public async Task<ServiceResponse<User>> UpdateUser(UpdateUserDTO nwUser)
        {
            async Task<User> rawUpdate(object obj)
            {
                var ci = (UpdateUserDTO)obj;
                var oldUser = await _repo.Users.FirstOrDefaultAsync(u => u.Id == ci.UserId);
                oldUser = ServiceHelper<User>.NoNullsAccepted(oldUser);

                oldUser.UserType = nwUser.UserType;
                oldUser.Email = nwUser.Email;
                oldUser.Name = nwUser.Name;
                oldUser.Phone = nwUser.Phone;

                await _repo.SaveChangesAsync();
                return oldUser;
            }
            return await ServiceHelper<User>.ActionHandler(rawUpdate, nwUser);
        }


        public async Task<ServiceResponse<User>> DeleteUser(int id)
        {
            async Task<User> rawDeletion(object obj)
            {
                var idn = (int)obj;
                var oldUser = await _repo.Users.FirstOrDefaultAsync(u => u.Id == idn);
                oldUser = ServiceHelper<User>.NoNullsAccepted(oldUser);
                _repo.Remove(oldUser);
                await _repo.SaveChangesAsync();
                return oldUser;
            }

            return await ServiceHelper<User>.ActionHandler(rawDeletion, id);
        }
    }
}