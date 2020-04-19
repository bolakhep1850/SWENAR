using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SWENAR.Models
{
    public class FileData
    {
        [Key]
        public int Id { get; set; }
        public byte[] Data { get; set; }

        public virtual File File { get; set; }
    }
}
