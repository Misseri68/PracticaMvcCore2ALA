using Microsoft.AspNetCore.Mvc;
using PracticaMvcCore2ALA.Models;
using PracticaMvcCore2ALA.Repositories;

namespace PracticaMvcCore2ALA.ViewComponents
{
    public class MenuGenerosViewComponent : ViewComponent
    {

        private LibrosRepository repo;

        public MenuGenerosViewComponent(LibrosRepository repo)
        {
            this.repo = repo;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Genero> generos = await this.repo.GetGenerosAsync();
            return View(generos);
        }
    }
}
