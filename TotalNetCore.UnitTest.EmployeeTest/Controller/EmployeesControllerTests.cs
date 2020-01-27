using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using TotalNetCore.UnitTest.EmployeeApp.Contracts;
using TotalNetCore.UnitTest.EmployeeApp.Controllers;
using TotalNetCore.UnitTest.EmployeeApp.Models;
using Xunit;

namespace TotalNetCore.UnitTest.EmployeeTest.Controller
{
    public class EmployeesControllerTests
    {
        private readonly Mock<IEmployeeRepository> _mockRepo;
        private readonly EmployeesController _controller;

        public EmployeesControllerTests()
        {
            _mockRepo = new Mock<IEmployeeRepository>();
            _controller = new EmployeesController(_mockRepo.Object);
        }

        /// <summary>
        /// 测试方法的返回类型
        /// </summary>
        [Fact]
        public void Index_ActionExecutes_ReturnsViewForIndex()
        {
            var result = _controller.Index();
            Assert.IsType<ViewResult>(result);
        }

        /// <summary>
        /// 测试方法返回集合的数量
        /// </summary>
        [Fact]
        public void Index_ActionExecutes_ReturnsExactNumberOfEmployees()
        {
            //Arrange
            _mockRepo.Setup(repo => repo.GetAll())
                .Returns(new List<Employee>() { new Employee(), new Employee() });
            //Act
            var result = _controller.Index();

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var employees = Assert.IsType<List<Employee>>(viewResult.Model);
            Assert.Equal(2, employees.Count);
        }

        /// <summary>
        /// 测试返回类型
        /// </summary>
        [Fact]
        public void Create_ActionExecutes_ReturnsViewForCreate()
        {
            var result = _controller.Create();
            Assert.IsType<ViewResult>(result);
        }

        /// <summary>
        /// 测试内在逻辑没有问题的情况
        /// </summary>
        [Fact]
        public void Create_InvalidModelState_ReturnsView()
        {
            //Arrange
            _controller.ModelState.AddModelError("Name", "Name is required");
            var employee = new Employee { Age = 25, AccountNumber = "255-8547963214-41" };

            //Act
            var result = _controller.Create(employee);

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var testEmployee = Assert.IsType<Employee>(viewResult.Model);
            Assert.Equal(employee.AccountNumber, testEmployee.AccountNumber);
            Assert.Equal(employee.Age, testEmployee.Age);
        }

        /// <summary>
        /// 测试内在逻辑有问题的情况
        /// </summary>
        [Fact]
        public void Create_InvalidModelState_CreateEmployeeNeverExecutes()
        {
            _controller.ModelState.AddModelError("Name","Name is required");
            var employee = new Employee { Age = 34 };

            _controller.Create(employee);

            _mockRepo.Verify(x=>x.CreateEmployee(It.IsAny<Employee>()), Times.Never);
        }

        /// <summary>
        /// 测试内在逻辑走通，执行一次
        /// </summary>
        [Fact]
        public void Create_ModelStateValid_CreateEmployeeCalledOnce()
        {
            Employee emp = null;
            _mockRepo.Setup(r => r.CreateEmployee(It.IsAny<Employee>()))
                .Callback<Employee>(x => emp = x);

            var employee = new Employee
            {
                Name = "Test Employee",
                Age = 32,
                AccountNumber = "123-5435789603-21"
            };

            _controller.Create(employee);

            _mockRepo.Verify(x => x.CreateEmployee(It.IsAny<Employee>()), Times.Once);

            Assert.Equal(emp.Name, employee.Name);
            Assert.Equal(emp.Age, employee.Age);
            Assert.Equal(emp.AccountNumber, employee.AccountNumber);
        }

        [Fact]
        public void Create_ActionExecuted_RedirectsToIndexAction()
        {
            var employee = new Employee
            {
                Name = "Test Employee",
                Age = 45,
                AccountNumber = "123-4356874310-43"
            };

            var result = _controller.Create(employee);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }
    }
}
