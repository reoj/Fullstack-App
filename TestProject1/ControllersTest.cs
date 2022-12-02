using DotnetBackend.Models;
using DotnetBackend.Services.UserServices;
using Microsoft.EntityFrameworkCore;
using Moq;

using users_items_backend.Context;

namespace TestProject1
{
    public class ControllersTest
    {
        private UserService _userService;

        [Fact]
        public async Task GetUsers_Returns_ServiceResponseAsync()
        {
            // Arrange
            var num = 1;

            var it = new List<InventoryItem>() { };

            var usenN = new User()
            {
                Id = 1,
                Name = "Test",
                Email = "email",
                Items = it,
                UserType = UserType_Enum.Soldier,
            };

            var fake_table = new List<User>() { usenN };
            var options = new DbContextOptionsBuilder<DataContext>()
                      .UseSqlite("Data Source=:memory:")
                      .Options;

            var context_fake = new Mock<DataContext>(options);


            //context_fake.SetupGet(m => m.Users).Returns(DbContextMock.GetQueryableMockDbSet(fake_table));
            context_fake.As<IDataContext>().Setup(m => m.Users).Returns(DbContextMock.GetQueryableMockDbSet(fake_table));

            context_fake.Setup(u => u.SaveChanges()).Returns(1);

            var fake_serv = new Mock<UserService>(context_fake.Object);

            _userService = fake_serv.Object;


            // Act
            var response = await _userService.GetAllUsers();


            // Assert
            var result_succ = response.Successfull;
            
            Assert.Equivalent(true, result_succ);
        }
       
        public static class DbContextMock
        {
            public static DbSet<T> GetQueryableMockDbSet<T>(List<T> sourceList) where T : class
            {
                var queryable = sourceList.AsQueryable();
                
                var dbSet = new Mock<DbSet<T>>();
                dbSet.As<IQueryable<T>>().Setup(m => m.Provider)
                    .Returns(queryable.Provider);
                dbSet.As<IQueryable<T>>().Setup(m => m.Expression)
                    .Returns(queryable.Expression);
                dbSet.As<IQueryable<T>>().Setup(m => m.ElementType)
                    .Returns(queryable.ElementType);
                dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator())
                    .Returns(() => queryable.GetEnumerator());
                
                
                dbSet.Setup(d => d.Add(It.IsAny<T>()))
                    .Callback<T>((s) => sourceList.Add(s));

                return dbSet.Object;
            }
        }
    }
    
}