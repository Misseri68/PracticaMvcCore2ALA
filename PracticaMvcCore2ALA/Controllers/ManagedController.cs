using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using PracticaMvcCore2ALA.Models;
using PracticaMvcCore2ALA.Repositories;
using System.Security.Claims;

namespace PracticaMvcCore2ALA.Controllers
{
    public class ManagedController : Controller
    {

        private LibrosRepository repo;

        public ManagedController(LibrosRepository repo)
        {
            this.repo = repo;
        }
        public IActionResult Login()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            Usuario user = await this.repo.LogInUsuarioAsync(email, password);
            if (user != null)
            {
                ClaimsIdentity identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme,
                    ClaimTypes.Name, ClaimTypes.Role);
                Claim claimName = new Claim(ClaimTypes.Name, user.Email);
                Claim claimUsername = new Claim("USERNAME", user.Nombre);
                Claim claimApellidos = new Claim("APELLIDOS", user.Apellidos);
                Claim claimFoto = new Claim("FOTO", user.Foto);
                Claim claimID = new Claim(ClaimTypes.NameIdentifier, user.Id.ToString());
                identity.AddClaim(claimName);
                identity.AddClaim(claimUsername);
                identity.AddClaim(claimID);
                identity.AddClaim(claimApellidos);
                identity.AddClaim(claimFoto);
                ClaimsPrincipal userPrincipal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal);
                return RedirectToAction("Perfil", "Usuarios");
            }
            else
            {
                ViewData["MENSAJE"] = "No hay ningun usuario con esas credenciales.";
                return View();
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Libros");
        }
    }
}
