using Microsoft.AspNetCore.Mvc;
using SWENAR.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SWENAR.ViewModels
{
    public class InvoiceEditVm: BaseInvoiceVm
    {
        [HiddenInput]
        public int Id { get; set; }

        public InvoiceStatus? Status { get; set; }
    }
}
