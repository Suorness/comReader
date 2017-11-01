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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CheckPointEntry>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Person>()
                .HasKey(p => p.Id);
            
        }
    }
}
