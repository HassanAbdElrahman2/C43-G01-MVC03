using LinkDev.IKEA.DAL.Contracts;
using LinkDev.IKEA.DAL.Contracts.Repositories;
using LinkDev.IKEA.DAL.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Persistence.UitOfWork
{
    internal class UintOfWork : IUnitOfWork
    {
        public IDepartmentRepository DepartmentRepository { get; set ; }

        private ApplicationDbContext _dbContext;

        public UintOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
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
