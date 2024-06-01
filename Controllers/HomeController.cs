using BusinessControlApp.Models;
using BusinessControlApp.Models.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;


namespace BusinessControlApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        private readonly BusinessControlDBContext _context;

        private readonly ILogger<HomeController> _logger;

        public HomeController(BusinessControlDBContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }
        // Options for ADMIN
        [Authorize(Roles = "Admin")]
        public IActionResult Business()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Users()
        {
            return View();
        }
        // Options for USER
        [Authorize (Roles = "User")]
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
