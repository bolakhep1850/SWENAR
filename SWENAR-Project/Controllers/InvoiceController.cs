using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using SWENAR.Data;
using SWENAR.Models;
using SWENAR.Validation;
using SWENAR.ViewModels;
using static SWENAR.Helpers.FileHelpers;

namespace SWENAR.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly SWENARDBContext _db;
        private readonly IWebHostEnvironment _env;

        public InvoiceController(SWENARDBContext db, IWebHostEnvironment env)
        {
            this._db = db;
            this._env = env;
        }

        /// <summary>
        /// Method to get all invoices
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Invoice>>> Get()
        {
            var invoices = await _db.Invoices
                .OrderBy(a => -a.Id).ToListAsync();
            return invoices;
        }

        /// <summary>
        /// Method to get all invoice details
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<InvoiceDetailVm>>> GetInvoiceDetails()
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
            return invoices;
        }

        /// <summary>
        /// Method to get a invoice 
        /// </summary>
        /// <param name="id">Invoice Id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Invoice>> Get(int id)
        {
            var invoice = await _db.Invoices.FindAsync(id);

            if (invoice is null)
            {
                return NotFound();
            }

            return invoice;
        }

        /// <summary>
        /// Method to get all invoice detail
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<InvoiceDetailVm>> GetInvoiceDetail(int id)
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
            return invoice;
        }

        /// <summary>
        /// Method to get a invoices for a customer by customer id
        /// </summary>
        /// <param name="id">Customer Id</param>
        /// <returns>List of invoices for a customer</returns>
        [HttpGet("[action]/{customerid}")]
        public async Task<ActionResult<List<InvoiceDetailVm>>> GetInvoiceByCustomerId(int customerId)
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

            return invoices;
        }

        /// <summary>
        /// Method to get a invoices for a customer by customer number
        /// </summary>
        /// <param name="id">Customer Id</param>
        /// <returns>List of invoices for a customer</returns>
        [HttpGet("[action]/{customernumber}")]
        public async Task<ActionResult<List<InvoiceDetailVm>>> GetInvoiceByCustomerNumber(string customerNumber)
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

            return invoices;
        }

        /// <summary>
        /// Method to create a invoice in database
        /// </summary>
        /// <param name="vm">Invoice create view model</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateModel]
        public async Task<ActionResult<Invoice>> Create(InvoiceCreateVm vm)
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

            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = invoice.Id }, invoice);
        }

        /// <summary>
        /// Method to update an existing Invoice
        /// </summary>
        /// <param name="id">Invoice Id</param>
        /// <param name="vm">Invoice update view model</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ValidateModel]
        public async Task<IActionResult> Update(int id, InvoiceEditVm vm)
        {
            if (id != vm.Id)
            {
                return BadRequest();
            }

            var invoice = await _db.Invoices.FindAsync(vm.Id);

            if (invoice is null)
            {
                return NotFound();
            }

            invoice.CustomerId = vm.CustomerId;
            invoice.InvoiceNumber = vm.InvoiceNumber;
            invoice.Amount = vm.Amount;
            invoice.InvoiceDate = Convert.ToDateTime(vm.InvoiceDate);
            invoice.DueDate = Convert.ToDateTime(vm.DueDate);
            invoice.Status = vm.Status ?? invoice.Status;

            await _db.SaveChangesAsync();
            return Ok();
        }

        /// <summary>
        /// Method to delete an existing Invoice 
        /// </summary>
        /// <param name="id">Invoice Id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Invoice>> Delete(int id)
        {
            var invoice = await _db.Invoices.FindAsync(id);

            if (invoice is null)
            {
                return NotFound();
            }

            _db.Invoices.Remove(invoice);
            await _db.SaveChangesAsync();
            return invoice;
        }

        /// <summary>
        /// Loads excel file with invoices to the database
        /// </summary>
        /// <param name="excelFile">IFormfile Excel file</param>
        /// <returns>List of rows from excel file</returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<IEnumerable<InvoiceLoadVm>>> Load([FromForm]IFormFile excelFile)
        {
            var customers = await _db.Customers.ToListAsync();

            if (excelFile == null || excelFile.Length <= 0)
            {
                return null;
            }

            if (!Path.GetExtension(excelFile.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            var invoiceVms = ReadFile(excelFile);
            await _db.Customers.AddRangeAsync(invoiceVms
                .Where(a => !customers.Any(c => c.Number.ToLower() == a.CustomerNumber.ToLower()))
                .Select(a => new Customer()
                {
                    Name = a.CustomerName,
                    Number = a.CustomerNumber
                }));

            await _db.SaveChangesAsync();
            customers = await _db.Customers.ToListAsync();
            var currentMaxInvoiceId = 1;
            if (_db.Customers.Count() > 0)
            {
                currentMaxInvoiceId = await _db.Invoices.MaxAsync(a => a.Id);
            }

            await _db.Invoices.AddRangeAsync(invoiceVms.Select(i => new Invoice()
            {
                CustomerId = customers.SingleOrDefault(c => c.Number.ToLower() == i.CustomerNumber.ToLower()).Id,
                InvoiceNumber = i.InvoiceNumber,
                InvoiceDate = i.InvoiceDate,
                DueDate = i.DueDate,
                Amount = i.Amount,
                Status = InvoiceStatus.PendingPayment
            }));

            await _db.SaveChangesAsync();

            return _db.Invoices
                .Where(i => i.Id > currentMaxInvoiceId)
                .Select(i => new InvoiceLoadVm()
                {
                    Id = i.Id,
                    InvoiceNumber = i.InvoiceNumber,
                    InvoiceDate = i.InvoiceDate,
                    DueDate = i.DueDate,
                    Amount = i.Amount
                }).ToList();
        }

        [HttpPost("[action]")]
        [ValidateModel]
        public async Task<ActionResult<Attachment>> Attachment(AttachmentVm vm)
        {
            if (vm is null)
            {
                return BadRequest();
            }

            var attachment = new Attachment()
            {
                InvoiceId = vm.InvoiceId,
                File = new SWENAR.Models.File()
                {
                    Name = vm.File.FileName,
                    ContentType = vm.File.ContentType,
                    FileData = new FileData()
                    {
                        Data = FileHelper.ReadFully(vm.File.OpenReadStream())
                    }
                }
            };

            _db.Attachments.Add(attachment);
            if (await _db.SaveChangesAsync() > 0)
                return CreatedAtAction(nameof(Attachment), new { id = attachment.Id },
                    new Attachment() { Id = attachment.Id });

            return StatusCode(500);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Download(int attachmentId)
        {
            var attachment = await _db.Attachments.Include(a => a.File)
                .ThenInclude(f => f.FileData)
                .SingleOrDefaultAsync(a => a.Id == attachmentId);

            if (attachment is null)
            {
                return NotFound();
            }

            return File(attachment.File.FileData.Data, attachment.File.ContentType, attachment.File.Name);
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteAttachment(int attachmentId)
        {
            var invoiceAttachment = await _db.Attachments.FindAsync(attachmentId);

            if (invoiceAttachment is null)
            {
                return NotFound();
            }

            _db.Attachments.Remove(invoiceAttachment);

            if (await _db.SaveChangesAsync() > 0)
                return Ok();

            return StatusCode(500);
        }

        /// <summary>
        /// Reads excel and return a list of rows
        /// </summary>
        /// <param name="excelFile">Excel file</param>
        /// <returns>List of rows</returns>
        private static List<InvoiceLoadVm> ReadFile(IFormFile excelFile)
        {
            var invoices = new List<InvoiceLoadVm>();
            using (var package = new ExcelPackage(excelFile.OpenReadStream()))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                var rowCount = worksheet.Dimension.Rows;

                for (int row = 2; row <= rowCount; row++)
                {
                    var invoice = new InvoiceLoadVm()
                    {
                        CustomerName = worksheet.Cells[row, 1].Value.ToString().Trim(),
                        CustomerNumber = worksheet.Cells[row, 2].Value.ToString().Trim(),
                        InvoiceNumber = worksheet.Cells[row, 3].Value.ToString().Trim(),
                        InvoiceDate = Convert.ToDateTime(worksheet.Cells[row, 4].Value.ToString().Trim()),
                        DueDate = Convert.ToDateTime(worksheet.Cells[row, 5].Value.ToString().Trim()),
                        Amount = Convert.ToDecimal(worksheet.Cells[row, 6].Value.ToString().Trim())
                    };

                    invoices.Add(invoice);
                }
            }

            return invoices;
        }
    }


}