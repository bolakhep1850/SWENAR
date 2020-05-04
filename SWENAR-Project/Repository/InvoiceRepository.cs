using SWENAR.Data;
using SWENAR.Models;
using SWENAR.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LaYumba.Functional;
using static LaYumba.Functional.F;
using System;

namespace SWENAR.Repository
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly SWENARDBContext _db;

        public InvoiceRepository(SWENARDBContext db)
        {
            this._db = db;
        }

        public async Task<Option<Invoice>> CreateAsync(InvoiceCreateVm vm)
        {
            var invoice = new Invoice()
            {
                CustomerId = vm.CustomerId,
                InvoiceNumber = vm.InvoiceNumber,
                Amount = vm.Amount,
                InvoiceDate = Convert.ToDateTime(vm.InvoiceDate),
                DueDate = Convert.ToDateTime(vm.DueDate)
            };

            _db.Invoices.Add(invoice);

            if (await _db.SaveChangesAsync() > 0) return Some(invoice);
            return None;
        }

        public async Task<Option<bool>> DeleteAsync(int id)
        {
            var invoice = await _db.Invoices.FindAsync(id);

            if (invoice is null)
            {
                return None;
            }

            _db.Invoices.Remove(invoice);

            if (await _db.SaveChangesAsync() > 0) return Some(true);
            return Some(false);
        }

        public async Task<Option<List<Invoice>>> GetAsync()
        {
            var invoices = await _db.Invoices
                .OrderBy(a => -a.Id)
                .AsNoTracking()
                .ToListAsync();

            if (invoices is null)
            {
                return None;
            }

            return Some(invoices);
        }

        public async Task<Option<Invoice>> GetAsync(int id)
        {
            var invoice = await _db.Invoices.FindAsync(id);

            if (invoice is null)
            {
                return None;
            }

            return Some(invoice);
        }

        public async Task<Option<List<InvoiceDetailVm>>> GetInvoiceByCustomerIdAsync(int customerId)
        {
            var invoices = await _db.Invoices
                .Include(inv => inv.Customer)
                .Where(inv => inv.CustomerId == customerId)
                .Select(inv => new InvoiceDetailVm()
                {
                    Id = inv.Id,
                    Amount = inv.Amount,
                    CustomerId = inv.Customer.Id,
                    CustomerName = inv.Customer.Name,
                    CustomerNumber = inv.Customer.Number,
                    DueDate = inv.DueDate,
                    InvoiceDate = inv.InvoiceDate,
                    InvoiceNo = inv.InvoiceNumber,
                    Status = inv.Status.ToString()
                })
                .ToListAsync();

            if (invoices.Count > 0) return Some(invoices);
            return None;
        }

        public async Task<Option<List<InvoiceDetailVm>>> GetInvoiceByCustomerNumberAsync(string customerNumber)
        {
            var invoices = await _db.Invoices
                .Include(inv => inv.Customer)
                .Where(inv => inv.Customer.Number == customerNumber)
                .Select(inv => new InvoiceDetailVm()
                {
                    Id = inv.Id,
                    Amount = inv.Amount,
                    CustomerId = inv.Customer.Id,
                    CustomerName = inv.Customer.Name,
                    CustomerNumber = inv.Customer.Number,
                    DueDate = inv.DueDate,
                    InvoiceDate = inv.InvoiceDate,
                    InvoiceNo = inv.InvoiceNumber,
                    Status = inv.Status.ToString()
                })
                .ToListAsync();

            if (invoices.Count > 0) return Some(invoices);
            return None;
        }

        public async Task<Option<InvoiceDetailVm>> GetInvoiceDetailAsync(int id)
        {
            var invoice = await _db.Invoices
                .Include(i => i.Customer)
                .Include(i => i.Attachments)
                .OrderBy(i => -i.Id)
                .Select(i => new InvoiceDetailVm()
                {
                    Id = i.Id,
                    InvoiceNo = i.InvoiceNumber,
                    CustomerId = i.CustomerId,
                    CustomerNumber = i.Customer.Number,
                    CustomerName = i.Customer.Name,
                    Amount = i.Amount,
                    InvoiceDate = i.InvoiceDate,
                    DueDate = i.DueDate,
                    Status = i.Status.ToString(),
                    Attachments = i.Attachments == null ? null
                        : i.Attachments.Select(a => new AttachmentDetailVm()
                        {
                            Id = a.Id,
                            Name = a.File.Name
                        }).ToList()
                }).SingleOrDefaultAsync(i => i.Id == id);

            if (invoice is null) return None;
            return Some(invoice);

        }

        public async Task<Option<List<InvoiceDetailVm>>> GetInvoiceDetailsAsync()
        {
            var invoices = await _db.Invoices
               .Include(i => i.Customer)
               .Include(i => i.Attachments)
               .OrderBy(i => -i.Id)
               .Select(i => new InvoiceDetailVm()
               {
                   Id = i.Id,
                   InvoiceNo = i.InvoiceNumber,
                   CustomerId = i.CustomerId,
                   CustomerNumber = i.Customer.Number,
                   CustomerName = i.Customer.Name,
                   Amount = i.Amount,
                   InvoiceDate = i.InvoiceDate,
                   DueDate = i.DueDate,
                   Status = i.Status.ToString(),
                   Attachments = i.Attachments == null ? null
                       : i.Attachments.Select(a => new AttachmentDetailVm()
                       {
                           Id = a.Id,
                           Name = a.File.Name
                       }).ToList()
               }).ToListAsync();

            if (invoices.Count < 1) return None;
            return Some(invoices);

        }

        public async Task<Option<bool>> UpdateAsync(InvoiceEditVm vm)
        {
            var invoice = await _db.Invoices.FindAsync(vm.Id);

            if (invoice is null)
            {
                return None;
            }

            invoice.CustomerId = vm.CustomerId;
            invoice.InvoiceNumber = vm.InvoiceNumber;
            invoice.Amount = vm.Amount;
            invoice.InvoiceDate = Convert.ToDateTime(vm.InvoiceDate);
            invoice.DueDate = Convert.ToDateTime(vm.DueDate);
            invoice.Status = vm.Status ?? invoice.Status;

            if (await _db.SaveChangesAsync() > 0) return Some(true);
            return Some(false);
        }
    }
}
