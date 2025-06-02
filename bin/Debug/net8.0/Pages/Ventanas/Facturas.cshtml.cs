using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentaciones;
using lib_presentaciones.Implementaciones;
using lib_presentaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OfficeOpenXml;


namespace asp_presentacion.Pages.Ventanas
{
    public class FacturasModel : PageModel
    {
        private IFacturasPresentacion? iPresentacion = null;
        private IArancelesPresentacion? iArancelesPresentacion = null; //Se conectara a la tabla paises y lo descargara


        public FacturasModel(IFacturasPresentacion iPresentacion, IArancelesPresentacion? iArancelesPresentacion)
        {
            try
            {
                this.iPresentacion = iPresentacion;
                this.iArancelesPresentacion = iArancelesPresentacion; //La presentacion de paises se usa para cargar los paises en el formulario de empresas
                Filtro = new Facturas();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public IFormFile? FormFile { get; set; }
        [BindProperty] public Enumerables.Ventanas Accion { get; set; }
        [BindProperty] public Facturas? Actual { get; set; }
        [BindProperty] public Facturas? Filtro { get; set; }
        [BindProperty] public List<Facturas>? Lista { get; set; }
        [BindProperty] public List<Aranceles>? ListaAranceles { get; set; } // Lista de paises para el formulario de empresas

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

                Filtro!.Cod = Filtro!.Cod ?? "";

                Accion = Enumerables.Ventanas.Listas;

                var task = this.iPresentacion!.PorCodigo(Filtro!);
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
                Actual = new Facturas();
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

                Task<Facturas>? task = null;
                if (Actual!.Id == 0)
                {
                    task = this.iPresentacion!.Guardar(Actual!)!;
                    GuardarAuditoria("Gua", "Guardar", "Facturas", Actual.Cod, IndexModel.UsuarioGlobal, DateTime.Now);

                }
                else
                {
                    task = this.iPresentacion!.Modificar(Actual!)!;
                    GuardarAuditoria("Mod", "Modificar", "Facturas", Actual.Cod, IndexModel.UsuarioGlobal, DateTime.Now);
                }
                task.Wait();
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
        public virtual void OnPostBtImprimir(string data)
        {
            try
            {
                CargarComboBox();
                OnPostBtRefrescar();
                string rutaArchivo = @"C:\\Users\\sebas\\OneDrive\\Escritorio\\Aranceles\\Facturas.xlsx";
                string DirectoriPath = Path.GetDirectoryName(rutaArchivo)!;

                if (!Directory.Exists(DirectoriPath)) //Si no existe se crea
                {
                    Directory.CreateDirectory(DirectoriPath);
                }
                ExcelPackage.License.SetNonCommercialOrganization("My Noncommercial organization"); //Para que sepa que es no comercial :)
                using (var Paquete = new ExcelPackage())
                {
                    var HojaExcel = Paquete.Workbook.Worksheets.Add("Facturas");
                    //Columnas
                    HojaExcel.Cells[1, 1].Value = "Codigo";
                    HojaExcel.Cells[1, 2].Value = "PagoTotalEnCop";
                    HojaExcel.Cells[1, 3].Value = "CodigoDelArancel";
                    HojaExcel.Cells[1, 4].Value = "Fecha";
                    //Informacion
                    int i = 0;
                    while (i<Lista.Count)
                    {
                        HojaExcel.Cells[i+2, 1].Value = Lista[i].Cod;
                        HojaExcel.Cells[i+2, 2].Value = Lista[i].PagoTotalCop.ToString();
                        HojaExcel.Cells[i + 2, 3].Value = ListaAranceles[(Lista[i].Id_Arancel.Value)-1].Cod;
                        HojaExcel.Cells[i+2, 4].Value = Lista[i].Fecha.ToString();
                        i++;
                    }
                    FileInfo archivo = new FileInfo(rutaArchivo);
                    Paquete.SaveAs(archivo); //Guardamos el archivo
                }
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
                GuardarAuditoria("Bor", "Borrar", "Facturas", Actual.Cod, IndexModel.UsuarioGlobal, DateTime.Now);

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
                var task = this.iArancelesPresentacion!.Listar(); //Listamos
                task.Wait();
                ListaAranceles = task.Result; //Se almacena en la propiedad ListaPaises
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