using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessControlApp.Models.DB
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Names { get; set; }
        public string Lastnames { get; set; }
        public string Email { get; set; }
        public int UserTypeId { get; set; }
        public string Password { get; set; }

        [ForeignKey("UserTypeId")]
        public virtual UserType UserType { get; set; }
        public virtual ICollection<Business> Business { get; set; }

        // Constructor sin parámetros
        public User() { }

    }
}
