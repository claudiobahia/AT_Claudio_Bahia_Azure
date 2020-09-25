using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIPaises.Model;
using WebAPIPaises.Repository.Mapping;

namespace WebAPIPaises.Repository
{
    public class PaisContext : DbContext
    {
        public DbSet<Pais> Paises { set; get; }
        public PaisContext(DbContextOptions<PaisContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PaisMap());
            modelBuilder.ApplyConfiguration(new EstadoMap());
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Estado> Estados { get; set; }
    }
}
