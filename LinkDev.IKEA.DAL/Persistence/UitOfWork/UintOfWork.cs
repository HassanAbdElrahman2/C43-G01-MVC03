using LinkDev.IKEA.DAL.Contracts;
using LinkDev.IKEA.DAL.Contracts.Repositories;
using LinkDev.IKEA.DAL.Persistence.Data;
using LinkDev.IKEA.DAL.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Persistence.UitOfWork
{
    // Part 09 Unit Of Work [UOW]
    internal class UintOfWork : IUnitOfWork
    {
        private readonly Lazy<IDepartmentRepository> _DepartmentRepository;

        private readonly Lazy<IEmployeeRepository> _EmployeeRepository;

        private ApplicationDbContext _dbContext;

        public UintOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _DepartmentRepository = new Lazy<IDepartmentRepository>(() => new DepartmentRepository(dbContext));
            _EmployeeRepository = new Lazy<IEmployeeRepository>(() => new EmployeeRepository(dbContext));
           
        }
        public IEmployeeRepository EmployeeRepository
        {
            get { return _EmployeeRepository.Value; }

        }
        public IDepartmentRepository DepartmentRepository
        {
            get { return _DepartmentRepository.Value; }

        }

        public int Complete()
        {
            return _dbContext.SaveChanges();
        }

        public void Dispose()
        {
           _dbContext.Dispose();
        }
    }
}
