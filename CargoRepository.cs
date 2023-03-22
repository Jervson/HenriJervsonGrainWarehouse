using HenriJervsonGrainWarehouse;

namespace HenriJervsonGrainWarehouse.Models
{
    using System;
    using HenriJervsonGrainWarehouse.Data;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    public class CargoRepository
    {
        private readonly MyDbContext _context;

        public CargoRepository(DbContextOptions<MyDbContext> options)
        {
            _context = new MyDbContext(options);
        }

        public void AddCargo(string carNumber, double enteringMass)
        {
            var Levcargo = _context.Cargo.Where(x => x.CarNumber == carNumber).Where(y => y.LeavingMass == null).FirstOrDefault();
            if (Levcargo != null)
            {
                Levcargo.LeavingMass = 4;
            }
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
            var cargo = _context.Cargo.Where(x => x.CarNumber == carNumber).Where(y => y.LeavingMass == null).FirstOrDefault();
            if (cargo != null)
            {
                cargo.LeavingMass = leavingMass;
            }
            else
            {
                cargo = new Cargo
                {
                    CarNumber = carNumber,
                    EnteringMass = 4,
                    LeavingMass = leavingMass
                };
                _context.Cargo.Add(cargo);
            }
            _context.SaveChanges();
        }
        public List<Cargo> GetAllCargo()
        {
            return _context.Cargo.ToList();
        }
    }

}