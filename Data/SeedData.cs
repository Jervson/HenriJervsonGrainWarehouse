using HenriJervsonGrainWarehouse.Models;
using Microsoft.AspNetCore.Identity;

namespace HenriJervsonGrainWarehouse.Data
{
    public class SeedData
    {
        public const string ROLE_ADMIN = "Admin";
        public static async Task SeedIdentity(UserManager<WarehouseUser> userManager, RoleManager<WarehouseRole> roleManager)
        {
            var user = await userManager.FindByNameAsync("admin@GrainwareHouse.com");
            if (user == null)
            {
                user = new WarehouseUser();
                user.Email = "admin@GrainwareHouse.com";
                user.EmailConfirmed = true;
                user.UserName = "admin@GrainwareHouse.com";
                var userResult = await userManager.CreateAsync(user);
                if (!userResult.Succeeded)
                {
                    throw new Exception($"User creation failed: {userResult.Errors.FirstOrDefault()}");
                }
                await userManager.AddPasswordAsync(user, "Pa$$w0rd");
            }
            var role = await roleManager.FindByNameAsync(ROLE_ADMIN);
            if (role == null)
            {
                role = new WarehouseRole();
                role.Name = ROLE_ADMIN;
                role.NormalizedName = ROLE_ADMIN;
                var roleResult = roleManager.CreateAsync(role).Result;
                if (!roleResult.Succeeded)
                {
                    throw new Exception(roleResult.Errors.First().Description);
                }
            }
            await userManager.AddToRoleAsync(user, ROLE_ADMIN);
        }
    }
}
