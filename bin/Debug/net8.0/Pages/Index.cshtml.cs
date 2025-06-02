using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentaciones;
using lib_presentaciones.Implementaciones;
using lib_presentaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace asp_presentacion.Pages
{
    public class IndexModel : PageModel
    {
        public bool EstaLogueado = false;
        public static bool Registrado = false;

        private Comunicaciones? comunicaciones = null;
        public static string RolGlobal { get; set; } = string.Empty;
        public static int UsuarioGlobal { get; set; }=-1; //Borrar si algo
        [BindProperty] public Enumerables.Ventanas Accion { get; set; }
        [BindProperty] public string? Email { get; set; }
        [BindProperty] public string? Contra { get; set; }
        [BindProperty] public string? Email2 { get; set; }
        [BindProperty] public string? Contra2 { get; set; }
        public async Task<List<Usuarios>> CargarUsuarios()
        {
            try
            {
                var lista = new List<Usuarios>();
                var datos = new Dictionary<string, object>();
                comunicaciones = new Comunicaciones();
                datos = comunicaciones.ConstruirUrl(datos, "Usuarios/Listar");
                var respuesta = await comunicaciones!.Ejecutar(datos);
                if (respuesta.ContainsKey("Error"))
                {
                    throw new Exception(respuesta["Error"].ToString()!);
                }
                lista = JsonConversor.ConvertirAObjeto<List<Usuarios>>(
                    JsonConversor.ConvertirAString(respuesta["Entidades"]));
                return lista;
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                return null;
            }
        }
        public async Task<Usuarios?> Guardar(Usuarios? entidad)
        {
            if (entidad!.Id != 0)
            {
                throw new Exception("lbFaltaInformacion");
            }

            var datos = new Dictionary<string, object>();
            datos["Entidad"] = entidad;

            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "Usuarios/Guardar");
            var respuesta = await comunicaciones!.Ejecutar(datos);

            if (respuesta.ContainsKey("Error"))
            {
                throw new Exception(respuesta["Error"].ToString()!);
            }
            entidad = JsonConversor.ConvertirAObjeto<Usuarios>(
                JsonConversor.ConvertirAString(respuesta["Entidad"]));
            return entidad;
        }

        public void OnGet()
        {
            var variable_session = HttpContext.Session.GetString("Usuario");
            if (!String.IsNullOrEmpty(variable_session))
            {
                EstaLogueado = true;
                return;
            }
        }

        public void OnPostBtClean()
        {
            try
            {
                Email = string.Empty;
                Contra = string.Empty;
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }
        public void OnPostBtClean2()
        {
            try
            {
                Email2 = string.Empty;
                Contra2 = string.Empty;
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }
        public async Task OnPostBtEnter2()
        {
            var Actual = new Usuarios();
            try
            {
                Actual.Usuario = Email2;
                Actual.Contraseña = Contra2;
                Actual.Cod = ((Actual.Usuario+"AB").Substring(0, 3).ToUpper() + Actual.Id_Rol + Actual.Id + (4)); //Puede colocar un random, pero aja
                if (Actual.Id_Rol == null || Actual.Id_Rol == 0)
                {
                    Actual.Id_Rol = 2; //Por defecto, asignamos el rol de usuario
                    await Guardar(Actual);
                    Registrado = true; 
                }
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }


        public void OnPostBtEnter()
        {
            try
            {
                List<Usuarios>? lista = CargarUsuarios().Result;
                if (string.IsNullOrEmpty(Email) &&
                    string.IsNullOrEmpty(Contra))
                {
                    OnPostBtClean();
                    return;
                }
                int i = 0;
                while (i < lista.Count)
                {
                    if (lista[i].Usuario == Email &&
                        lista[i].Contraseña == Contra)
                    {
                        ViewData["Logged"] = true;
                        HttpContext.Session.SetString("Usuario", Email!);
                        if (lista[i].Cod.Substring(0,3) == "ADM")
                        { RolGlobal = "1"; }
                        EstaLogueado = true;
                        IndexModel.UsuarioGlobal = lista[i].Id; //Borrar si algo
                        OnPostBtClean();
                        return;
                    }
                    i++;
                }
                /*if ("admin.123" != Email + "." + Contrasena)
                {
                    OnPostBtClean();
                    return;
                }*/
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public void OnPostBtClose()
        {
            try
            {
                HttpContext.Session.Clear();
                HttpContext.Response.Redirect("/");
                EstaLogueado = false;
                IndexModel.RolGlobal = string.Empty;
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }
    }
}