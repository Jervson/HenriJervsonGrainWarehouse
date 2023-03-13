using Microsoft.AspNetCore.Mvc;
using HenriJervsonGrainWarehouse;

namespace HenriJervsonGrainWarehouse
{
    using HenriJervsonGrainWarehouse.Controllers;
    using HenriJervsonGrainWarehouse.Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;

    public class HomeController : Controller
    {
        private readonly CargoRepositoryController _cargoRepository;

        public HomeController(CargoRepositoryController cargoRepository)
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
