using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HenriJervsonGrainWarehouse.Data;
using HenriJervsonGrainWarehouse.Models;
using System.Net.NetworkInformation;

namespace HenriJervsonGrainWarehouse.Controllers
{
    public class WarehouseController : Controller
    {
        private readonly MyDbContext _context;
        private readonly CargoRepository _cargoRepository;
        public WarehouseController(CargoRepository cargoRepository)
        {
            _cargoRepository = cargoRepository;
        }

        // GET: Warehouse
        

        // POST: Warehouse/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CarNumber,EnteringMass,LeavingMass")] Cargo cargo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cargo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cargo);
        }
        private bool CargoExists(int id)
        {
          return _context.Cargo.Any(e => e.Id == id);
        }
        public IActionResult GetAllCargo()
        {
            return View();
        }
        public async Task<IActionResult> GetAllCargo(string carNumber, double EnteringMass, double LeavingMass)
        {
            if (ModelState.IsValid)
            {
                ViewData["carNumber"] = new SelectList(_context.Set<Cargo>(), "CarNumber");
                ViewData["EnteringMass"] = new SelectList(_context.Set<Cargo>(), "EnteringMass");
                ViewData["LeavingMass"] = new SelectList(_context.Set<Cargo>(), "LeavingMass");
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Warehouse(string carNumber, double enteringMass, double leavingMass)
        {
            if (ModelState.IsValid)
            {
                _cargoRepository.Warehouse(carNumber, enteringMass, leavingMass);

            }
            return View();
        }

    }
}
