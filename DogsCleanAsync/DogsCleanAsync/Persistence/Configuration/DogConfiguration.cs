using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Dogs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration
{
    internal class DogConfiguration : IEntityTypeConfiguration<Dog>
    {
        public void Configure(EntityTypeBuilder<Dog> builder)
        {
            builder.HasKey(d => d.Id);

            builder.Property(d => d.Id).HasConversion(
                dogId =>  dogId.Value,
                value => new DogId(value));

            builder.Property(d => d.Name).HasMaxLength(100);

            builder.Property(d => d.Color).HasMaxLength(255);

            builder.HasIndex(d => d.Name).IsUnique();

        }
    }
}
