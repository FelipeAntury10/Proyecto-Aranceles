using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentaciones;
using lib_presentaciones.Implementaciones;
using lib_presentaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace asp_presentacion.Pages.Ventanas
{
    public class EmpresasModel : PageModel
    {
        private IEmpresasPresentacion? iPresentacion = null;
        private IPaisesPresentacion? iPaisesPresentacion = null; //Se conectara a la tabla paises y lo descargara

        public EmpresasModel(IEmpresasPresentacion iPresentacion, IPaisesPresentacion? iPaisesPresentacion)
        {
            try
            {
                this.iPresentacion = iPresentacion;
                this.iPaisesPresentacion = iPaisesPresentacion; //La presentacion de paises se usa para cargar los paises en el formulario de empresas
                Filtro = new Empresas();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public IFormFile? FormFile { get; set; }
        [BindProperty] public Enumerables.Ventanas Accion { get; set; }
        [BindProperty] public Empresas? Actual { get; set; }
        [BindProperty] public Empresas? Filtro { get; set; }
        [BindProperty] public List<Empresas>? Lista { get; set; }
        [BindProperty] public List<Paises>? ListaPaises { get; set; } // Lista de paises para el formulario de empresas

        public virtual void OnGet() { OnPostBtRefrescar(); }

        public void OnPostBtRefrescar()
        {
            try
            {
                var variable_session = HttpContext.Session.GetString("Usuario");
                if (String.IsNullOrEmpty(variable_session))
                {
                    HttpContext.Response.Redirect("/");
                    return;
                }

                Filtro!.Nombre = Filtro!.Nombre ?? "";

                Accion = Enumerables.Ventanas.Listas;
                
                var task = this.iPresentacion!.PorNombre(Filtro!);
                task.Wait();
                Lista = task.Result;
                Actual = null;
                CargarComboBox(); // Cargamos los paises para el formulario de empresas
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public virtual void OnPostBtNuevo()
        {
            try
            {
                CargarComboBox(); // Cargamos los paises para el formulario de empresas
                Accion = Enumerables.Ventanas.Editar;
                Actual = new Empresas();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public virtual void OnPostBtModificar(string data)
        {
            try
            {
                OnPostBtRefrescar();
                Accion = Enumerables.Ventanas.Editar;
                Actual = Lista!.FirstOrDefault(x => x.Id.ToString() == data);
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public virtual void OnPostBtGuardar()
        {
            try
            {
                CargarComboBox(); // Cargamos los paises para el formulario de empresas
                Accion = Enumerables.Ventanas.Editar;

                Task<Empresas>? task = null;
                if (Actual!.Id == 0)
                {
                    task = this.iPresentacion!.Guardar(Actual!)!;
                    GuardarAuditoria("Gua", "Guardar", "Empresas", Actual.Nombre, IndexModel.UsuarioGlobal, DateTime.Now);

                }
                else
                {
                    task = this.iPresentacion!.Modificar(Actual!)!;
                    GuardarAuditoria("Mod", "Modificar", "Empresas", Actual.Nombre, IndexModel.UsuarioGlobal, DateTime.Now);
                }
                Actual = task.Result;
                Accion = Enumerables.Ventanas.Listas;
                OnPostBtRefrescar();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public virtual void OnPostBtBorrarVal(string data)
        {
            try
            {
                OnPostBtRefrescar();
                Accion = Enumerables.Ventanas.Borrar;
                Actual = Lista!.FirstOrDefault(x => x.Id.ToString() == data);
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public virtual void OnPostBtBorrar()
        {
            try
            {
                var task = this.iPresentacion!.Borrar(Actual!);
                GuardarAuditoria("Bor", "Borrar", "Empresas", Actual.Nombre, IndexModel.UsuarioGlobal, DateTime.Now);
                Actual = task.Result;
                OnPostBtRefrescar();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public void OnPostBtCancelar()
        {
            try
            {
                Accion = Enumerables.Ventanas.Listas;
                OnPostBtRefrescar();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public void OnPostBtCerrar()
        {
            try
            {
                if (Accion == Enumerables.Ventanas.Listas)
                    OnPostBtRefrescar();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }
        public void CargarComboBox()
        {
            try
            {
                var task = this.iPaisesPresentacion!.Listar(); //Listamos
                task.Wait();
                ListaPaises = task.Result; //Se almacena en la propiedad ListaPaises
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }
        //Copiar todo aqui
        public async Task GuardarAuditoria(string Codigo, string Accion, string Entidad, string Informacion, int id_Usuario, DateTime Fecha)
        {
            var Actual = new Auditorias();
            try
            {
                Actual.Cod = Codigo;
                Actual.Accion = Accion;
                Actual.Entidad = Entidad;
                Actual.Informacion = Informacion;
                Actual.Id_Usuario = id_Usuario;
                Actual.Fecha = Fecha;
                await Guardar(Actual);
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }
        private Comunicaciones? comunicaciones = null;
        public async Task<Auditorias?> Guardar(Auditorias? entidad)
        {
            if (entidad!.Id != 0)
            {
                throw new Exception("lbFaltaInformacion");
            }

            var datos = new Dictionary<string, object>();
            datos["Entidad"] = entidad;

            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "Auditorias/Guardar");
            var respuesta = await comunicaciones!.Ejecutar(datos);

            if (respuesta.ContainsKey("Error"))
            {
                throw new Exception(respuesta["Error"].ToString()!);
            }
            entidad = JsonConversor.ConvertirAObjeto<Auditorias>(
                JsonConversor.ConvertirAString(respuesta["Entidad"]));
            return entidad;
        }

    }
}