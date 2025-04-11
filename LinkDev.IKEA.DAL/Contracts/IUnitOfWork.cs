using LinkDev.IKEA.DAL.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Contracts
{
    // Part 09 Unit Of Work[UOW]
    public interface IUnitOfWork
    {
        public IDepartmentRepository  DepartmentRepository { get;  }
        public IEmployeeRepository EmployeeRepository { get; }
        int Complete();
        void Dispose();
    }
}
