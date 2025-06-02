using lib_dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace lib_repositorios.Interfaces
{
    public interface IConexion
    {
        string? StringConexion { get; set; }
        
        DbSet<Paises>? Paises { get; set; }
        DbSet<Empresas>? Empresas { get; set; }
        DbSet<TiposDeAranceles>? TiposDeAranceles { get; set; }
        DbSet<TiposDeProductos>? TiposDeProductos { get; set; }
        DbSet<Productos>? Productos { get; set; }
        DbSet<Ordenes>? Ordenes { get; set; }
        DbSet<Aranceles>? Aranceles { get; set; }
        DbSet<Facturas>? Facturas { get; set; }
        DbSet<Roles>? Roles { get; set; }
        DbSet<Usuarios>? Usuarios { get; set; }
        DbSet<Auditorias>? Auditorias { get; set; }









        EntityEntry<T> Entry<T>(T entity) where T : class;
        int SaveChanges();
    }
}
