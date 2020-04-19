using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SWENAR.ViewModels
{
    public class InvoiceDetailVm
    {
        public InvoiceDetailVm()
        {
            Attachments = new List<AttachmentDetailVm>();
        }

        public int Id { get; set; }
        public string InvoiceNo { get; set; }
        public int CustomerId { get; set; }
        public string CustomerNumber { get; set; }
        public string CustomerName { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public DateTime? DueDate { get; set; }
        public string Status { get; set; }

        public List<AttachmentDetailVm> Attachments { get; set; }

    }
}
