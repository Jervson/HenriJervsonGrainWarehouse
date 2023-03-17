using HenriJervsonGrainWarehouse;

namespace HenriJervsonGrainWarehouse.Models
{
    using System;
    using HenriJervsonGrainWarehouse.Data;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;

    public class CargoRepository
    {
        private readonly MyDbContext _context;

        public CargoRepository(DbContextOptions<MyDbContext> options)
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
        public void Warehouse(string carNumber, double enteringMass, double leavingMass)
        {
            var cargo = new Cargo
            {
                CarNumber = carNumber,
                EnteringMass = enteringMass,
                LeavingMass = leavingMass
            };
            _context.Cargo.Add(cargo);
            _context.SaveChanges();
        }

        public void UpdateCargoLeavingMass(string carNumber, double leavingMass)
        {
            var cargo = _context.Cargo.Find(carNumber);
            cargo.LeavingMass = leavingMass;
            _context.SaveChanges();
        }
        public List<Cargo> GetAllCargo()
        {
            return _context.Cargo.ToList();
        }
    }

}
