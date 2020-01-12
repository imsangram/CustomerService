using Xunit;
using Customer.EFRepository;
using Customer.EFRepository.Dal;
using Microsoft.EntityFrameworkCore;
using CustomerService.Controllers;
using System;
using Customer.BL;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Customer.Dto;

namespace Customer.Test
{
    public class CustomerTests
    {
        private readonly CustomersController custController;

        public CustomerTests()
        {
            // Setup InMemory Database for Unit testing

            #region Setup

            CustomerDBContext context = GetContext();
            SeedDatabase(context);
            CustomerRepository customerRepository = new CustomerRepository(context);
            CustomerController CustomerController = new CustomerController(customerRepository);
            custController = new CustomersController(customerRepository);

            #endregion

        }

        public CustomerDBContext GetContext()
        {
            var options = new DbContextOptionsBuilder<CustomerDBContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString())
                      .Options;
            return new CustomerDBContext(options);
        }

        /// <summary>
        /// Seed data In Memory database.
        /// </summary>
        /// <param name="customerRepository">The customer repository.</param>
        private void SeedDatabase(CustomerDBContext context)
        {
            context.Customers.Add(new Entity.Customer { LastName = "Doe", FirstName = "John", DateOfBirth = new System.DateTime(1988, 01, 01), Id = 1 });
            context.Customers.Add(new Entity.Customer { LastName = "Doe", FirstName = "Jane", DateOfBirth = new System.DateTime(1988, 01, 01), Id = 2 });
            context.Customers.Add(new Entity.Customer { LastName = "Payne", FirstName = "Jimmi", DateOfBirth = new System.DateTime(1988, 01, 01), Id = 3 });
            context.Customers.Add(new Entity.Customer { LastName = "Sarah", FirstName = "Bill", DateOfBirth = new System.DateTime(1988, 01, 01), Id = 4 });
            context.Customers.Add(new Entity.Customer { LastName = "John", FirstName = "Steve", DateOfBirth = new System.DateTime(1988, 01, 01), Id = 5 });
            context.Customers.Add(new Entity.Customer { LastName = "Irwin", FirstName = "Steve", DateOfBirth = new System.DateTime(1988, 01, 01), Id = 6 });
            context.SaveChanges();
        }

        [Fact]
        public void Test_Get_All_Customers_ReturnsOkResult()
        {
            var response = custController.Search(string.Empty);
            Assert.IsType<OkObjectResult>(response.Result);

        }

        [Fact]
        public void Test_Search_Customers_ByName_ReturnsOkResult()
        {
            //Search for customers having name 'John'
            var data = custController.Search("John").Result as OkObjectResult;
            var customers = Assert.IsType<List<CustomerDto>>(data.Value);
            Assert.Equal(2, customers.Count);

        }

        [Fact]
        public void Test_Search_Customers_ReturnsNotFound()
        {
            var notFoundResult = custController.Search("Jonathan");
            Assert.IsType<NotFoundObjectResult>(notFoundResult.Result);
        }

        [Fact]
        public void Test_Get_Customer_by_Id_ReturnsOkResult()
        {
            var response = custController.Get(1);
            Assert.IsType<OkObjectResult>(response.Result);

            var okResult = response.Result as OkObjectResult;
            var customer = Assert.IsType<CustomerDto>(okResult.Value);
            Assert.Equal("John", customer.FirstName);
        }


        [Fact]
        public void Test_Create_new_Customer_Returns_Success()
        {
            var response = custController.Add(new CustomerCreateDto { FirstName = "Mary", LastName = "Poppins", DateOfBirth = new DateTime(1999,10,10) });
            Assert.IsType<CreatedResult>(response);
        }

        
        [Fact]
        public void Test_Update_Customer_Returns_Success()
        {
            var response = custController.Update(1, new CustomerDto { Id = 1, FirstName = "Mary", LastName = "Poppins", DateOfBirth = new DateTime(1999, 10, 10) });
            Assert.IsType<StatusCodeResult>(response);
            var statusCode = response as StatusCodeResult;
            Assert.Equal(204, statusCode.StatusCode);

            var okResult = custController.Get(1).Result as OkObjectResult;
            var customer = Assert.IsType<CustomerDto>(okResult.Value);
            Assert.Equal("Mary", customer.FirstName);

        }

        [Fact]
        public void Test_Delete_Customer_Returns_Success()
        {
            var response = custController.Remove(1);
            Assert.IsType<StatusCodeResult>(response);
            var statusCode = response as StatusCodeResult;
            Assert.Equal(204, statusCode.StatusCode);
        }

        [Fact]
        public void Test_Delete_Customer_Returns_Failure()
        {
            var response = custController.Remove(100);
            Assert.IsType<NotFoundObjectResult>(response);

        }
    }
}
