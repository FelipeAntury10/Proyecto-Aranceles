using lib_dominio.Entidades;

namespace lib_presentaciones.Interfaces
{
    public interface IEmpresasPresentacion
    {
        Task<List<Empresas>> Listar();
        Task<List<Empresas>> PorNombre(Empresas? entidad);
        Task<Empresas?> Guardar(Empresas? entidad);
        Task<Empresas?> Modificar(Empresas? entidad);
        Task<Empresas?> Borrar(Empresas? entidad);
    }
}