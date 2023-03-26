using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HenriJervsonGrainWarehouse.Data;
using HenriJervsonGrainWarehouse.Models;
using System.Net.NetworkInformation;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace HenriJervsonGrainWarehouse.Controllers
{
    public class WarehouseController : Controller
    {
        private readonly MyDbContext _context;
        private readonly CargoRepository _cargoRepository;

        public WarehouseController(MyDbContext context, CargoRepository cargoRepository)
        {
            _context = context;
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

        [Authorize(Roles = SeedData.ROLE_ADMIN)]
        public async Task<IActionResult> WarehouseView([Bind] string CarNumber)
        {
            var applicationDbContext = _context
                            .Cargo
                            .Include(c => c.CarNumber)
                            .Where(r => r.CarNumber == CarNumber);

            IQueryable<string> genreQuery = from m in _context.Cargo
                                            orderby m.CarNumber
                                            select m.CarNumber;
            var cargos = from m in _context.Cargo
                         select m;

            if (!string.IsNullOrEmpty(CarNumber))
            {
                cargos = cargos.Where(x => x.CarNumber == CarNumber);
            }
            var enteringMasses = from c in cargos
                                 select c.EnteringMass;

            var leavingMasses = from c in cargos
                                select c.LeavingMass;

            double totalMass = (double)(enteringMasses.Sum() - leavingMasses.Sum());

            var warehouseVM = new WarehouseViewModel
            {
                CarNumbers = new SelectList(await genreQuery.Distinct().ToListAsync()),
                Cargos = await cargos.ToListAsync(),
                TotalMass = totalMass
            };

            return View(warehouseVM);
        }
    }
}