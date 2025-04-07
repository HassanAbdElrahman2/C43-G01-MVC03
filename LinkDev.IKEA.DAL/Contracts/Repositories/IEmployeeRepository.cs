using LinkDev.IKEA.DAL.Common.Interfaces;
using LinkDev.IKEA.DAL.Entities.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Contracts.Repositories
{
    public interface IEmployeeRepository :IGenericRepositproy<int,Employee>
    {
    }
}
