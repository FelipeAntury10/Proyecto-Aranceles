using lib_dominio.Entidades;

namespace lib_presentaciones.Interfaces
{
    public interface ITiposDeProductosPresentacion
    {
        Task<List<TiposDeProductos>> Listar();
        Task<List<TiposDeProductos>> PorNombre(TiposDeProductos? entidad);
        Task<TiposDeProductos?> Guardar(TiposDeProductos? entidad);
        Task<TiposDeProductos?> Modificar(TiposDeProductos? entidad);
        Task<TiposDeProductos?> Borrar(TiposDeProductos? entidad);
    }
}