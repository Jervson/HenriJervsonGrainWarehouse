using System.ComponentModel.DataAnnotations;

namespace HenriJervsonGrainWarehouse.Models
{
    public class Cargo
    {
        public int Id { get; set; }
        public string CarNumber { get; set; }
        [Range(4, 500)]
        public double EnteringMass { get; set; }
        [Range(4, 500)]
        public double? LeavingMass { get; set; }

        public double? CargoBrought
        {
            get
            {
                return EnteringMass - LeavingMass;
            }
        }
    }
}
