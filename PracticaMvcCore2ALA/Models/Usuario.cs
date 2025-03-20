using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracticaMvcCore2ALA.Models
{
    [Table("USUARIOS")]
    public class Usuario
    {
        [Key]
        [Column("IdUsuario")]
        public int Id { get; set; }

        [Column("Nombre")]
        public string Nombre { get; set; }

        [Column("Apellidos")]
        public string Apellidos { get; set; }

        [Column("Email")]
        public string Email { get; set; }

        [Column("Pass")]
        public string Password { get; set; }

        [Column("Foto")]
        public string Foto { get; set; }
    }
}
