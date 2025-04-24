using LegoCollection.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace LegoCollection.Data
{
    public class LegoCollectionContext(DbContextOptions<LegoCollectionContext> options) : DbContext(options)
    {
        
        public DbSet<Owned> Legos => Set<Owned>();

        public DbSet<ColorList> Colors => Set<ColorList>();   
    }
}
