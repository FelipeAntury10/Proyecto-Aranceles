using lib_dominio.Entidades;

namespace lib_presentaciones.Interfaces
{
    public interface ITiposDeArancelesPresentacion
    {
        Task<List<TiposDeAranceles>> Listar();
        Task<List<TiposDeAranceles>> PorNombre(TiposDeAranceles? entidad);
        Task<TiposDeAranceles?> Guardar(TiposDeAranceles? entidad);
        Task<TiposDeAranceles?> Modificar(TiposDeAranceles? entidad);
        Task<TiposDeAranceles?> Borrar(TiposDeAranceles? entidad);
    }
}