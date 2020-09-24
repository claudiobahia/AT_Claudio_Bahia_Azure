using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIAmigos.Domain;
using WebAPIAmigos.Repository.Mapping;

namespace WebAPIAmigos.Repository
{
    public class AmigoContext : DbContext
    {
        public DbSet<Amigo> Amigos { get; set; }
        public AmigoContext(DbContextOptions<AmigoContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AmigoMap());
            modelBuilder.ApplyConfiguration(new AmizadeMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
