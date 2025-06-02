using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    public class ArancelesAplicacion : IArancelesAplicacion
    {
        private IConexion? IConexion = null;

        public ArancelesAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public Aranceles? Borrar(Aranceles? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");

            // Calculos

            this.IConexion!.Aranceles!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Aranceles? Guardar(Aranceles? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad.Id != 0)
                throw new Exception("lbYaSeGuardo");

            // Calculos

            this.IConexion!.Aranceles!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Aranceles> Listar()
        {
            return this.IConexion!.Aranceles!.Take(20).ToList();
        }

        public List<Aranceles> PorCodigo(Aranceles? entidad)
        {
            return this.IConexion!.Aranceles!
                .Where(x => x.Cod!.Contains(entidad!.Cod!))
                .ToList();
        }

        public Aranceles? Modificar(Aranceles? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");

            // Calculos

            var entry = this.IConexion!.Entry<Aranceles>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }
    }
}
