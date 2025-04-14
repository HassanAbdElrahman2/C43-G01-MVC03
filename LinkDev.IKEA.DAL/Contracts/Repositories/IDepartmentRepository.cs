using LinkDev.IKEA.DAL.Common.Interfaces;
using LinkDev.IKEA.DAL.Entities.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Contracts.Repositories
{
    public interface IDepartmentRepository : IGenericRepositproy<int,Department>
    {
      
    }
}
