using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SWENAR.ViewModels
{
    public class AttachmentVm
    {
        public int InvoiceId { get; set; }

        [Required]
        public IFormFile File { get; set; }

        [MaxLength(512)]
        public string Description { get; set; }
    }
}
