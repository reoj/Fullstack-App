using DotnetBackend.DTOs;
using DotnetBackend.Models;
// using exam_webapi.Repositories;
using Microsoft.EntityFrameworkCore;
using users_items_backend.Context;

namespace DotnetBackend.Services.UserServices
{
    public class UserService : IUserService
    {
        public DataContext _repo { get; set; }

        #region Constructor
        public UserService(DataContext context)
        {
            _repo = context;
        }
        #endregion

        #region Public Async Methods
        public async Task<ServiceResponse<User>> CreateUser(CreateUserDTO nwUser)
        {
            async Task<User> rawCreation(object obj)
            {
                var current = (CreateUserDTO)obj;
                User nw = new()
                {
                    Name = current.Name,
                    Phone = current.Phone,
                    Email = current.Email.ToLower(),
                    UserType = (UserType_Enum)current.UserType
                };
                var added = await _repo.Users.AddAsync(nw);
                _ = await _repo.SaveChangesAsync();
                return ServiceHelper<User>.NoNullsAccepted(
                    await _repo.Users.FirstOrDefaultAsync(u => u.Phone == nw.Phone));
            }
            return await ServiceHelper<User>.ActionHandler(rawCreation, nwUser);
        }

        public async Task<ServiceResponse<GetUserDTO>> GetUser(int id)
        {
            async Task<GetUserDTO> rawGetter(object obj)
            {
                var idn = (int)obj;
                var requested = await GetUserRaw(idn);
                //var requested = await _repo.Users
                //    .Include(u => u.Items)
                //    .FirstOrDefaultAsync(u => u.Id == idn);
                //requested = ServiceHelper<User>.NoNullsAccepted(requested);
                return new GetUserDTO(requested);
            }
            return await ServiceHelper<GetUserDTO>.ActionHandler(rawGetter, id);
        }

        public async Task<ServiceResponse<List<GetUserDTO>>> GetAllUsers()
        {
            var rawList = await _repo.Users
                   .Include(u => u.Items)
                   .ToListAsync();

            var dtoList = new List<GetUserDTO>();
            rawList.ForEach(u => dtoList.Add(new GetUserDTO(u)));

            return new ServiceResponse<List<GetUserDTO>>()
            {
                Body = dtoList
            };
        }

        public async Task<ServiceResponse<GetUserDTO>> UpdateUser(UpdateUserDTO nwUser)
        {
            async Task<GetUserDTO> rawUpdate(object obj)
            {
                var ci = (UpdateUserDTO)obj;
                var oldUser = await _repo.Users
                    .Include(u => u.Items)
                    .FirstOrDefaultAsync(u => u.Id == ci.UserId);

                oldUser = ServiceHelper<User>.NoNullsAccepted(oldUser);

                oldUser.UserType = nwUser.UserType;
                oldUser.Email = nwUser.Email;
                oldUser.Name = nwUser.Name;
                oldUser.Phone = nwUser.Phone;

                await _repo.SaveChangesAsync();
                return new GetUserDTO(oldUser);
            }
            return await ServiceHelper<GetUserDTO>.ActionHandler(rawUpdate, nwUser);
        }

        public async Task<ServiceResponse<GetUserDTO>> DeleteUser(int id)
        {
            async Task<GetUserDTO> rawDeletion(object obj)
            {
                var idn = (int)obj;
                var oldUser = await _repo.Users
                    .Include(u => u.Items)
                    .FirstOrDefaultAsync(u => u.Id == idn);
                oldUser = ServiceHelper<User>.NoNullsAccepted(oldUser);
                _repo.Remove(oldUser);
                await _repo.SaveChangesAsync();
                return new GetUserDTO(oldUser);
            }

            return await ServiceHelper<GetUserDTO>.ActionHandler(rawDeletion, id);
        }

        #endregion

        #region Raw Methods
        public async Task<User> GetUserRaw(int idn)
        {
            var requested = await _repo.Users
                .Include(u => u.Items)
                .FirstOrDefaultAsync(u => u.Id == idn);
            return ServiceHelper<User>.NoNullsAccepted(requested);
        }
        #endregion
    }
}