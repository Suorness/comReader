using System.Data.Entity;

namespace comReaderLib.Domain
{
    public class ContextReader : DbContext
    {
        public ContextReader()
        : base("comReaderDB")
        {
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<CheckPointEntry> CheckPointEntries { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Card> Cards { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CheckPointEntry>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Person>()
                .HasKey(p => p.Id)
                .HasOptional(p => p.Card)
                .WithRequired(card => card.Person);

            modelBuilder.Entity<Card>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Device>()
                .HasKey(p => p.Id);
        }
    }
}
