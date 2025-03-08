using LinkDev.IKEA.DAL.Entities.Departments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Persistence.Data.Configurations.Departments
{
    class DepartmentConfigurations : IEntityTypeConfiguration<Department>
    {
   

        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property(D => D.Id).UseIdentityColumn(10, 10);
            builder.Property(D => D.Name).HasColumnType("varchar(100)");
            builder.Property(D => D.Code).HasColumnType("varchar(10)");
            builder.Property(D => D.Description).HasColumnType("varchar(100)");

            builder.Property(E => E.CreatedBy).HasColumnType("varchar(50)");
            builder.Property(E => E.LastModifiedBy).HasColumnType("varchar(50)");

            builder.Property(E => E.CreatedOn).HasDefaultValueSql("GetUTCDate()"); // save date by UTC may be used my app in different country
            builder.Property(E => E.LastModifiedOn).HasComputedColumnSql("GetUTCDate()"); // to Change with any Modified
        }
    }
}
