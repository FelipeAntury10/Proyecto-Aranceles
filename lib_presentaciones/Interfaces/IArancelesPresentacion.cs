using lib_dominio.Entidades;

namespace lib_presentaciones.Interfaces
{
    public interface IArancelesPresentacion
    {
        Task<List<Aranceles>> Listar();
        Task<List<Aranceles>> PorCodigo(Aranceles? entidad);
        Task<Aranceles?> Guardar(Aranceles? entidad);
        Task<Aranceles?> Modificar(Aranceles? entidad);
        Task<Aranceles?> Borrar(Aranceles? entidad);
    }
}