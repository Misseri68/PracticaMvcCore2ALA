using Microsoft.AspNetCore.Mvc;
using PracticaMvcCore2ALA.Extensions;
using PracticaMvcCore2ALA.Filters;
using PracticaMvcCore2ALA.Models;
using PracticaMvcCore2ALA.Repositories;

namespace PracticaMvcCore2ALA.Controllers
{
    public class LibrosController : Controller
    {

        private LibrosRepository repo;

        public LibrosController(LibrosRepository repo) {
            this.repo = repo;
        }


        public async Task<IActionResult> Index(int? IdGenero)
        {
            List<Libro> libros = new List<Libro>();
            if (IdGenero.HasValue) {
                libros = await this.repo.GetLibrosGeneroAsync(IdGenero.Value);
            }
            else
            {
                libros = await this.repo.GetLibrosAsync();
            }
            return View(libros);
        }

        public async Task<IActionResult> Details(int IdLibro)
        {
            Libro libro = await this.repo.FindLibroAsync(IdLibro);
            return View(libro);
        }

        public async Task<IActionResult> Carrito()
        {
            if (HttpContext.Session.GetObject<List<Libro>>("CARRITO") != null)
            {
                List<Libro> libros = HttpContext.Session.GetObject<List<Libro>>("CARRITO");
                return View(libros);
            }
            else { return View(); }
        }

        public async Task<IActionResult> AgregarCarrito(int IdLibro)
        {
            List<Libro> libros;
            Libro libro = await this.repo.FindLibroAsync(IdLibro);
            if(HttpContext.Session.GetObject<List<Libro>>("CARRITO") == null)
            {
                libros = new List<Libro>();
                libros.Add(libro);
                HttpContext.Session.SetObject("CARRITO", libros);
            }
            else
            {
                libros = HttpContext.Session.GetObject<List<Libro>>("CARRITO");
                if(libros.Find(l=> l.Id == IdLibro) == null)
                {
                    libros.Add(libro);
                }
                HttpContext.Session.SetObject("CARRITO", libros);
            }
            return RedirectToAction("Index");

        }

        public async Task<IActionResult> QuitarCarrito(int IdLibro)
        {
            List<Libro> libros = HttpContext.Session.GetObject<List<Libro>>("CARRITO");
            int i = libros.FindIndex(l=> l.Id == IdLibro);
            libros.RemoveAt(i);
            HttpContext.Session.SetObject("CARRITO", libros);
            return RedirectToAction("Carrito");
        }

        [AuthorizeUsers]
        public async Task<IActionResult> ConfirmarPedido()
        {
            //No me ha dado tiempo pero aquí le pasaría al metodo del repo HacerPedidoAsync la lista de lbiros y
            //el int id usuario, y luego buscaríac on el id del usuario los registros de la vista y la fecha
            //y lo mostaría en detallesPEdido.
            if (HttpContext.Session.GetObject<List<Libro>>("CARRITO") != null)
            {
                List<Libro> libros = HttpContext.Session.GetObject<List<Libro>>("CARRITO");
               
            }
            return View();
        }
    }
}
