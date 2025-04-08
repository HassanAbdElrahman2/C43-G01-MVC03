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
    internal class UintOfWork : IUnitOfWork
    {
        private DepartmentRepository? _DepartmentRepository;

        private EmployeeRepository? _EmployeeRepository;

        private ApplicationDbContext _dbContext;

        public UintOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
           
        }
        public IEmployeeRepository EmployeeRepository
        {
            get { return _EmployeeRepository??new EmployeeRepository(_dbContext); }

        }
        public IDepartmentRepository DepartmentRepository
        {
            get { return _DepartmentRepository ?? new DepartmentRepository(_dbContext); }

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
