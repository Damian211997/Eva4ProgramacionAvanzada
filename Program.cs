using Microsoft.EntityFrameworkCore;
using GestorProyectos.Models;

var builder = WebApplication.CreateBuilder(args);

// Configurar el DbContext con la cadena de conexi�n
builder.Services.AddDbContext<DbConnection>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Agregar servicios para controladores y vistas
builder.Services.AddControllersWithViews();

// Configurar sesiones en la aplicaci�n
builder.Services.AddSession(options =>
{
    // Configura el tiempo de expiraci�n de la sesi�n, por ejemplo, 30 minutos
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});

var app = builder.Build();

// Configurar el pipeline de solicitudes HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // El valor predeterminado de HSTS es 30 d�as. Puedes cambiar esto para escenarios de producci�n, consulta https://aka.ms/aspnetcore-hsts.
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
