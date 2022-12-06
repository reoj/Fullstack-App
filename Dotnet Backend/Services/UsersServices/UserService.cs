using DotnetBackend.DTOs;
using DotnetBackend.Models;
using Microsoft.EntityFrameworkCore;
using users_items_backend.Context;

namespace DotnetBackend.Services.UserServices
{
    public class UserService : IUserService
    {
        public DataContext DataRepository { get; set; }

        #region Constructor
        /// <summary>
        /// Constructor for User service
        /// </summary>
        /// <param name="context">The Data Context that contain a DbSet with a Table called Users</param>
        public UserService(DataContext context)
        {
            DataRepository = context;
        }
        #endregion

        #region Public Async Methods
        /// <summary>
        /// Creates a user
        /// </summary>
        /// <param name="newUserInformation"></param>
        /// <returns></returns>
        public async Task<ServiceResponse<User>> CreateUser(CreateUserDTO newUserInformation)
        {
            return await ServiceHelper<User>.HandleAnActionInsideAServiceResponse(CreateUserFromDto, newUserInformation);
        }

        /// <summary>
        /// Reads a User
        /// </summary>
        /// <param name="id">The Unique userID for the User</param>
        /// <returns></returns>
        public async Task<ServiceResponse<GetUserDTO>> GetUser(int id)
        {

            return await ServiceHelper<GetUserDTO>.HandleAnActionInsideAServiceResponse(ReadyUserForExport, id);
        }

        /// <summary>
        /// Reads A list with All Users
        /// </summary>
        /// <returns>An async ServiceResponde containing A list with all Users in repository</returns>
        public async Task<ServiceResponse<List<GetUserDTO>>> GetAllUsers()
        {

            return await ServiceHelper<List<GetUserDTO>>.HandleAnActionInsideAServiceResponse(() => GenerateReadableListOfUsers());
        }

        /// <summary>
        /// Changes the information of a User
        /// </summary>
        /// <param name="user">An object indicating the old UserID and the new information of the User</param>
        /// <returns>An async ServiceResponse with the updated User information</returns>
        public async Task<ServiceResponse<GetUserDTO>> UpdateUser(UpdateUserDTO user)
        {
            return await ServiceHelper<GetUserDTO>.HandleAnActionInsideAServiceResponse(UpdateUserRaw, user);
        }

        /// <summary>
        /// Deletes a User with a given ID
        /// </summary>
        /// <param name="id">The unique identification number of the user to be deleted</param>
        /// <returns>An async ServiceResponse with the data of the user that got deleted</returns>
        public async Task<ServiceResponse<GetUserDTO>> DeleteUser(int id)
        {
            return await ServiceHelper<GetUserDTO>.HandleAnActionInsideAServiceResponse(DeleteUserRaw, id);
        }

        #endregion

        #region Private Methods
        private async Task<List<GetUserDTO>> GenerateReadableListOfUsers()
        {
            List<User> listOfUsers = await GetListOfAllUsersFromDataRepository();
            List<GetUserDTO> listofUserDTOs = GenerateListofUserDTOFromUsers(listOfUsers);
            return listofUserDTOs;
        }

        private async Task<GetUserDTO> DeleteUserRaw(object obj)
        {
            // Retrieve
            var identifier = (int)obj;
            var oldUser = await GetUserFromDataRepo(identifier);

            // Act
            DataRepository.Remove(oldUser);

            // Save
            await DataRepository.SaveChangesAsync();

            //Return
            return new GetUserDTO(oldUser);
        }
        private async Task<GetUserDTO> UpdateUserRaw(object obj)
        {
            // Retrieve
            var updatedUser = (UpdateUserDTO)obj;
            var oldUser = await GetUserFromDataRepo(updatedUser.UserId);

            // Act: Update fields one by one
            oldUser.UserType = updatedUser.UserType;
            oldUser.Email = updatedUser.Email;
            oldUser.Name = updatedUser.Name;
            oldUser.Phone = updatedUser.Phone;

            // Save
            await DataRepository.SaveChangesAsync();

            // Return
            return new GetUserDTO(oldUser);
        }
        private async Task<User> CreateUserFromDto(object obj)
        {
            // Retrieve
            var creationInformation = (CreateUserDTO)obj;

            // act: Generate the new User object 
            User inCreation = new()
            {
                Name = creationInformation.Name,
                Phone = creationInformation.Phone,
                Email = creationInformation.Email.ToLower(),
                UserType = (UserType_Enum)creationInformation.UserType
            };
            var added = await DataRepository.Users.AddAsync(inCreation);

            // Save in Repository 
            _ = await DataRepository.SaveChangesAsync();

            // Return
            return ServiceHelper<User>.NoNullsAccepted(added.Entity);
        }
        #endregion

        #region Helper Methods
        private async Task<GetUserDTO> ReadyUserForExport(object obj)
        {
            var userID = (int)obj;
            var requested = await GetUserFromDataRepo(userID);
            return new GetUserDTO(requested);
        }
        public async Task<User> GetUserFromDataRepo(int idetifier)
        {
            var requested = await DataRepository.Users
                .Include(fromUser => fromUser.Items)
                .FirstOrDefaultAsync(currentUser => currentUser.Id == idetifier);
            return ServiceHelper<User>.NoNullsAccepted(requested);
        }

        private static List<GetUserDTO> GenerateListofUserDTOFromUsers(List<User> listOfUsers)
        {
            var listofUserDTOs = new List<GetUserDTO>();
            listOfUsers.ForEach(currentUser => listofUserDTOs
                .Add(new GetUserDTO(currentUser)));
            return listofUserDTOs;
        }

        private async Task<List<User>> GetListOfAllUsersFromDataRepository()
        {
            return await DataRepository.Users
                               .Include(currentUser => currentUser.Items)
                               .ToListAsync();
        }
        #endregion
    
    }
}