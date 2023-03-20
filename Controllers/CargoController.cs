using Microsoft.AspNetCore.Mvc;

namespace HenriJervsonGrainWarehouse
{
    using HenriJervsonGrainWarehouse;
    using HenriJervsonGrainWarehouse.Models;
    using Microsoft.AspNetCore.Mvc;
    using HenriJervsonGrainWarehouse.Data;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class CargoController : Controller
    {
        private readonly MyDbContext _context;
        private readonly CargoRepository _cargoRepository;

        public CargoController(MyDbContext context, CargoRepository cargoRepository)
        {
            _context = context;
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
        public async Task<IActionResult> UpdateCargoLeavingMass(string carNumber, double leavingMass)
        {
                IQueryable<string> genreQuery = from m in _context.Cargo
                                                orderby m.CarNumber
                                                select m.CarNumber;

                var cargos = from m in _context.Cargo
                             select m;

            if (!string.IsNullOrEmpty(carNumber))
            {
                cargos = cargos.Where(x => x.CarNumber == carNumber);
            }

            var LeavingCargoVM = new WarehouseViewModel
                {
                    CarNumber = new SelectList(await genreQuery.Distinct().ToListAsync()),
                    Cargos = await cargos.ToListAsync()
                };
            
            if (ModelState.IsValid)
            {
                _cargoRepository.UpdateCargoLeavingMass(carNumber, leavingMass);
                return RedirectToAction();
            }
            return View();
        }
    }

}
