using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    public class TiposDeProductosAplicacion : ITiposDeProductosAplicacion
    {
        private IConexion? IConexion = null;

        public TiposDeProductosAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public TiposDeProductos? Borrar(TiposDeProductos? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");

            // Calculos

            this.IConexion!.TiposDeProductos!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public TiposDeProductos? Guardar(TiposDeProductos? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad.Id != 0)
                throw new Exception("lbYaSeGuardo");

            // Calculos

            this.IConexion!.TiposDeProductos!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<TiposDeProductos> Listar()
        {
            return this.IConexion!.TiposDeProductos!.Take(20).ToList();
        }

        public List<TiposDeProductos> PorNombre(TiposDeProductos? entidad)
        {
            return this.IConexion!.TiposDeProductos!
                .Where(x => x.Nombre!.Contains(entidad!.Nombre!))
                .ToList();
        }

        public TiposDeProductos? Modificar(TiposDeProductos? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");

            // Calculos

            var entry = this.IConexion!.Entry<TiposDeProductos>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }
    }
}
