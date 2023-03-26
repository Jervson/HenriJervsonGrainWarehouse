using HenriJervsonGrainWarehouse.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HenriJervsonGrainWarehouse.Data
{
    public class MyDbContext : IdentityDbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }
        public DbSet<Cargo> Cargo { get; set; }
        public DbSet<WarehouseUser> WarehouseUsers { get; set; }
        public DbSet<WarehouseRole> WarehouseRoles { get; set; }
    }
}
