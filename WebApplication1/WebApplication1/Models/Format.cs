using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [Table("tFormats")]
    public class Format
    {
        [Key]
        public int FormatId { get; set; }
        public string Name { get; set; }
    }
}
