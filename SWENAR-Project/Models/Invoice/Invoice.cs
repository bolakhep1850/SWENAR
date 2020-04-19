using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SWENAR.Models
{
    public class Invoice
    {
        [Key]
        public int Id { get; set; }

        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        [Required]
        [MaxLength(100)]
        public string InvoiceNumber { get; set; }

        public decimal? Amount { get; set; }

        public DateTime InvoiceDate { get; set; }
        public DateTime DueDate { get; set; }

        public InvoiceStatus Status { get; set; }

        public virtual ICollection<Attachment> Attachments { get; set; }
    }
}
