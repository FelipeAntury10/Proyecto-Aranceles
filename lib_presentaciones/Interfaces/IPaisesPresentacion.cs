using lib_dominio.Entidades;

namespace lib_presentaciones.Interfaces
{
    public interface IPaisesPresentacion
    {
        Task<List<Paises>> Listar();
        Task<List<Paises>> PorNombre(Paises? entidad);
        Task<Paises?> Guardar(Paises? entidad);
        Task<Paises?> Modificar(Paises? entidad);
        Task<Paises?> Borrar(Paises? entidad);
    }
}