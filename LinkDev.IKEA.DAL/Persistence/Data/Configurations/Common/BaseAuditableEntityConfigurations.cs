using LinkDev.IKEA.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Persistence.Data.Configurations.Common
{
    internal class BaseAuditableEntityConfigurations<TKey,TEntity> : BaseEntityConfiguration<TKey, TEntity>
        where TKey : IEquatable<TKey>
        where TEntity : BaseAuditableEntity<TKey>
        
    {

        public override void Configure(EntityTypeBuilder<TEntity> builder)
        {
            base.Configure(builder);
            builder.Property(E => E.CreatedBy).HasColumnType("varchar(50)");
            builder.Property(E => E.LastModifiedBy).HasColumnType("varchar(50)");

            builder.Property(E => E.CreatedOn).HasDefaultValueSql("GetUTCDate()"); // save date by UTC may be used my app in different country
            builder.Property(E => E.LastModifiedOn).HasComputedColumnSql("GetUTCDate()"); // to Change with any Modified
        }

    }
}
