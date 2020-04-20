using SWENAR.Models;
using SWENAR.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SWENAR.Repository
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAsync();
        Task<Customer> GetAsync(int id);
        Task <Customer> CreateAsync(CustomerCreateVm createVm);
        Task<bool> UpdateAsync(CustomerEditVm editVm);
        Task<bool> DeleteAsync(int customerId);
    }
}
