using LaYumba.Functional;
using static LaYumba.Functional.F;
using Microsoft.EntityFrameworkCore;
using SWENAR.Data;
using SWENAR.Models;
using SWENAR.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SWENAR.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly SWENARDBContext _db;

        public CustomerRepository(SWENARDBContext db)
        {
            this._db = db;
        }

        public async Task<Option<Customer>> CreateAsync(CustomerCreateVm createVm)
        {
            var customer = new Customer()
            {
                Name = createVm.Name,
                Number = createVm.Number
            };

            _db.Customers.Add(customer);
            if (await _db.SaveChangesAsync() > 0) return Some(customer);
            return None;
        }

        public async Task<Option<bool>> DeleteAsync(int customerId)
        {
            var customer = await _db.Customers.FindAsync(customerId);

            if (customer is null)
            {
                return None;
            }

            _db.Customers.Remove(customer);
            if (await _db.SaveChangesAsync() > 0) return Some(true);
            return Some(false);
        }

        public async Task<Option<List<Customer>>> GetAsync()
        {
            var customers = await _db.Customers
                .OrderBy(a => -a.Id).ToListAsync();

            if (!customers.Any())
            {
                return None;
            }

            return Some(customers);
        }

        public async Task<Option<Customer>> GetAsync(int id)
        {
            var item = await _db.Customers.FindAsync(id);
            if (item is null)
            {
                return None;
            }

            return Some(item);
        }

        public async Task<Option<bool>> UpdateAsync(CustomerEditVm editVm)
        {
            var customer = await _db.Customers.FindAsync(editVm.Id);

            if (customer is null)
            {
                return None;
            }

            customer.Name = editVm.Name;
            customer.Number = editVm.Number;

            if (await _db.SaveChangesAsync() > 0) return Some(true);
            return Some(false);
        }
    }
}
