using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Contexts
{
    public class Db : DbContext
    {
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Species> Species { get; set; }

        public Db(DbContextOptions options) : base(options)
        {
        }
    }
}
