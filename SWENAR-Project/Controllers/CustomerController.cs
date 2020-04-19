using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SWENAR.Data;
using SWENAR.Models;
using SWENAR.Validation;
using SWENAR.ViewModels;

namespace SWENAR.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly SWENARDBContext _db;
        public CustomerController(SWENARDBContext db)
        {
            this._db = db;
        }

        /// <summary>
        /// Method to get all customers
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> Get()
        {
            var customers = await _db.Customers
                .OrderBy(a => -a.Id).ToListAsync();
            return customers;
        }

        /// <summary>
        /// Method to get a customer 
        /// </summary>
        /// <param name="id">Customer Id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> Get(int id)
        {
            var customer = await _db.Customers.FindAsync(id);

            if (customer is null)
            {
                return NotFound();
            }

            return customer;
        }

        /// <summary>
        /// Method to create a customer in database
        /// </summary>
        /// <param name="vm">Customer create view model</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateModel]
        public async Task<ActionResult<Customer>> Create(CustomerCreateVm vm)
        {

            var customer = new Customer()
            {
                Name = vm.Name,
                Number = vm.Number
            };

            _db.Customers.Add(customer);

            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = customer.Id }, customer);
        }

        /// <summary>
        /// Method to update an existing customer
        /// </summary>
        /// <param name="id">Customer Id</param>
        /// <param name="vm">Customer update view model</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ValidateModel]
        public async Task<IActionResult> Update(int id, CustomerEditVm vm)
        {
            if (id != vm.Id)
            {
                return BadRequest();
            }

            var customer = await _db.Customers.FindAsync(vm.Id);

            if (customer is null)
            {
                return NotFound();
            }

            customer.Name = vm.Name;
            customer.Number = vm.Number;

            await _db.SaveChangesAsync();
            return Ok();
        }

        /// <summary>
        /// Method to delete an existing customer 
        /// </summary>
        /// <param name="id">Customer Id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Customer>> Delete(int id)
        {
            var customer = await _db.Customers.FindAsync(id);

            if (customer is null)
            {
                return NotFound();
            }

            _db.Customers.Remove(customer);
            await _db.SaveChangesAsync();
            return customer;
        }
    }
}