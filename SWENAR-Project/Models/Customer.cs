using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SWENAR.Models
{
    public class Customer
    {
        public Customer()
        {
            Invoices = new HashSet<Invoice>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(256)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string Number { get; set; }

        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}
