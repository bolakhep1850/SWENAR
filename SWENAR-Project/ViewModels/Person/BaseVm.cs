using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SWENAR.ViewModels
{
    public class PersonBaseVm
    {
        [Required(ErrorMessage = "First Name is required.")]
        [MaxLength(100, ErrorMessage = "Maximum length for First Name is 100 characters.")]
        public string FName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        [MaxLength(100, ErrorMessage = "Maximum length for Last Name is 100 characters.")]
        public string LName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [MaxLength(100, ErrorMessage = "Maximum length for Email is 100 characters.")]
        [EmailAddress]
        public string Email { get; set; }

        [MaxLength(16, ErrorMessage = "Maximum length for Phone is 16 characters.")]
        public string Phone { get; set; }

        public int? CustomerId { get; set; }
    }
}
