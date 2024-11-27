using Microsoft.EntityFrameworkCore;
using GestorProyectos.Models;

var builder = WebApplication.CreateBuilder(args);

// Configurar el DbContext con la cadena de conexión
builder.Services.AddDbContext<DbConnection>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Agregar servicios para controladores y vistas
builder.Services.AddControllersWithViews();

// Configurar sesiones en la aplicación
builder.Services.AddSession(options =>
{
    // Configura el tiempo de expiración de la sesión, por ejemplo, 30 minutos
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});

var app = builder.Build();

// Configurar el pipeline de solicitudes HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // El valor predeterminado de HSTS es 30 días. Puedes cambiar esto para escenarios de producción, consulta https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Habilitar el uso de sesiones
app.UseSession();

app.UseAuthorization();

// Cambiar la ruta predeterminada para que apunte al controlador Login
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
