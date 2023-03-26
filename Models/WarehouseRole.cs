using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace HenriJervsonGrainWarehouse.Models
{
    public class WarehouseRole : IdentityRole
    {
        [StringLength(128, MinimumLength = 1)]
        public string DisplayName { get; set; }
    }
}
