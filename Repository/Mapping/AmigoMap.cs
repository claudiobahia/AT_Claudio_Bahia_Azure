using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Repository.Domain;

namespace Repository.Mapping
{
    public class AmigoMap : IEntityTypeConfiguration<Amigo>
    {
        public void Configure(EntityTypeBuilder<Amigo> builder)
        {
            builder.ToTable("Amigo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Foto).IsRequired();
            builder.Property(x => x.Nome).IsRequired();
            builder.Property(x => x.Sobrenome).IsRequired();
            builder.Property(x => x.Email).IsRequired();
            builder.Property(x => x.Telefone).IsRequired();
            builder.Property(x => x.Dataaniversario).IsRequired();
            builder.Property(x => x.PaisId).IsRequired();
            builder.Property(x => x.EstadoId).IsRequired();

            builder.HasOne(x => x.Pais).WithMany().HasForeignKey(x => x.PaisId).OnDelete(DeleteBehavior.NoAction) ;
            builder.HasOne(x => x.Estado).WithMany().HasForeignKey(x => x.EstadoId).OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(x => x.Amizades).WithOne(x => x.PessoaEamigo).HasForeignKey(x => x.PessoaId);
            builder.HasMany(x => x.Amizades).WithOne(x => x.PessoaEamigo).HasForeignKey(x => x.AmigoId);
        }
    }
}
