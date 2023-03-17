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

        public IActionResult AddCargo()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddCargo(string carNumber, double enteringMass)
        {
            if (ModelState.IsValid)
            {
                _cargoRepository.AddCargo(carNumber, enteringMass);
                return RedirectToAction();

            }
            return View();
        }
        public IActionResult UpdateCargoLeavingMass()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UpdateCargoLeavingMass(string carNumber, double leavingMass)
        {
            if (ModelState.IsValid)
            {
                _cargoRepository.UpdateCargoLeavingMass(carNumber, leavingMass);
                return RedirectToAction();
            }
            return View();
        }
    }

}
