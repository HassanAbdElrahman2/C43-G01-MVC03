using LinkDev.IKEA.DAL.Common.Enums;
using LinkDev.IKEA.DAL.Entities;
using LinkDev.IKEA.DAL.Entities.Employees;
using LinkDev.IKEA.DAL.Entities.Employees.Enums;
using LinkDev.IKEA.DAL.Persistence.Data.Configurations.Common;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Persistence.Data.Configurations.Employees
{
    public class EmployeeConfigurations :BaseAuditableEntityConfigurations<int,Employee>
    {
        public override void Configure(EntityTypeBuilder<Employee> builder)
        {
            base.Configure(builder);
           
            builder.Property(E => E.Name).HasColumnType("varchar(50)").IsRequired();
            builder.Property(E => E.Address).HasColumnType("varchar(150)");
            builder.Property(E => E.Salary).HasColumnType("decimal(10,2)");
            builder.Property(E => E.Gender).HasConversion
                ((Gender) => Gender.ToString(), (gender) => (Gender)Enum.Parse(typeof(Gender), gender));
            builder.Property(E => E.EmployeeType).HasConversion
                (Gender => Gender.ToString(), gender => (EmployeeType)Enum.Parse(typeof(EmployeeType), gender));
        }
    }

}
