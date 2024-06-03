using BusinessControlApp.Models.DB;
using BusinessControlApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace BusinessControlApp.Controllers
{
    public class AccessController : Controller
    {
        private readonly BusinessControlDBContext _context;

        private readonly ILogger<HomeController> _logger;

        private readonly IMapper _mapper;

        public AccessController(BusinessControlDBContext context, ILogger<HomeController> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }
        // Options for ACCESS

        public IActionResult Login()
        {
            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Access");
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserViewModel _user)
        {
            var user = _context.Users.Where(u => u.Email == _user.Email && u.Password == _user.Password)
                .Include(u => u.UserType).FirstOrDefault();
            if (user == null)
            {
                return RedirectToAction("Login", "Access");
            }

            // retornar la vista de acuerdo al tipo de usuario
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, user.Names ),
                new("Email", user.Email),
                new("Id", user.Id.ToString()),
                new(ClaimTypes.Role, user.UserType.Type)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            // according to the user type return the view
            return user.UserType.Id switch
            {
                1 => RedirectToAction("Business", "Home"),
                2 => RedirectToAction("BusinessMenuItems", "Home"),
                _ => RedirectToAction("Login", "Access"),
            };
        }

        public async Task<IActionResult> Register()
        {
            var types = await _context.UserTypes.ToListAsync();
            var typesList = _mapper.Map<List<UserTypeViewModel>>(types);
            return View(typesList);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
