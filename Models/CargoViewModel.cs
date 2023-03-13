namespace HenriJervsonGrainWarehouse
{
    public class CargoViewModel
    {
        public int Id { get; set; }
        public string CarNumber { get; set; }
        public double EnteringMass { get; set; }
        public List<double?> LeavingMasses { get; set; }
    }

}
