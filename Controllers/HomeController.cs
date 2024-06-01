using BusinessControlApp.Models;
using BusinessControlApp.Models.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BusinessControlApp.Controllers
{
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
        public IActionResult Business()
        {
            return View();
        }
        public IActionResult Users()
        {
            return View();
        }
        // Options for USER
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
