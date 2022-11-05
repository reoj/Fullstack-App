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
        public User CreateUser(CreateUserDTO nwUser)
        {
            User nw = new User(){
                Id = _repo.Count + 1,
                Name = nwUser.Name,
                Phone = nwUser.Phone,
                Email = nwUser.Email,
                UserType = (UserType_Enum) nwUser.UserType
            };
            _repo.Add(nw);
            return nw;
        }

        public User GetUser(int id)
        {
            return _repo.Where(u => u.Id == id).SingleOrDefault();
            
        }

        public User UpdateUser(UserDTO nwUser)
        {
            User oldUser = _repo.Find(u => u.Id == nwUser.UserId);
            if (oldUser != null)
            {
                int index = _repo.IndexOf(oldUser);
                User nw = new User(){
                    Id = _repo.Count + 1,
                    Name = nwUser.Name,
                    Phone = nwUser.Phone,
                    Email = nwUser.Email,
                    UserType = (UserType_Enum) nwUser.UserType
                };
                _repo[index] = nw;
                return _repo[index];
            }else{
                return new User();
            }
        }


        User IUserService.DeleteUser(int id)
        {
            User oldUser = _repo.Find(u => u.Id == id);

            _repo.Remove(oldUser);
            return oldUser; 
        }
    }
}