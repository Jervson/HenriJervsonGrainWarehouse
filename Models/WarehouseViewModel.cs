using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace HenriJervsonGrainWarehouse.Models
{
    public class WarehouseViewModel
    {
        public List<Cargo> Cargos { get; set; }
        public SelectList? CarNumber { get; set; }
        public double? EnteringMass { get; set; }
        public double? LeavingMass { get; set; }
    }
}
