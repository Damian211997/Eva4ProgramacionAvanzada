using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;
using GestorProyectos.Models;

public class LoginController : Controller
{
    private readonly DbConnection _context;

    // Constructor que recibe ApplicationDbContext mediante inyección de dependencias
    public LoginController(DbConnection context)
    {
        _context = context;
    }

    // Acción GET que retorna la vista de inicio de sesión
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    // Acción POST que procesa el inicio de sesión
    [HttpPost]
    public IActionResult Index(string username, string password)
    {
        // Verifica si el usuario existe con las credenciales proporcionadas
        var user = _context.Usuarios
            .FirstOrDefault(u => u.Username == username && u.Password == password);

        if (user != null)
        {
           
            // Redirige al Home o cualquier otra página que desees
            return RedirectToAction("Index", "Home");
        }

        // Si las credenciales son incorrectas, muestra un mensaje de error
        ViewBag.Error = "Credenciales incorrectas.";
        return View();
    }

    // Acción para cerrar sesión
    public IActionResult Logout()
    {
        // Limpia la sesión
        HttpContext.Session.Clear();

        // Redirige de nuevo a la página de inicio de sesión
        return RedirectToAction("Index");
    }
}
