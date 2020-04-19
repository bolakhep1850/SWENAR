using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SWENAR.ViewModels
{
    public class CustomerEditVm: BaseCustomerVm
    {
        [HiddenInput]
        public int Id { get; set; }

    }
}
