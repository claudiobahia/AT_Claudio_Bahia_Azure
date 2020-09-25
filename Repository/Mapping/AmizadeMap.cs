using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Repository.Domain;

namespace Repository.Mapping
{
    public class AmizadeMap : IEntityTypeConfiguration<Amizade>
    {
        public void Configure(EntityTypeBuilder<Amizade> builder)
        {
            builder.ToTable("Amizade");
            builder.HasKey(x => new { x.PessoaId, x.AmigoId });
            builder.HasOne(x => x.PessoaEamigo)
                .WithMany(x => x.Amizades)
                .HasForeignKey(x => x.PessoaId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.PessoaEamigo)
                .WithMany(x => x.Amizades)
                .HasForeignKey(x => x.AmigoId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
