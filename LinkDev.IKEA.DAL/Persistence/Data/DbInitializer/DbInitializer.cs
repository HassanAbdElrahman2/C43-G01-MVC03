using LinkDev.IKEA.DAL.Contracts;
using LinkDev.IKEA.DAL.Entities.Departments;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Persistence.Data.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _context;
        public DbInitializer(ApplicationDbContext dbContext)
        {
            _context = dbContext;  
        }
        public void Initialize()
        {
            if(_context.Database.GetPendingMigrations().Any())
                _context.Database.Migrate();
        }

        public void Seed()
        {
            if (!_context.Departments.Any())
            {
                var DepartmentsData = File.ReadAllText("../LinkDev.IKEA.DAL/Persistence/Data/Seeds/departments.json");
                var Departments = JsonSerializer.Deserialize<List<Department>>(DepartmentsData);
               
                if (Departments?.Count>0 )
                {

                    _context.Departments.AddRange(Departments);
                    _context.SaveChanges();
                }
            }
        }
    }
}
