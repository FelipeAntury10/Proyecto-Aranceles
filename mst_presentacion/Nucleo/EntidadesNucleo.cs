using lib_dominio.Entidades;

namespace mst_presentacion.Nucleo
{
    public class EntidadesNucleo
    {
        public static Distribuidores? Distribuidores()
        {
            var entidad = new Distribuidores();
            entidad.Codigo = "Pruebas-" + DateTime.Now.ToString("yyyyMMddhhmmss");
            entidad.Nombre = "Pruebas";
            entidad.Dirreccion = "Pruebas";
            entidad.Telefono = "Pruebas";
            return entidad;
        }

        public static Productos? Productos()
        {
            var entidad = new Productos();
            entidad.Codigo = "Pruebas-" + DateTime.Now.ToString("yyyyMMddhhmmss");
            entidad.Nombre = "Pruebas";
            entidad.Lote = "Pruebas";
            entidad.Cantidad = 20;
            entidad.Precio = 100000.0m;
            entidad.Distribuidor = 1;
            return entidad;
        }
    }
}
