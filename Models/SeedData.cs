using HenriJervsonGrainWarehouse.Data;
using Microsoft.EntityFrameworkCore;

namespace HenriJervsonGrainWarehouse.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            Console.WriteLine("SeedData Initialize method called."); // Debugging code
            using (var context = new MyDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<MyDbContext>>()))
            {

                if (context.Cargo.Any())
                {
                    return;
                }

                context.Cargo.AddRange(
                    new Cargo
                    {
                        CarNumber = "abc123",
                        EnteringMass = 165,
                        LeavingMass = 150
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
