using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessControlApp.Models.DB
{
    public class MenuItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public double Price { get; set; }
        public int CategoryId { get; set; }
        public int BusinessId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual required Category Category { get; set; }
        [ForeignKey("BusinessId")]
        public virtual required Business Business { get; set; }
    }
}
