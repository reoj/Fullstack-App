using DotnetBackend.Controllers;
using DotnetBackend.DTOs;
using DotnetBackend.Models;
using DotnetBackend.Services.UserServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Expressions;
using Moq;
using users_items_backend.Context;

namespace TestProject1
{
    public class ControllersTest
    {

        [Fact]
        public async Task GetUsers_Returns_ServiceResponseAsync()
        {
            // Arrange
            var num = 1;
            var usenN = new User()
            {
                Id = num,
                Name = "Test",
                Email = "test@email.com"
            };
            var context = GetDataContext();
            context.Database.EnsureCreated();
            context.Users.Add(usenN);
            context.SaveChanges();
            var service = new UserService(context);

            // Act
            var response = await service.GetUser(num);

            // Assert
            var result_succ = response.Successfull;            
            Assert.Equivalent(true, result_succ);
        }
        [Fact]
        public async Task GetUserFromController()
        {
            //Arrange
            var userId = 1;
            ServiceResponse<GetUserDTO> response = new ServiceResponse<GetUserDTO>()
            {
                Message = "Succesfull",
                Successfull = true,
                Body = new GetUserDTO(new User()),
            };

            var fakeService = GetMokOfUserService();
            fakeService.Setup(us => us.GetUser(userId)).Returns(Task.FromResult(response));
            var controller = new UsersController(fakeService.Object);

            //Act
            var controllerResult = await controller.GetUser(userId);
            Assert.NotNull(controllerResult);
        }

        public Mock<IUserService> GetMokOfUserService()
        {
            return new Mock<IUserService>();
        }
        public DataContext GetDataContext()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString())
                      .Options;

            var dataContext = new DataContext(options);
            dataContext.Database.EnsureCreated();
            return dataContext;
        }
    }

}