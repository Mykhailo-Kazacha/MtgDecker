using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public class MtgDeckerContext : DbContext
    {
        public DbSet<Card> Cards { get; set; }
        public DbSet<Deck> Decks { get; set; }
        public DbSet<DecklistEntry> DecklistEntries { get; set; }
        public DbSet<Format> Formats { get; set; }
        public  DbSet<Match> Matches { get; set; }

        public MtgDeckerContext(DbContextOptions<MtgDeckerContext> options) :base(options)
        {
            Database.EnsureCreated();
        }

        public override int SaveChanges()
        {
            //delete unchanged entries of declists

            return base.SaveChanges();
        }

        //  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //  {
        //  optionsBuilder.UseSqlServer("Server=(localdb)\\mtgDecker;Database=mtgdecker;Trusted_Connection=True;");

        //   }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Card>().HasIndex(c => c.Name).IsUnique();

            modelBuilder.Entity<DecklistEntry>().HasKey(dle => 
            new { dle.DecklistId, dle.CardId, dle.Quantity, dle.Index, dle.IsSideboard});
            //modelBuilder.Entity<DecklistEntry>().Property(dle => dle.DecklistId).ValueGeneratedOnAdd();
        }
    }
}
