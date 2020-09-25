using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIAmigos.Model;

namespace WebAPIAmigos.Repository.Mapping
{
    public class AmizadeMap : IEntityTypeConfiguration<Amizade>
    {
        public void Configure(EntityTypeBuilder<Amizade> builder)
        {
            builder.ToTable("Amizade");
            builder.HasKey(x => new { x.PessoaId, x.AmigoId });
            builder.HasOne(x => x.PessoaEamigo).WithMany(x => x.Amizades).HasForeignKey(x => x.PessoaId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.PessoaEamigo).WithMany(x => x.Amizades).HasForeignKey(x => x.AmigoId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
