﻿using LinkDev.IKEA.BLL.Models.Deoartments;
using LinkDev.IKEA.DAL.Entities.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.BLL.Services.Departments
{
  public interface IDepartmentService
    {
        IEnumerable<DepartmentDto> GetDepartments(string? SearchValue);
        DepartmentDetailsDto? GetDepartmentById(int id);
        int CreateDepartment(CreateDepartmentDto department);
        int UpdateDepartment(UpdateDepartmentDto department);
        bool DeleteDepartment(int id);

    }
}
