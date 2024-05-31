using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DB
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public required string Names { get; set; }
        public required string Lastnames { get; set; }
        public required string Email { get; set; }
        public int UserTypeId { get; set; }
        public required string Password { get; set; }

        [ForeignKey("UserTypeId")]
        public virtual UserType? UserType { get; set; }

        public virtual ICollection<Business>? Business { get; set; }
    }
}
