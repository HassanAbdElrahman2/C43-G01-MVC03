﻿using LinkDev.IKEA.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Persistence.Data.Configurations.Common
{
    public class BaseEntityConfiguration<TKey,TEntity> : IEntityTypeConfiguration<TEntity>
    where TKey: IEquatable<TKey>
    where TEntity : BaseEntity<TKey>
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)

        {
            builder.HasKey(E => E.Id);
        }
    }
}
