using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    public class TiposDeArancelesAplicacion : ITiposDeArancelesAplicacion
    {
        private IConexion? IConexion = null;

        public TiposDeArancelesAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public TiposDeAranceles? Borrar(TiposDeAranceles? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");

            // Calculos

            this.IConexion!.TiposDeAranceles!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public TiposDeAranceles? Guardar(TiposDeAranceles? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad.Id != 0)
                throw new Exception("lbYaSeGuardo");

            // Calculos

            this.IConexion!.TiposDeAranceles!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<TiposDeAranceles> Listar()
        {
            return this.IConexion!.TiposDeAranceles!.Take(20).ToList();
        }

        public List<TiposDeAranceles> PorNombre(TiposDeAranceles? entidad)
        {
            return this.IConexion!.TiposDeAranceles!
                .Where(x => x.Nombre!.Contains(entidad!.Nombre!))
                .ToList();
        }

        public TiposDeAranceles? Modificar(TiposDeAranceles? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");

            // Calculos

            var entry = this.IConexion!.Entry<TiposDeAranceles>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }
    }
}
