using Microsoft.EntityFrameworkCore;
using PracticaMvcCore2ALA.Data;
using PracticaMvcCore2ALA.Models;

namespace PracticaMvcCore2ALA.Repositories
{
    public class LibrosRepository
    {
        private LibrosContext context;


        public LibrosRepository(LibrosContext context)
        {
            this.context = context;
        }

        #region Usuario

        public async Task<Usuario> LogInUsuarioAsync(string email, string password)
        {
            Usuario usuario = await this.context.Usuarios
                .Where(x => x.Email == email && x.Password == password).FirstOrDefaultAsync();
            return usuario;
        }
        #endregion

        #region Libros


        public async Task<List<Libro>> GetLibrosAsync()
        {
           return await this.context.Libros.ToListAsync();
        }


        public async Task <List<Libro>> GetLibrosGeneroAsync(int IdGenero)
        {
            return await this.context.Libros
                .Where(l=>l.IdGenero == IdGenero).ToListAsync();
        }

        public async Task<Libro> FindLibroAsync(int IdLibro)
        {
            return await this.context.Libros
                .Where(l => l.Id == IdLibro).FirstOrDefaultAsync();
        }

        #endregion

        #region Generos

        public async Task<List<Genero>> GetGenerosAsync()
        {
            return await this.context.Generos.ToListAsync();
        }

        #endregion


        #region Pedidos

        public async Task<VistaPedidos> HacerPedidoAsync(List<Libro> libros, int idUsuario)
        {
            foreach (Libro libro in libros)
            {
                Pedido pedido = new Pedido();
                pedido.Id = await GetMaxIdAsync();
                pedido.Fecha = DateTime.Now.Date;
                pedido.Cantidad = 1;
                pedido.IdLibro = libro.Id;
                pedido.IdUsuario = idUsuario;
                pedido.IdFactura = await GetMaxIdFacturaAsync();
                this.context.Pedidos.Add(pedido);
            }
            this.context.SaveChanges();
            VistaPedidos vista = new VistaPedidos();
            return vista;
        }

        public async Task<int> GetMaxIdAsync()
        {
            if(await this.context.Pedidos.CountAsync() == 0)
            {
                return 1;
            }
            else
            {
                return await this.context.Pedidos.MaxAsync(p => p.Id) + 1;
            }
        }

        public async Task<int> GetMaxIdFacturaAsync()
        {
            if (await this.context.Pedidos.CountAsync() == 0)
            {
                return 1;
            }
            else
            {
                return await this.context.Pedidos.MaxAsync(p => p.IdFactura) + 1;
            }
        }


        #endregion
    }
}
