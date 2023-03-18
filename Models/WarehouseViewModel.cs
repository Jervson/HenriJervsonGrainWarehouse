using Microsoft.AspNetCore.Mvc.Rendering;

namespace HenriJervsonGrainWarehouse.Models
{
    public class WarehouseViewModel
    {
        public List<Cargo> Cargo { get; set; }
        public SelectList CarNumber { get; set; }
        public double EnteringMass { get; set; }
        public double LeavingMass { get; set; }
    }
}
