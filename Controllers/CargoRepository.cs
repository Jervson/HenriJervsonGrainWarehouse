using HenriJervsonGrainWarehouse.Models;

namespace HenriJervsonGrainWarehouse.Controllers
{
    using System;
    using HenriJervsonGrainWarehouse.Data;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;

    public class CargoRepositoryController
    {
        private readonly MyDbContext _context;

        public CargoRepositoryController(DbContextOptions<MyDbContext> options)
        {
            _context = new MyDbContext(options);
        }

        public void AddCargo(string carNumber, double enteringMass)
        {
            var cargo = new Cargo
            {
                CarNumber = carNumber,
                EnteringMass = enteringMass
            };
            _context.Cargo.Add(cargo);
            _context.SaveChanges();
        }

        public void UpdateCargoLeavingMass(int id, double leavingMass)
        {
            var cargo = _context.Cargo.Find(id);
            cargo.LeavingMass = leavingMass;
            _context.SaveChanges();
        }

        public List<double?> GetLeavingMassesForCargo(int carId)
        {
            using (var db = new MyDbContext(null))
            {
                return db.Cargo
                    .Where(c => c.Id == carId)
                    .Select(c => c.LeavingMass)
                    .ToList();
            }
        }

        public List<Cargo> GetAllCargo()
        {
            return _context.Cargo.ToList();
        }
    }

}
