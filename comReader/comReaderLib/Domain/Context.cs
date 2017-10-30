using System.Data.Entity;

namespace comReaderLib.Domain
{
    public class ContextReader3: DbContext
    {
        public DbSet <Person> listOfPerson { get; set; }
        public DbSet <DataControl> listOfData { get; set; }
    }
}
