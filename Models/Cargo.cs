namespace HenriJervsonGrainWarehouse.Models
{
    public class Cargo
    {
        public int Id { get; set; }
        public string CarNumber { get; set; }
        public double EnteringMass { get; set; }
        public double? LeavingMass { get; set; }
    }
}
