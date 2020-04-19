using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SWENAR.ViewModels
{
    public class BaseInvoiceVm
    {
        [Required(ErrorMessage = "Customer Id is required.")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Invoice Number is required.")]
        [MaxLength(100, ErrorMessage = "Maximum length for invoice number is 100 characters.")]
        public string InvoiceNumber { get; set; }

        public decimal? Amount { get; set; }

        [Required(ErrorMessage = "Invoice Date is required.")]
        [DataType(DataType.Date)]
        public DateTime? InvoiceDate { get; set; }

        [Required(ErrorMessage = "Due Date is required.")]
        [DataType(DataType.Date)]
        public DateTime? DueDate { get; set; }
    }
}
