using Microsoft.AspNetCore.Mvc;
using HenriJervsonGrainWarehouse;

namespace HenriJervsonGrainWarehouse
{
    using HenriJervsonGrainWarehouse;
    using HenriJervsonGrainWarehouse.Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;

    public class HomeController : Controller
    {
        private readonly CargoRepository _cargoRepository;

        public HomeController(CargoRepository cargoRepository)
        {
            _cargoRepository = cargoRepository;
        }

        public IActionResult Index()
        {
            var cargo = _cargoRepository.GetAllCargo()
                .Select(c => new CargoViewModel
                {
                    Id = c.Id,
                    CarNumber = c.CarNumber,
                    EnteringMass = c.EnteringMass,
                    LeavingMasses = _cargoRepository.GetLeavingMassesForCargo(c.Id)
                })
                .ToList();
            return View(cargo);
        }

        [HttpPost]
        public IActionResult UpdateCargoLeavingMass(int id, double leavingMass)
        {
            _cargoRepository.UpdateCargoLeavingMass(id, leavingMass);
            return RedirectToAction("Index");
        }
        public IActionResult Privacy()
        {
            return View();
        }
    }

}
