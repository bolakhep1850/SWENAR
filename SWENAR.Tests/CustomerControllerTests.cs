using Moq;
using NUnit.Framework;
using SWENAR.Controllers;
using SWENAR.Models;
using SWENAR.Repository;
using SWENAR.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWENAR.Tests
{
    [TestFixture]
    public class CustomerControllerTests
    {
        private CustomerController _controller;

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public async Task Get_Gets_All_Customers()
        {

            //Arrange
            var mock = new Mock<ICustomerRepository>();
            IEnumerable<Customer> customers = new List<Customer>()
            {
                new Customer(){},
                new Customer(){},
                new Customer(){},
            };

            mock.Setup(c => c.GetAsync()).Returns(Task.FromResult(customers));
            _controller = new CustomerController(mock.Object);

            //Act
            var actionResult = await _controller.Get();

            //Assert
            Assert.AreEqual(actionResult.Value.Count(), 3);
        }

        [Test]
        public async Task Get_With_A_Id_Gets_A_Customer()
        {
            //Arrange
            var mock = new Mock<ICustomerRepository>();
            _controller = new CustomerController(mock.Object);

            IEnumerable<Customer> customers = new List<Customer>()
            {
                new Customer(){Id=1, Name = "Exxon", Number = "4435345"},
                new Customer(){Id=2, Name = "Citi", Number = "345345"},
                new Customer(){Id=3, Name = "Chase", Number = "56456456"},
            };

            int id = 1;
            mock.Setup(c => c.GetAsync(id)).Returns(Task.FromResult(customers.FirstOrDefault(a => a.Id == id)));

            //Act
            var actionResult = await _controller.Get(id);

            //Asset
            Assert.AreEqual(actionResult.Value, customers.FirstOrDefault(a => a.Id == id));
        }

        [Test]
        public async Task Create_Creates_A_Customer()
        {
            //arrange
            var mock = new Mock<ICustomerRepository>();
            _controller = new CustomerController(mock.Object);

            var customerCreateVm = new CustomerCreateVm()
            {
                Name = "Chase",
                Number = "345435345"
            };

            mock.Setup(c => c.CreateAsync(customerCreateVm)).Returns(Task.FromResult(new Customer()
            {
                Id = 1,
                Name = customerCreateVm.Name,
                Number = customerCreateVm.Number
            }));

            //act
            var actionResult = await _controller.Create(customerCreateVm);


            //Assert
            Assert.NotNull(actionResult.Result);
        }

        [Test]
        public async Task Update_Updates_A_Customer()
        {
            //arrange
            var mock = new Mock<ICustomerRepository>();
            _controller = new CustomerController(mock.Object);
            var customerEditVm = new CustomerEditVm()
            {
                Id = 1,
                Name = "Boa",
                Number = "354435"
            };

            mock.Setup(c => c.UpdateAsync(customerEditVm)).Returns(Task.FromResult(true));

            //act
            var actionResult = await _controller.Update(1, customerEditVm);

            //Asset
            Assert.NotNull(actionResult);
        }

        [Test]
        public async Task Delete_Deletes_A_Customer()
        {

            //Arrange
            var mock = new Mock<ICustomerRepository>();
            _controller = new CustomerController(mock.Object);

            int id = 1;
            mock.Setup(c => c.DeleteAsync(id)).Returns(Task.FromResult(true));

            //Act
            var actionResult = await _controller.Delete(1);

            //Assert
            Assert.AreEqual(actionResult.Value, true);
        }
    }
}
