using Microsoft.AspNetCore.Mvc;

namespace HenriJervsonGrainWarehouse
{
    using HenriJervsonGrainWarehouse;
    using HenriJervsonGrainWarehouse.Models;
    using Microsoft.AspNetCore.Mvc;
    using HenriJervsonGrainWarehouse.Data;
    using Microsoft.EntityFrameworkCore;

    public class CargoController : Controller
    {
        private readonly CargoRepository _cargoRepository;

        public CargoController(CargoRepository cargoRepository)
        {
            _cargoRepository = cargoRepository;
        }

        [HttpPost]
        public IActionResult AddCargo()
        {
            return View();
        }
        public async Task<IActionResult> AddCargo(string carNumber, double enteringMass)
        {
            if (ModelState.IsValid)
            {
                _cargoRepository.AddCargo(carNumber, enteringMass);
                return RedirectToAction("Index", "Home");

            }
            return View();
        }

        [HttpPost]
        public IActionResult UpdateCargoLeavingMass(int id, double leavingMass)
        {
            _cargoRepository.UpdateCargoLeavingMass(id, leavingMass);
            return RedirectToAction("Index", "Home");
        }
    }

}
