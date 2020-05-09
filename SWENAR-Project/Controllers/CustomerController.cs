using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SWENAR.Data;
using SWENAR.Models;
using SWENAR.Repository;
using SWENAR.Validation;
using SWENAR.ViewModels;

namespace SWENAR.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _repo;
        public CustomerController(ICustomerRepository repo)
        {
            this._repo = repo;
        }

        /// <summary>
        /// Method to get all customers
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> Get()
            => (await _repo.GetAsync()).Match(() => new List<Customer>(), customers => customers);

        /// <summary>
        /// Method to get a customer 
        /// </summary>
        /// <param name="id">Customer Id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> Get(int id)
            => (await _repo.GetAsync(id)).Match<ActionResult<Customer>>(() => NotFound(), (customer) => customer);

        /// <summary>
        /// Method to create a customer in database
        /// </summary>
        /// <param name="vm">Customer create view model</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateModel]
        public async Task<ActionResult<Customer>> Create(CustomerCreateVm vm)
            => (await _repo.CreateAsync(vm)).Match<ActionResult<Customer>>(
                () => BadRequest(),
                (customer) => CreatedAtAction(nameof(Get), new { id = customer.Id }, customer));

        /// <summary>
        /// Method to update an existing customer
        /// </summary>
        /// <param name="id">Customer Id</param>
        /// <param name="vm">Customer update view model</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ValidateModel]
        public async Task<IActionResult> Update(int id, CustomerEditVm vm)
            => (await _repo.UpdateAsync(vm)).Match(() => NotFound(),
                (isUpdated) => isUpdated ? NoContent() : StatusCode(500));

        /// <summary>
        /// Method to delete an existing customer 
        /// </summary>
        /// <param name="id">Customer Id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
            => (await _repo.DeleteAsync(id)).Match(() => NotFound(),
                (isDeleted) => isDeleted ? Ok() : StatusCode(500));
    }
}