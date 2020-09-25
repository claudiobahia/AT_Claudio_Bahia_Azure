using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIAmigos.Model;

namespace WebAPIAmigos.Repository.Mapping
{
    public class AmigoMap : IEntityTypeConfiguration<Amigo>
    {
        public void Configure(EntityTypeBuilder<Amigo> builder)
        {
            builder.ToTable("Amigo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Nome).IsRequired();
            builder.Property(x => x.Sobrenome).IsRequired();
            builder.Property(x => x.Foto).IsRequired();
            builder.Property(x => x.Telefone).IsRequired();
            builder.Property(x => x.PaisId).IsRequired();
            builder.Property(x => x.EstadoId).IsRequired();

            builder.HasOne(x => x.Estado);
            builder.HasOne(x => x.Pais);

            builder.HasMany(x => x.Amizades).WithOne(x => x.PessoaEamigo).HasForeignKey(x => x.PessoaId);
            builder.HasMany(x => x.Amizades).WithOne(x => x.PessoaEamigo).HasForeignKey(x => x.AmigoId);
        }
    }
}
