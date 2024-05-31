using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB
{
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public required string Nombres { get; set; }
        public required string Apellidos { get; set; }
        public int IdTipoUsuario { get; set; }
        public required string Contrasena { get; set; }

        [ForeignKey("IdTipoUsuario")]
        public virtual required TipoUsuario TipoUsuario { get; set; }

        public virtual ICollection<Negocio>? Negocios { get; set; }
    }
}
