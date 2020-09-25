using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Repository.Domain;


namespace Repository.Mapping
{
    public class EstadoMap : IEntityTypeConfiguration<Estado>
    {
        public void Configure(EntityTypeBuilder<Estado> builder)
        {
            builder.ToTable("Estado");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Foto).IsRequired();
            builder.Property(x => x.Nome).IsRequired();
            builder.Property(x => x.PaisId).IsRequired();
            builder.HasOne(x => x.Pais);
        }
    }
}
