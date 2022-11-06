using DotnetBackend.Models;
using DotnetBackend.Services;
using exam_webapi.DTOs.UserDTOs;
using exam_webapi.Models;
using exam_webapi.Repositories;

namespace exam_webapi.Services.UserServices
{
    public class UserService : IUserService
    {
        public List<User> _repo { get; set; }

        public UserService()
        {
            _repo = StaticData.UsersContext;
        }
        public ServiceResponse<User> CreateUser(CreateUserDTO nwUser)
        {
            Func<Object, User> rawCreation = (obj) =>
            {
                var current = (CreateUserDTO)obj;
                User nw = new User()
                {
                    Id = _repo.Count + 1,
                    Name = current.Name,
                    Phone = current.Phone,
                    Email = current.Email,
                    UserType = (UserType_Enum)current.UserType
                };
                _repo.Add(nw);
                return nw;
            };
            return ServiceHelper<User>.ActionHandler(rawCreation, nwUser);
        }

        public ServiceResponse<User> GetUser(int id)
        {
            Func<Object, User> rawGetter = (obj) =>
            {
                var idn = (int)obj;
                var requested = _repo.Where(u => u.Id == idn).SingleOrDefault();
                return ServiceHelper<User>.NoNullsAccepted(requested);
            };
            return ServiceHelper<User>.ActionHandler(rawGetter, id);
        }

        public ServiceResponse<User> UpdateUser(UserDTO nwUser)
        {
            Func<Object, User> rawUpdate = (obj) =>
            {
                var ci = (UserDTO)obj;
                var oldUser = _repo.Find(u => u.Id == ci.UserId);
                oldUser = ServiceHelper<User>.NoNullsAccepted(oldUser);
                int index = _repo.IndexOf(oldUser);
                User nw = new User()
                {
                    Id = _repo.Count + 1,
                    Name = nwUser.Name,
                    Phone = nwUser.Phone,
                    Email = nwUser.Email,
                    UserType = (UserType_Enum)nwUser.UserType
                };
                _repo[index] = nw;
                return _repo[index];
            };
            return ServiceHelper<User>.ActionHandler(rawUpdate, nwUser);
        }


        ServiceResponse<User> IUserService.DeleteUser(int id)
        {
            Func<Object, User> rawDeletion = (obj) =>
            {
                var idn = (int)obj;
                var oldUser = _repo.Find(u => u.Id == idn);
                oldUser = ServiceHelper<User>.NoNullsAccepted(oldUser);
                _repo.Remove(oldUser);
                return oldUser;
            };

            return ServiceHelper<User>.ActionHandler(rawDeletion, id);
        }
    }
}