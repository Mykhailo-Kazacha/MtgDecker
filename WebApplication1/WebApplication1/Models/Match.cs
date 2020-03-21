using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [Table("tMatches")]
    public class Match
    {
        [Key]
        public int MatchId { get; set; }

        public int DeckId { get; set; }
        public Deck Deck { get; set; }

        public string OppDeck { get; set; }
        
        [ForeignKey("Format_Match")]
        public int FormatId { get; set; }
        public Format Format { get; set; }

        public int Wins { get; set; }
        public int Losses { get; set; }
        public string Result { get; set; }
        public string Comment { get; set; }
    }
}
