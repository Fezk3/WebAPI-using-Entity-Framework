using Microsoft.EntityFrameworkCore;
using WebAPI.Models.Domain;

namespace WebAPI.Data
{
    public class NZWalksDbContext: DbContext
    {

        public NZWalksDbContext(DbContextOptions dbContextOpctions) : base(dbContextOpctions)
        {
                
        }

        // Db Sets (collection of entities/tables in the Data Base)

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }

        public DbSet<Walk> Walks { get; set; }

    }
}
