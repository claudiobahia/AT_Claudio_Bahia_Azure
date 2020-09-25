using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication.Models;

namespace WebApplication.Data
{
    public class WebApplicationContext : DbContext
    {
        public WebApplicationContext (DbContextOptions<WebApplicationContext> options)
            : base(options)
        {
        }

        public DbSet<WebApplication.Models.Pais> Pais { get; set; }

        public DbSet<WebApplication.Models.Estado> Estado { get; set; }

        public DbSet<WebApplication.Models.Amigo> Amigo { get; set; }
    }
}
