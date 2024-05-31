using DB;
using Microsoft.EntityFrameworkCore;

namespace BusinessControlApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Inyección de dependencias
            builder.Services.AddDbContext<BusinessControlDBContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("BusinessControlAppDBConnection"));
            });

            var app = builder.Build();

            // Agregando el contexto de la base de datos
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<BusinessControlDBContext>();
                
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();


            // Agregar a la App las rutas
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            // Configurar las rutas a nivel superior
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Login}/{id?}");

            // Llamar al método para configurar rutas personalizadas
            Routes.ConfigureRoutes(app);

            app.Run();
        }
    }
}
