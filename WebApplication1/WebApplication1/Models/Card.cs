using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;


namespace WebApplication1.Models
{
    [Table("tCards")]
    public class Card
    {
        private string _legality;

        [Key]
        public int CardId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Art { get; set; }

        [NotMapped]
        public List<int> Legality { get { return JsonSerializer.Deserialize<List<int>>(string.IsNullOrEmpty(_legality) ? "[]" :_legality); }
                 set { _legality = JsonSerializer.Serialize(value); } }
    }
}
