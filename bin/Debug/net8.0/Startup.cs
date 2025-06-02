using lib_presentaciones.Implementaciones;
using lib_presentaciones.Interfaces;

namespace asp_presentacion
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfiguration? Configuration { set; get; }

        public void ConfigureServices(WebApplicationBuilder builder, IServiceCollection services)
        {
            // Presentaciones
            services.AddScoped<IPaisesPresentacion, PaisesPresentacion>();
            services.AddScoped<IEmpresasPresentacion, EmpresasPresentacion>();
            services.AddScoped<ITiposDeArancelesPresentacion, TiposDeArancelesPresentacion>();
            services.AddScoped<ITiposDeProductosPresentacion, TiposDeProductosPresentacion>();
            services.AddScoped<IProductosPresentacion, ProductosPresentacion>();
            services.AddScoped<IOrdenesPresentacion, OrdenesPresentacion>();
            services.AddScoped<IArancelesPresentacion, ArancelesPresentacion>();
            services.AddScoped<IFacturasPresentacion, FacturasPresentacion>();
            services.AddScoped<IRolesPresentacion, RolesPresentacion>();
            services.AddScoped<IUsuariosPresentacion, UsuariosPresentacion>();
            services.AddScoped<IAuditoriasPresentacion, AuditoriasPresentacion>();








            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddRazorPages();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
            });
        }

        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.MapRazorPages();
            app.UseSession();
            app.Run();
        }
    }
}