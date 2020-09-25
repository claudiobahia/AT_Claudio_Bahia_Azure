using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Repository.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Mapping
{
    public class PaisMap : IEntityTypeConfiguration<Pais>
    {
        public void Configure(EntityTypeBuilder<Pais> builder)
        {
            builder.ToTable("Pais");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Foto).IsRequired();
            builder.Property(x => x.Nome).IsRequired();

            builder.HasMany(x => x.Estados).WithOne(x => x.Pais).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
