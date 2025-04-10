﻿using LinkDev.IKEA.DAL.Entities;
using LinkDev.IKEA.DAL.Entities.Departments;
using LinkDev.IKEA.DAL.Entities.Employees;
using LinkDev.IKEA.DAL.Persistence.Data.Configurations.Common;
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
    class DepartmentConfigurations : BaseAuditableEntityConfigurations<int,Department>
    {
        public override void Configure(EntityTypeBuilder<Department> builder)
        {
           
            base.Configure(builder);
            builder.Property(D => D.Id).UseIdentityColumn(10, 10);
            builder.Property(D => D.Name).HasColumnType("varchar(100)");
            builder.Property(D => D.Code).HasColumnType("varchar(10)");
            builder.Property(D => D.Description).HasColumnType("varchar(100)");
            builder.HasMany(D => D.Employees).WithOne(E => E.Department)
                .HasForeignKey(E => E.DepartmentId)
                .OnDelete(DeleteBehavior.SetNull);
        }


    }
}
