using BusinessControlApp.Models;
using BusinessControlApp.Models.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;


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
            return View(businessesList);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Users()
        {
            var userDB = await _context.Users.Include(u => u.UserType).ToListAsync();
            var users = _mapper.Map<List<UserViewModel>>(userDB);
            return View(users);
        }
        // Options for USER
        [Authorize(Roles = "User")]
        public IActionResult BusinessMenuItems()
        {
            return View();
        }

        // Options for ERROR
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
