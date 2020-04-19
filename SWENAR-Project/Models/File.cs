using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SWENAR.Models
{
    public class File
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string ContentType { get; set; }

        public int FileDataId { get; set; }
        public FileData FileData { get; set; }

        public virtual Attachment Attachment { get; set; }
    }
}
