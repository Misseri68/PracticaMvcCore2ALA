using Microsoft.AspNetCore.Mvc;
using PracticaMvcCore2ALA.Filters;
using PracticaMvcCore2ALA.Models;
using PracticaMvcCore2ALA.Repositories;

namespace PracticaMvcCore2ALA.Controllers
{
    public class UsuariosController : Controller
    {
        private LibrosRepository repo;
        public UsuariosController(LibrosRepository repo) 
        {
            this.repo = repo;
        }


        [AuthorizeUsers]
        public IActionResult Perfil()
        {
            return View();
        }

    }
}
