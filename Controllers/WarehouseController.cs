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

        public WarehouseController(MyDbContext context)
        {
            _context = context;
        }

        // GET: Warehouse
        public async Task<IActionResult> Index()
        {
              return View(await _context.Cargo.ToListAsync());
        }

        // GET: Warehouse/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cargo == null)
            {
                return NotFound();
            }

            var cargo = await _context.Cargo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cargo == null)
            {
                return NotFound();
            }

            return View(cargo);
        }

        // GET: Warehouse/Create
        public IActionResult Create()
        {
            return View();
        }

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

        // GET: Warehouse/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Cargo == null)
            {
                return NotFound();
            }

            var cargo = await _context.Cargo.FindAsync(id);
            if (cargo == null)
            {
                return NotFound();
            }
            return View(cargo);
        }

        // POST: Warehouse/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CarNumber,EnteringMass,LeavingMass")] Cargo cargo)
        {
            if (id != cargo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cargo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CargoExists(cargo.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(cargo);
        }

        // GET: Warehouse/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Cargo == null)
            {
                return NotFound();
            }

            var cargo = await _context.Cargo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cargo == null)
            {
                return NotFound();
            }

            return View(cargo);
        }

        // POST: Warehouse/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Cargo == null)
            {
                return Problem("Entity set 'MyDbContext.Cargo'  is null.");
            }
            var cargo = await _context.Cargo.FindAsync(id);
            if (cargo != null)
            {
                _context.Cargo.Remove(cargo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
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
        
    }
}
