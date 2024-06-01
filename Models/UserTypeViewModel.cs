using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessControlApp.Models
{
    public class UserTypeViewModel
    {
        
        public int Id { get; set; }
        public required string Type { get; set; }
    }
}
