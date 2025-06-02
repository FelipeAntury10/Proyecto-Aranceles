using lib_dominio.Entidades;

namespace lib_aplicaciones.Interfaces
{
    public interface IArancelesAplicacion
    {
        void Configurar(string StringConexion);
        List<Aranceles> PorCodigo(Aranceles? entidad);
        List<Aranceles> Listar();
        Aranceles? Guardar(Aranceles? entidad);
        Aranceles? Modificar(Aranceles? entidad);
        Aranceles? Borrar(Aranceles? entidad);
    }
}