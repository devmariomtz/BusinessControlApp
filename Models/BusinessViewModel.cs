using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BusinessControlApp.Models.DB;

namespace BusinessControlApp.Models
{
    public class BusinessViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        
        public BusinessViewModel() { }

        // lista de business
        public List<BusinessViewModel> Businesses { get; set; }

    }
}
