﻿using LinkDev.IKEA.DAL.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Contracts
{
    public interface IUnitOfWork
    {
        public IDepartmentRepository  DepartmentRepository { get;  }
        public IEmployeeRepository EmployeeRepository { get; }
        int Complete();
        void Dispose();
    }
}
