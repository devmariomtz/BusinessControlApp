using BusinessControlApp.Models;
using BusinessControlApp.Models.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace BusinessControlApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        private readonly BusinessControlDBContext _context;
        private readonly ILogger<HomeController> _logger;
        private readonly IMapper _mapper;

        public HomeController(BusinessControlDBContext context, ILogger<HomeController> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }
        // Options for ADMIN
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Business()
        {
            var businesses = await _context.Business.Include(b => b.User).ToListAsync();
            var businessesList = _mapper.Map<List<BusinessViewModel>>(businesses);
            var businessViewModel = new BusinessViewModel
            {
                Businesses = businessesList
            };
            return View(businessViewModel);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Users()
        {
            // sacar de la sesion el usuario
            var idUser = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Id").Value;
            Console.WriteLine("ID USER: " + idUser);
            // obtener los usuarios menos el usuario que esta logueado
            var userDB = await _context.Users.Where(u => u.Id != int.Parse(idUser)).Include(u => u.UserType).ToListAsync();
            var users = _mapper.Map<List<UserViewModel>>(userDB);
            return View(users);
        }
        // Options for USER
        [Authorize(Roles = "User")]
        public async Task<IActionResult> BusinessMenuItems()
        {
            var menuItemsDb = await _context.MenuItems.Include(m => m.Category).Include(i => i.Business).ToListAsync();
            var menuItems = _mapper.Map<List<MenuItemViewModel>>(menuItemsDb);
            var catogoriesDB = await _context.Categories.ToListAsync();
            var categories = _mapper.Map<List<CategoryViewModel>>(catogoriesDB);
            ViewBag.Categories = categories.Select(ut => new SelectListItem
            {
                Value = ut.Id.ToString(),
                Text = ut.Name
            }).ToList();
            return View(menuItems);
        }

        // Options for ERROR
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
