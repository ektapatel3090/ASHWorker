using AshWorker.Controllers;
using AshWorker.Models;
using AshWorker.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AshWorker.test.Controller
{
   public  class WorkerControllerTest
    {
        private readonly Mock<IWorkerRepository> _mockWorkerRepository;
        private readonly WorkerController controller;

        public WorkerControllerTest()
        {
            _mockWorkerRepository = new Mock<IWorkerRepository>();
            controller = new WorkerController(_mockWorkerRepository.Object);
            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
        }

        [Fact]
        public async Task GetGetWorkers_ShouldReturnOK_WhenProvideProperRequestAsync()
        {
            _mockWorkerRepository.Setup(p => p.GetWorkers()).ReturnsAsync(new List<Worker> { new Worker() });
            var result = await controller.GetWorkers();
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            Assert.Equal(StatusCodes.Status200OK, okObjectResult.StatusCode);
            Assert.NotNull(okObjectResult.Value);
        }

        [Fact]
        public async Task GetGetWorkers_ShouldReturnNotFound_WhenNoAccountsFound()
        {
            _mockWorkerRepository.Setup(p => p.GetWorkers()).ReturnsAsync(new List<Worker> { });

            var result = await controller.GetWorkers();

            Assert.NotNull(result);
            Assert.IsType<ObjectResult>(result);
            var objectResult = result as ObjectResult;
            Assert.NotNull(objectResult);
            Assert.Equal(StatusCodes.Status404NotFound, objectResult.StatusCode);
            Assert.NotNull(objectResult.Value);
            Assert.IsType<ErrorResponse>(objectResult.Value);
            var errorResponse = objectResult.Value as ErrorResponse;
            Assert.NotNull(errorResponse);
        }


        [Fact]
        public async Task CreateWorker_ShouldReturnOK_WhenProvideProperRequestAsync()
        {
            var worker = new Worker //valid input
            {
                FirstName = "James",
                LastName = "Doe",
                Address1 = "123 abc Dr",
                workerType = new WorkerType() { Id = 1, WorkerTypeDesc = "Manager" },
                salaryType = new SalaryType() { Id = 1, SalaryTypeDesc = "Salaried" },
                Salary = 30000,
                CanHaveExpenses = true,
                MaxExpenseAmount = 1000
            };

            var result = await controller.CreateWorker(worker);
            Assert.NotNull(result);
            Assert.IsType<CreatedResult>(result);
            var createdResult = result as CreatedResult;
            Assert.NotNull(createdResult);
            Assert.Equal(StatusCodes.Status201Created, createdResult.StatusCode);
        }

        [Fact]
        public async Task CreateWorker_ShouldReturnBadRequest_WhenProvideProperRequestAsync()
        {
            var worker = new Worker // give invalid input here CanHaveExpenses is false and still it has MaxexpenseAmount >0 so, it becomes invalid input.
            {
                FirstName = "James",
                LastName = "Doe",
                Address1 = "123 abc Dr",
                workerType = new WorkerType() { Id = 1, WorkerTypeDesc = "Manager" },
                salaryType = new SalaryType() { Id = 1, SalaryTypeDesc = "Salaried" },
                Salary = 30000,
                CanHaveExpenses = false,
                MaxExpenseAmount = 1000

            };
            var errorMessage = string.Format("One or more input parameters are invalid.");
            var result = await controller.CreateWorker(worker);
            Assert.IsType<ObjectResult>(result);
            var objectResult = result as ObjectResult;
            Assert.NotNull(objectResult);
            Assert.NotNull(objectResult.Value);
            Assert.Equal(objectResult.StatusCode, StatusCodes.Status400BadRequest);
            ErrorResponse errorResult = (ErrorResponse)objectResult.Value;
            Assert.Contains(errorMessage, errorResult.Message);
        }
    }
}
