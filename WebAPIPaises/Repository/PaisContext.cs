using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIPaises.Domain;
using WebAPIPaises.Repository.Mapping;

namespace WebAPIPaises.Repository
{
    public class PaisContext : DbContext
    {
        public DbSet<Pais> Paises { get; set; }
        public PaisContext(DbContextOptions<PaisContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PaisMap());
            modelBuilder.ApplyConfiguration(new EstadoMap());
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<WebAPIPaises.Domain.Estado> Estado { get; set; }
    }
}
