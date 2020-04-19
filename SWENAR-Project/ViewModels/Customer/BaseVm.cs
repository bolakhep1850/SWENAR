using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SWENAR.ViewModels
{
    public class BaseCustomerVm
    {
        [DisplayName("Customer Name")]
        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(256, ErrorMessage = "Maximum length for customer name is 256 characters.")]
        public string Name { get; set; }

        [DisplayName("Customer Number")]
        [Required(ErrorMessage = "Number is required.")]
        [MaxLength(100, ErrorMessage = "Maximum length for customer number is 100 characters.")]
        public string Number { get; set; }
    }
}
