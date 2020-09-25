using Microsoft.EntityFrameworkCore;
using Repository.Domain;
using Repository.Mapping;

namespace Repository
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Estado> Estado { get; set; }
        public DbSet<Pais> Paises { get; set; }
        public DbSet<Amigo> Amigos { get; set; }
        public DbSet<Amizade> Amizade { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options){ }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = AmigoAT; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False");
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PaisMap());
            modelBuilder.ApplyConfiguration(new EstadoMap());
            modelBuilder.ApplyConfiguration(new AmigoMap());
            modelBuilder.ApplyConfiguration(new AmizadeMap());
            base.OnModelCreating(modelBuilder);
        }

    }
}
