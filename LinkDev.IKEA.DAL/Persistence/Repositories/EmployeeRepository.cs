using LinkDev.IKEA.DAL.Common.Entities;
using LinkDev.IKEA.DAL.Contracts.Repositories;
using LinkDev.IKEA.DAL.Entities.Employees;
using LinkDev.IKEA.DAL.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Persistence.Repositories
{
    public class EmployeeRepository : GenericRepositproy<int,Employee>,IEmployeeRepository
    {
        private readonly ApplicationDbContext dbContext;

        public EmployeeRepository( ApplicationDbContext _dbContext):base(_dbContext)
        {
            dbContext = _dbContext;
        }
    }
}
