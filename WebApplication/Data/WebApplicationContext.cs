using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Repository.Domain;

namespace WebApplication.Data
{
    public class WebApplicationContext : DbContext
    {
        public WebApplicationContext (DbContextOptions<WebApplicationContext> options)
            : base(options)
        {
        }

        public DbSet<Pais> Pais { get; set; }

        public DbSet<Estado> Estado { get; set; }

        public DbSet<Amigo> Amigo { get; set; }
    }
}
