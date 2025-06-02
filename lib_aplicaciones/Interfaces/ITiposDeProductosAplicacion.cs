using lib_dominio.Entidades;

namespace lib_aplicaciones.Interfaces
{
    public interface ITiposDeProductosAplicacion
    {
        void Configurar(string StringConexion);
        List<TiposDeProductos> PorNombre(TiposDeProductos? entidad);
        List<TiposDeProductos> Listar();
        TiposDeProductos? Guardar(TiposDeProductos? entidad);
        TiposDeProductos? Modificar(TiposDeProductos? entidad);
        TiposDeProductos? Borrar(TiposDeProductos? entidad);
    }
}