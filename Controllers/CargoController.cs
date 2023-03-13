using Microsoft.AspNetCore.Mvc;

namespace HenriJervsonGrainWarehouse
{
    using Microsoft.AspNetCore.Mvc;

    public class CargoController : Controller
    {
        private readonly CargoRepository _cargoRepository;

        public CargoController(CargoRepository cargoRepository)
        {
            _cargoRepository = cargoRepository;
        }

        [HttpPost]
        public IActionResult AddCargo(string carNumber, double enteringMass)
        {
            _cargoRepository.AddCargo(carNumber, enteringMass);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult UpdateCargoLeavingMass(int id, double leavingMass)
        {
            _cargoRepository.UpdateCargoLeavingMass(id, leavingMass);
            return RedirectToAction("Index", "Home");
        }
    }

}
