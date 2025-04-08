using LinkDev.IKEA.BLL.Models.Employees;
using LinkDev.IKEA.DAL.Contracts;
using LinkDev.IKEA.DAL.Entities.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.BLL.Services.Employees
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeService( IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<EmployeeDto> GatEmployees(bool WithTracking)
        {
            var Employees=_unitOfWork.EmployeeRepository.GetAll(WithTracking);
            foreach (var E in Employees)
            {
                yield return new EmployeeDto(E.Id, E.Name, E.Age, E.Salary, E.IsActive, E.Email, E.Gender.ToString(), E.EmployeeType.ToString());
            }

        }

        public EmployeeDetailsDto? GetEmployeeById(int id)
        {
            var Employee = _unitOfWork.EmployeeRepository.GetById(id);
             return Employee is null? null: new EmployeeDetailsDto
                    (Employee.Id, Employee.Name, Employee.Age, Employee.Address, Employee.Salary, Employee.IsActive, Employee.PhoneNumber,Employee.HiringDate,
                    Employee.Email, Employee.Gender.ToString(), Employee.EmployeeType.ToString(), Employee.CreatedBy, Employee.CreatedOn, Employee.LastModifiedBy, Employee.LastModifiedOn);
        }
        public int CreateEmployee(EmployeeCreateDto employee)
        {
            var E = new Employee()
            {
                Name = employee.Name,
                Salary = employee.Salary,
                Address = employee.Address,
                HiringDate =employee.HiringDate,
                Age = employee.Age,
                CreatedBy = "",
                LastModifiedBy = "",
                Email = employee.Email,
                IsActive = employee.IsActive,
                PhoneNumber = employee.PhoneNumber,
                Gender = employee.Gender,
                EmployeeType = employee.EmployeeType
                
            };

            _unitOfWork.EmployeeRepository.Add(E);
            return _unitOfWork.Complete();
        }

        public bool DeleteEmployee(int id)
        {
            _unitOfWork.EmployeeRepository.Delete(id);
            return _unitOfWork.Complete()>0;
        }



        public int UpdateEmployee(EmployeeUpdateDto employee)
        {
            var Employee = new Employee()
            {
                Id = employee.Id,
                Name = employee.Name,
                Salary = employee.Salary,
                Address = employee.Address,
                HiringDate = employee.HiringDate,
                Age = employee.Age,
                CreatedBy = "",
                LastModifiedBy = "",
                Email = employee.Email,
                IsActive = employee.IsActive,
                PhoneNumber = employee.PhoneNumber,
                Gender = employee.Gender,
                
            };
            _unitOfWork.EmployeeRepository.Update(Employee);
            return _unitOfWork.Complete();
        }
    }
}
