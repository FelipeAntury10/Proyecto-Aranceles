using lib_dominio.Entidades;

namespace lib_aplicaciones.Interfaces
{
    public interface IEmpresasAplicacion
    {
        void Configurar(string StringConexion);
        List<Empresas> PorNombre(Empresas? entidad);
        List<Empresas> Listar();
        Empresas? Guardar(Empresas? entidad);
        Empresas? Modificar(Empresas? entidad);
        Empresas? Borrar(Empresas? entidad);
    }
}