using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_repositorios.Implementaciones
{
    public partial class Conexion : DbContext, IConexion
    {
        public string? StringConexion { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this.StringConexion!, p => { });
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        public DbSet<Paises>? Paises { get; set; }
        public DbSet<Empresas>? Empresas { get; set; }
        public DbSet<TiposDeAranceles>? TiposDeAranceles { get; set; }
        public DbSet<TiposDeProductos>? TiposDeProductos { get; set; }
        public DbSet<Productos>? Productos { get; set; }
        public DbSet<Ordenes>? Ordenes { get; set; }
        public DbSet<Aranceles>? Aranceles { get; set; }
        public DbSet<Facturas>? Facturas { get; set; }
        public DbSet<Roles>? Roles { get; set; }
        public DbSet<Usuarios>? Usuarios { get; set; }
        public DbSet<Auditorias>? Auditorias { get; set; }









    }
}
