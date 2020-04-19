using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SWENAR.Models
{
    public class Attachment
    {
        [Key]
        public int Id { get; set; }
        
        public int FileId { get; set; }
        public File File { get; set; }

        public string Description { get; set; }

        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }
    }
}
