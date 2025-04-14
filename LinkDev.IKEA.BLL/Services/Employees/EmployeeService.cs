using AutoMapper;
using LinkDev.IKEA.BLL.Models.Employees;
using LinkDev.IKEA.BLL.Services.AttschementService;
using LinkDev.IKEA.DAL.Contracts;
using LinkDev.IKEA.DAL.Entities.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.BLL.Services.Employees
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAttachmenetService _attachmenet;

        public EmployeeService( IUnitOfWork unitOfWork, IMapper mapper,IAttachmenetService attachmenet)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _attachmenet = attachmenet;
        }
        public IEnumerable<EmployeeDto> GatEmployees(bool WithTracking)
        {
            var Employees=_unitOfWork.EmployeeRepository
                .GetAll(E=> new EmployeeDto(E.Id, E.Name,
                E.Age, E.Salary, E.IsActive,
                E.Email, E.Gender.ToString(),
                E.EmployeeType.ToString(),
                E.Department==null?null: E.Department.Name));
            foreach (var E in Employees)
            {
                // yield return new EmployeeDto(E.Id, E.Name, E.Age, E.Salary, E.IsActive, E.Email, E.Gender.ToString(), E.EmployeeType.ToString());
                // yield return _mapper.Map<Employee, EmployeeDto>(E);
                yield return E;

            }

        }
        public EmployeeDetailsDto? GetEmployeeById(int id)
        {
            var Employee = _unitOfWork.EmployeeRepository.GetById(id);
            return Employee is null ? null : new EmployeeDetailsDto
                   (Employee.Id, Employee.Name,
                   Employee.Age, Employee.Address,
                   Employee.Salary, Employee.IsActive,
                   Employee.PhoneNumber, Employee.HiringDate,
                   Employee.Email, Employee.Gender.ToString(),
                   Employee.EmployeeType.ToString(),
                   Employee.CreatedBy, Employee.CreatedOn,
                   Employee.LastModifiedBy,
                   Employee.LastModifiedOn,
                   Employee.DepartmentId,
                   Employee.Department?.Name);

            // return Employee is null ? null : _mapper.Map<Employee, EmployeeDetailsDto>(Employee);
        }
        public int CreateEmployee(EmployeeCreateDto employee)
        {
            //var E = new Employee()
            //{
            //    Name = employee.Name,
            //    Salary = employee.Salary,
            //    Address = employee.Address,
            //    HiringDate =employee.HiringDate,
            //    Age = employee.Age,
            //    CreatedBy = "",
            //    LastModifiedBy = "",
            //    Email = employee.Email,
            //    IsActive = employee.IsActive,
            //    PhoneNumber = employee.PhoneNumber,
            //    Gender = employee.Gender,
            //    EmployeeType = employee.EmployeeType

            //};
            var Employee = _mapper.Map<EmployeeCreateDto, Employee>(employee);
            if(employee.Image is not null)
                Employee.ImageName= _attachmenet.Upload(employee.Image, "Images");
           // may be work with one or more repository before save changes to save all in one time or any problem donot save any operation
          // this is advantages of UnitOfWork
           
            _unitOfWork.EmployeeRepository.Add(Employee);
            return _unitOfWork.Complete();
        }
        public bool DeleteEmployee(int id)
        {
            _unitOfWork.EmployeeRepository.Delete(id);
            return _unitOfWork.Complete()>0;
        }
        public int UpdateEmployee(EmployeeUpdateDto employee)
        {
            //var Employee = new Employee()
            //{
            //    Id = employee.Id,
            //    Name = employee.Name,
            //    Salary = employee.Salary,
            //    Address = employee.Address,
            //    HiringDate = employee.HiringDate,
            //    Age = employee.Age,
            //    CreatedBy = "",
            //    LastModifiedBy = "",
            //    Email = employee.Email,
            //    IsActive = employee.IsActive,
            //    PhoneNumber = employee.PhoneNumber,
            //    Gender = employee.Gender,
                
            //};
            _unitOfWork.EmployeeRepository.Update(_mapper.Map<EmployeeUpdateDto,Employee>(employee));
            return _unitOfWork.Complete();
        }

        public IEnumerable<EmployeeDto> GatEmployees(string? SearchValue)
        {
            IEnumerable<Employee> employees;
            if (!String.IsNullOrWhiteSpace(SearchValue))
            {
               var Employees= _unitOfWork.EmployeeRepository.GetAll(E => E.Name.ToLower()
                    .Contains(SearchValue.ToLower())).Select(E=> new EmployeeDto(E.Id, E.Name,
              E.Age, E.Salary, E.IsActive,
              E.Email, E.Gender.ToString(),
              E.EmployeeType.ToString(),
              E.Department == null ? null : E.Department.Name));
                foreach (var E in Employees)
                {
                    yield return E;
                }
            }
            else
            {
                var Employees = _unitOfWork.EmployeeRepository
              .GetAll(E => new EmployeeDto(E.Id, E.Name,
              E.Age, E.Salary, E.IsActive,
              E.Email, E.Gender.ToString(),
              E.EmployeeType.ToString(),
              E.Department == null ? null : E.Department.Name));
                foreach (var E in Employees)
                {   
                    yield return E;
                }
            }
        }
    }
}
