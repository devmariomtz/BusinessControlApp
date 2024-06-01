using BusinessControlApp.Models.DB;
using BusinessControlApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public IActionResult LogOut()
        {
            return RedirectToAction("Login", "Access");
        }

        [HttpPost]
        public IActionResult Login(UserViewModel _user)
        {
            var user = _context.Users.Where(u => u.Email == _user.Email && u.Password == _user.Password).FirstOrDefault();
            if (user == null)
            {
                return RedirectToAction("Login", "Access");
            }
            // retornar la vista de acuerdo al tipo de usuario
            return RedirectToAction("Business", "Home");
        }

        public async Task<IActionResult> Register()
        {
            var types = await _context.UserTypes.ToListAsync();
            var typesList = _mapper.Map<List<UserTypeViewModel>>(types);
            return View(typesList);
        }
    }
}
