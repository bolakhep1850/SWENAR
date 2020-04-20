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

        public async Task<Customer> CreateAsync(CustomerCreateVm createVm)
        {
            var customer = new Customer()
            {
                Name = createVm.Name,
                Number = createVm.Number
            };

            _db.Customers.Add(customer);
            await _db.SaveChangesAsync();
            return customer;
        }

        public async Task<bool> DeleteAsync(int customerId)
        {
            var customer = await _db.Customers.FindAsync(customerId);

            if (customer is null)
            {
                return false;
            }

            _db.Customers.Remove(customer);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Customer>> GetAsync()
        {
            return await _db.Customers
                .OrderBy(a => -a.Id).ToListAsync();
        }

        public async Task<Customer> GetAsync(int id)
        {
            return await _db.Customers.FindAsync(id);

        }

        public async Task<bool> UpdateAsync(CustomerEditVm editVm)
        {
            var customer = await _db.Customers.FindAsync(editVm.Id);

            if (customer != null)
            {
                customer.Name = editVm.Name;
                customer.Number = editVm.Number;

                return await _db.SaveChangesAsync() >= 0;
            }

            return false;
        }
    }
}
