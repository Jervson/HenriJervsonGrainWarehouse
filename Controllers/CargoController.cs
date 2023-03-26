using Microsoft.AspNetCore.Mvc;
using HenriJervsonGrainWarehouse;
using HenriJervsonGrainWarehouse.Models;
using HenriJervsonGrainWarehouse.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace HenriJervsonGrainWarehouse
{

    public class CargoController : Controller
    {
        private readonly CargoRepository _cargoRepository;

        public CargoController(CargoRepository cargoRepository)
        {
            _cargoRepository = cargoRepository;
        }

        [Authorize(Roles = SeedData.ROLE_ADMIN)]
        public IActionResult AddCargo()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = SeedData.ROLE_ADMIN)]
        public IActionResult AddCargo(string carNumber, double enteringMass)
        {
            if (ModelState.IsValid)
            {
                _cargoRepository.AddCargo(carNumber, enteringMass);
                return RedirectToAction();

            }
            return View();
        }
        [Authorize(Roles = SeedData.ROLE_ADMIN)]
        public IActionResult UpdateCargoLeavingMass()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = SeedData.ROLE_ADMIN)]
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
