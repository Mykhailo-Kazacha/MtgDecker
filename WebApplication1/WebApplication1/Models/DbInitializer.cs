using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class DbInitializer 
    {
        public DbInitializer(MtgDeckerContext db)
        {
            if (!db.Formats.Any())
            {
                db.Formats.Add(new Format { Name = "Historic" });
                db.Formats.Add(new Format { Name = "Standard BO1" });
                db.Formats.Add(new Format { Name = "Standard BO3" });
                db.Formats.Add(new Format { Name = "Standard" });
                db.SaveChanges();
            }
        }

    }
}
