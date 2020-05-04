using LaYumba.Functional;
using SWENAR.Models;
using SWENAR.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SWENAR.Repository
{
    public interface IInvoiceRepository
    {
        Task<Option<List<Invoice>>> GetAsync();
        Task<Option<Invoice>> GetAsync(int id);
        Task<Option<List<InvoiceDetailVm>>> GetInvoiceDetailsAsync();
        Task<Option<InvoiceDetailVm>> GetInvoiceDetailAsync(int id);
        Task<Option<List<InvoiceDetailVm>>> GetInvoiceByCustomerIdAsync(int customerId);
        Task<Option<List<InvoiceDetailVm>>> GetInvoiceByCustomerNumberAsync(string customerNumber);
        Task<Option<Invoice>> CreateAsync(InvoiceCreateVm vm);
        Task<Option<bool>> UpdateAsync(InvoiceEditVm vm);
        Task<Option<bool>> DeleteAsync(int id);

    }
}
