using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB
{
    public class ItemMenu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public required string Descripcion { get; set; }
        public double Precio { get; set; }
        public int IdCategoria { get; set; }
        public int IdNegocio { get; set; }

        [ForeignKey("IdCategoria")]
        public virtual required Categoria Categoria { get; set; }
        [ForeignKey("IdNegocio")]
        public virtual required Negocio Negocio { get; set; }
    }
}
