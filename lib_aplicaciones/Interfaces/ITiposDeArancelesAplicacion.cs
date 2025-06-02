using lib_dominio.Entidades;

namespace lib_aplicaciones.Interfaces
{
    public interface ITiposDeArancelesAplicacion
    {
        void Configurar(string StringConexion);
        List<TiposDeAranceles> PorNombre(TiposDeAranceles? entidad);
        List<TiposDeAranceles> Listar();
        TiposDeAranceles? Guardar(TiposDeAranceles? entidad);
        TiposDeAranceles? Modificar(TiposDeAranceles? entidad);
        TiposDeAranceles? Borrar(TiposDeAranceles? entidad);
    }
}