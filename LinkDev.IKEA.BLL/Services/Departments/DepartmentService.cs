using LinkDev.IKEA.BLL.Models.Deoartments;
using LinkDev.IKEA.DAL.Contracts;
using LinkDev.IKEA.DAL.Entities.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.BLL.Services.Departments
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public int CreateDepartment(CreateDepartmentDto department)
        {
            // may be work with one or more repository before save changes to save all in one time or any problem donot save any operation
            // this is advantages of UnitOfWork
            var depertmentToCreate = new Department() 
           { Code = department.Code, Name = department.Name,Description=department.Description,
               CreationDate=department.CreationDate,CreatedBy="",LastModifiedBy="" };
            _unitOfWork.DepartmentRepository.Add(depertmentToCreate);
            return _unitOfWork.Complete();
        }

        public bool DeleteDepartment(int id)
        {
            _unitOfWork.DepartmentRepository.Delete(id);
            var deleted = _unitOfWork.Complete() > 0;
            return deleted;
        }

        public DepartmentDetailsDto? GetDepartmentById(int id)
        {

            var department = _unitOfWork.DepartmentRepository.GetById(id);
            {
                if (department is null)
                    return null;
                    return new DepartmentDetailsDto(department.Id, department.CreatedBy, department.CreatedOn, department.LastModifiedBy, department.LastModifiedOn, department.Name, department.Code, department.Description, department.CreationDate)
                    ;
            }
        }

        public IEnumerable<DepartmentDto> GetDepartments(string? SearchValue)
        {
            if (!String.IsNullOrWhiteSpace(SearchValue))
            {
                var departments = _unitOfWork.DepartmentRepository.GetAll(D => D.Name.ToLower().Contains(SearchValue.ToLower()));
                foreach (var item in departments)
                {
                    yield return new DepartmentDto(item.Name, item.Code, item.Id, item.CreationDate);

                } 

            }
            else
            {
                var departments = _unitOfWork.DepartmentRepository.GetAll();
                foreach (var item in departments)
                {
                    yield return new DepartmentDto(item.Name, item.Code, item.Id, item.CreationDate);

                }

            }
        }

        public int UpdateDepartment(UpdateDepartmentDto department)
        {
            var depertmentToUpdate = new Department()
            {
                Id=department.Id,
                Code = department.Code,
                Name = department.Name,
                Description = department.Description,
                CreationDate = department.CreationDate,
                CreatedBy = "",
                LastModifiedBy = ""
            };
            _unitOfWork.DepartmentRepository.Update(depertmentToUpdate);
            return _unitOfWork.Complete();
        }
    }
}
