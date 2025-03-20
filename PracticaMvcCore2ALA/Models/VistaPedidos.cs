using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracticaMvcCore2ALA.Models
{
    [Table("VISTAPEDIDOS")]
    public class VistaPedidos
    {
        [Key]
        [Column("IDVISTAPEDIDOS")]
        public int Id { get; set; }

        [Column("IdUsuario")]
        public int IdUsuario { get; set; }

        [Column("Nombre")]
        public string Nombre { get; set; }

        [Column("Apellidos")]
        public string Apellidos { get; set; }

        [Column("Titulo")]
        public string Titulo { get; set; }

        [Column("Precio")]
        public int Precio;

        [Column("FECHA")]
        public DateTime Fecha { get; set; }

        [Column("PRECIOFINAL")]
        public int PrecioFinal {  get; set; }

    }
}
