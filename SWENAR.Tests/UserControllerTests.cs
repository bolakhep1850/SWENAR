using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SWENAR.Controllers;
using SWENAR.Data;
using SWENAR.Models;
using SWENAR.Tests.Services;
using SWENAR.ViewModels;
using System;
using System.Threading.Tasks;

namespace SWENAR.Tests
{
    [TestFixture]
    public class UserControllerTests
    {
        private DbContextOptionsBuilder _options;
        private UserManager<User> _userManager;
        private UserController _controller;

        [SetUp]
        public void SetUp()
        {
            //mocking the db context
            _options = new DbContextOptionsBuilder();
            _options.UseNpgsql("Server=127.0.0.1;Port=5432;Database=SWENARDB;User Id=admin;Password=password;");

            //mocking usermanager
            _userManager = MockHelper.TestUserManager<User>();
            _controller = new UserController(new SWENARDBContext(_options.Options), _userManager);
        }

        [Test]
        public async Task Create_Creates_User()
        {
            //Arrange
            var random = new Random();

            //Act
            var person = new Person()
            {
                FName = $"Pramod {random.Next()}",
                LName = $"Bolakhe {random.Next()}",
                Phone = $"832-660-348{random.Next(0, 9)}",
                Email = $"bolakhep{random.Next()}@hotmail.com"
            };

            using (var context = new SWENARDBContext(_options.Options))
            {
                context.People.Add(person);
                await context.SaveChangesAsync();

                person = await context.People.FindAsync(person.Id);
                var result = await _controller.Create(new UserCreateVm()
                {
                    PersonId = person.Id,
                    UserName = $"{DateTime.Now.Date.ToString("MM/dd/yyyy").Replace("/", "")}{DateTime.Now.Hour}{DateTime.Now.Minute}{DateTime.Now.Second}"
                });

                //Assert
                Assert.NotNull(result.Value);
            }
        }

    }
}
