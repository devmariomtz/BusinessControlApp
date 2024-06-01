using BusinessControlApp.Models.DB;

namespace BusinessControlApp.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Names { get; set; }
        public string Lastnames { get; set; }
        public string Email { get; set; }
        public int UserTypeId { get; set; }
        public string Password { get; set; }

        public virtual UserTypeViewModel UserType { get; set; }
        public UserViewModel() { }
    }
}
