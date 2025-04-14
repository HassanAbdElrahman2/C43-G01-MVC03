using AutoMapper;
using LinkDev.IKEA.BLL.Models.Employees;
using LinkDev.IKEA.DAL.Entities.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.BLL.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() {
            CreateMap<Employee, EmployeeDto>()
                .ForMember(Dest => Dest.EmployeeType,option=>option.MapFrom(Source=>Source.EmployeeType))
                .ForMember(Dest => Dest.Gender,option=>option.MapFrom(Source=>Source.Gender));
                // do than when i have properties not same names in this case not need
            CreateMap<Employee, EmployeeDetailsDto>()
                .ForMember(Dest => Dest.EmployeeType, option => option.MapFrom(Source => Source.EmployeeType))
                .ForMember(Dest => Dest.Gender, option => option.MapFrom(Source => Source.Gender))
                .ForMember(Dest=>Dest.HiringDate,option=>option.MapFrom(Source=>Source.HiringDate))
                .ForMember(Dest=>Dest.Department,option=>option.MapFrom(Source=>Source.Department==null?null: Source.Department.Name)); 
            CreateMap<EmployeeCreateDto, Employee>()
                .ForMember(Dest => Dest.HiringDate, option => option.MapFrom(Source => Source.HiringDate))
                .ForMember(Dest=> Dest.ImageName,option=>option.MapFrom(Source=>Source.Image==null?null:Source.Image.FileName));

            CreateMap<EmployeeUpdateDto, Employee>()
                .ForMember(Dest => Dest.HiringDate, option => option.MapFrom(Source => Source.HiringDate));
        }
    }
}
