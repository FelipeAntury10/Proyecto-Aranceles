using lib_dominio.Entidades;

namespace lib_presentaciones.Interfaces
{
    public interface IProductosPresentacion
    {
        Task<List<Productos>> Listar();
        Task<List<Productos>> PorNombre(Productos? entidad);
        Task<Productos?> Guardar(Productos? entidad);
        Task<Productos?> Modificar(Productos? entidad);
        Task<Productos?> Borrar(Productos? entidad);
    }
}