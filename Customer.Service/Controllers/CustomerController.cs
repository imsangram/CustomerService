using Customer.BL;
using Customer.Dto;
using Customer.EFRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CustomerService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly CustomerController _customerController;
        public CustomersController(ICustomerRepository repository)
        {
            _customerController = new CustomerController(repository);
        }

        /// <summary>
        /// Returns list of all customers
        /// </summary>
        /// <param name="query">Optional field. Returns data by a Search Query.</param>
        /// <returns></returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(404, Type = typeof(string))]
        [HttpGet]
        public ActionResult<List<CustomerCreateDto>> Search(string query)
        {
            var data = _customerController.Search(query);
            if (data?.Count > 0)
                return Ok(data);
            else
                return NotFound("No customers found for the search criteria");
        }


        /// <summary>
        /// Returns a customer by Id
        /// </summary>
        /// <param name="id">customer Id</param>
        /// <returns></returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(404, Type = typeof(string))]
        [HttpGet("{id}")]
        public ActionResult<CustomerDto> Get(int id)
        {
            var customer = _customerController.Get(id);
            if (customer != null)
                return Ok(customer);
            else
                return NotFound("Customer could not be found");
        }

        /// <summary>
        /// Create a new Customer
        /// </summary>
        /// <param name="customer">Customer details</param>
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [HttpPost]
        public ActionResult Add([FromBody] CustomerCreateDto customer)
        {
           var id = _customerController.Add(customer);
            if (id > 0)
            {
                return Created(string.Format($"/api/Customers/{id}", id), "Customer has been created");
            }
            else
                return BadRequest("There is problem with request");
        }

        /// <summary>
        /// Update the customer by providing Id and detail object
        /// </summary>
        /// <param name="id">Customer Id which needs to be updated</param>
        /// <param name="customer">Customer details</param>
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] CustomerDto customer)
        {
            if (_customerController.Update(customer))
                return StatusCode(StatusCodes.Status204NoContent);
            else
                return StatusCode(StatusCodes.Status500InternalServerError, "Update failed");
        }

        /// <summary>
        /// Remove the customer by customer id
        /// </summary>
        /// <param name="id">Customer Id</param>
        /// <returns></returns>
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [HttpDelete("{id}")]
        public ActionResult Remove(int id)
        {
            var response = _customerController.Delete(id);
            if (response)
                return StatusCode(StatusCodes.Status204NoContent);
            else
                return NotFound("The Customer resource doesn't exist");
            
        }
    }
}
