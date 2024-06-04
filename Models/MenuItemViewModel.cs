using BusinessControlApp.Models.DB;

namespace BusinessControlApp.Models
{
    public class MenuItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int CategoryId { get; set; }
        public int BusinessId { get; set; }

        public virtual CategoryViewModel Category { get; set; }
        public virtual BusinessViewModel Business { get; set; }
    }
}
