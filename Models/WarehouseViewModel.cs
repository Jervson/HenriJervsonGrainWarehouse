using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace HenriJervsonGrainWarehouse.Models
{
    public class WarehouseViewModel
    {
        public List<Cargo> Cargos { get; set; }
        public SelectList CarNumbers { get; set; }
        public string CarNumber { get; set; }
    }
}
