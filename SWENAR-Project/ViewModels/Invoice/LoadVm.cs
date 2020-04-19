using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SWENAR.ViewModels
{
    public class InvoiceLoadVm
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string CustomerNumber { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime DueDate { get; set; }
        public decimal? Amount { get; set; }
    }
}
