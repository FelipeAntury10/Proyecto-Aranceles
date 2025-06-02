using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using mst_presentacion.Nucleo;

namespace mst_presentacion.Repositorios
{
    [TestClass]
    public class DistribuidoresPrueba
    {
        private readonly IConexion? iConexion;
        private List<Distribuidores>? lista;
        private Distribuidores? entidad;

        public DistribuidoresPrueba()
        {
            iConexion = new Conexion();
            iConexion.StringConexion = Configuracion.ObtenerValor("StringConexion");
        }

        [TestMethod]
        public void Ejecutar()
        {
            Assert.AreEqual(true, Guardar());
            Assert.AreEqual(true, Modificar());
            Assert.AreEqual(true, Listar());
            Assert.AreEqual(true, Borrar());
        }

        public bool Listar()
        {
            this.lista = this.iConexion!.Distribuidores!.ToList();
            return lista.Count > 0;
        }

        public bool Guardar()
        {
            this.entidad = EntidadesNucleo.Distribuidores()!;

            this.iConexion!.Distribuidores!.Add(this.entidad);
            this.iConexion!.SaveChanges();

            return true;
        }

        public bool Modificar()
        {
            this.entidad!.Dirreccion = "Test 2";

            var entry = this.iConexion!.Entry<Distribuidores>(this.entidad);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();

            return true;
        }

        public bool Borrar()
        {
            this.iConexion!.Distribuidores!.Remove(this.entidad!);
            this.iConexion!.SaveChanges();
            return true;
        }
    }
}