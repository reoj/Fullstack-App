using DotnetBackend.Models;
using DotnetBackend.Repositories;
using DotnetBackend.Services.UserServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using Moq;
using System.Linq.Expressions;
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
                Name= "Test",
                Email = ""
            };
            var context_fake = GetDataContextMok();
            context_fake.Setup(
                context => context.Users
                .Include(u => u.Items).FirstAsync())
                .Returns(Task.FromResult(usenN));
            var service = new UserService(context_fake.Object);


            // Act
            var response = await service.GetUser(num);


            // Assert
            var result_succ = response.Successfull;
            var result_Users = response.Body as IEnumerable<User>;
            Assert.Equivalent(true, result_succ);
        }
        public Mock<DataContext> GetDataContextMok()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                      .UseSqlite("Data Source=:memory:")
                      .Options;

            var dataContextMock = new Mock<DataContext>(options);
            return dataContextMock;
        }
    }
    
}