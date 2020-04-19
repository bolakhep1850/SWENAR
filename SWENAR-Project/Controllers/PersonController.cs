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
    public class PersonController : ControllerBase
    {
        private readonly SWENARDBContext _db;
        public PersonController(SWENARDBContext db)
        {
            this._db = db;
        }

        /// <summary>
        /// Method to get all people
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> Get()
        {
            var persons = await _db.People
                .OrderBy(a => -a.Id).ToListAsync();
            return persons;
        }

        /// <summary>
        /// Method to get a person 
        /// </summary>
        /// <param name="id">Person Id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> Get(int id)
        {
            var person = await _db.People.FindAsync(id);

            if (person is null)
            {
                return NotFound();
            }

            return person;
        }

        /// <summary>
        /// Method to create a person in database
        /// </summary>
        /// <param name="vm">Person create view model</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateModel]
        public async Task<ActionResult<Person>> Create(PersonCreateVm vm)
        {

            var person = new Person()
            {
                FName = vm.FName,
                LName = vm.LName,
                Email = vm.Email,
                Phone = vm.Phone,
                CustomerId = vm.CustomerId
            };

            _db.People.Add(person);

            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = person.Id }, person);
        }

        /// <summary>
        /// Method to update an existing person
        /// </summary>
        /// <param name="id">Person Id</param>
        /// <param name="vm">Person update view model</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ValidateModel]
        public async Task<IActionResult> Update(int id, PersonEditVm vm)
        {
            if (id != vm.Id)
            {
                return BadRequest();
            }

            var person = await _db.People.FindAsync(vm.Id);

            if (person is null)
            {
                return NotFound();
            }

            person.FName = vm.FName;
            person.LName = vm.LName;
            person.Email = vm.Email;
            person.Phone = vm.Phone;
            person.CustomerId = vm.CustomerId;

            await _db.SaveChangesAsync();
            return Ok();
        }

        /// <summary>
        /// Method to delete an existing person 
        /// </summary>
        /// <param name="id">Person Id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Person>> Delete(int id)
        {
            var person = await _db.People.FindAsync(id);

            if (person is null)
            {
                return NotFound();
            }

            _db.People.Remove(person);
            await _db.SaveChangesAsync();
            return person;
        }
    }
}