using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SWENAR.ViewModels
{
    public class PersonEditVm : PersonBaseVm
    {
        [Required(ErrorMessage = "Id is required.")]
        public int Id { get; set; }
    }
}
