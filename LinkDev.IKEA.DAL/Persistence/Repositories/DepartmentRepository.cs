using LinkDev.IKEA.DAL.Contracts.Repositories;
using LinkDev.IKEA.DAL.Entities.Departments;
using LinkDev.IKEA.DAL.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Persistence.Repositories
{
    
    internal class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext _DbContext;

        public DepartmentRepository(ApplicationDbContext dbContext)
        {
            _DbContext = dbContext;
        }

        public Department? GetById(int id)
        {
            var Department = _DbContext.Departments.Find(id);
            return Department;
        }
        public IEnumerable<Department> GetAll(bool WithTracking = false)
        {
            if (!WithTracking)
                return _DbContext.Departments.AsNoTracking();
            return _DbContext.Departments;
        }
        public void Add(Department department)
        {
            _DbContext.Departments.Add(department);

            
        }

        public void Update(Department department)
        {
            _DbContext.Departments.Update(department);
            
        }

        public void Delete( int id )
        {
            var department =_DbContext.Departments.Find(id);
            if (department is { })
            {
                _DbContext.Departments.Remove(department);
               
            }
           
        }
    }
}
