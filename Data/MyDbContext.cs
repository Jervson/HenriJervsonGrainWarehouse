using HenriJervsonGrainWarehouse.Models;
using Microsoft.EntityFrameworkCore;

namespace HenriJervsonGrainWarehouse.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }
        public DbSet<Cargo> Cargo { get; set; }
    }
}
