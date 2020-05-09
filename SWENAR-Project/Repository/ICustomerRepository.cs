using LaYumba.Functional;
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
        Task<Option<List<Customer>>> GetAsync();
        Task<Option<Customer>> GetAsync(int id);
        Task<Option<Customer>> CreateAsync(CustomerCreateVm createVm);
        Task<Option<bool>> UpdateAsync(CustomerEditVm editVm);
        Task<Option<bool>> DeleteAsync(int customerId);
    }
}
