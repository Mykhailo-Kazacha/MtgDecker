using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Infrastructure;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace WebApplication1.Models
{    
    [ModelBinder(BinderType = typeof(DecklistModelBinder))]
    public class Decklist
    {
        public string DeckName { get; set; }
        public List<DecklistEntry> DecklistEntries { get; set; }
        public string Comment { get; set; }
    }
}
