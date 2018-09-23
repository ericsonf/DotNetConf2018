using DotNetConf2018.Entities;
using Microsoft.EntityFrameworkCore;

namespace DotNetConf2018.Data
{
    public class DotNetConfContext : DbContext
    {
         public DotNetConfContext(DbContextOptions<DotNetConfContext> options)
            : base(options) { }

        public DbSet<Person> Persons { get; set; }
    }
}