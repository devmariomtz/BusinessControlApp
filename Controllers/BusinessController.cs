using BusinessControlApp.Models;
using BusinessControlApp.Models.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using AutoMapper;


namespace BusinessControlApp.Controllers
{
    [Authorize]
    public class BusinessController : Controller
    {

        private readonly BusinessControlDBContext _context;

        private readonly ILogger<HomeController> _logger;
        private readonly IMapper _mapper;

        public BusinessController(BusinessControlDBContext context, ILogger<HomeController> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }
        // Options for ADMIN
        [Authorize(Roles = "Admin")]
        public IActionResult Business()
        {
            return View();
        }

        // POST: Business/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Business _business)
        {
            if (ModelState.IsValid)
            {
                _context.Add(_business);
                _context.SaveChanges();
                // redireccionar a la vista de usuarios
                return RedirectToAction("Business", "Home");
            }
            // retornar que no se pudo crear el usuario
            return BadRequest();
        }

        public async Task<IActionResult> Create()
        {
            var usersDB = await _context.Users.ToListAsync();
            var users = _mapper.Map<List<UserViewModel>>(usersDB);
            ViewBag.Users = users.Select(ut => new SelectListItem
            {
                Value = ut.Id.ToString(),
                Text = ut.Names + " " + ut.Lastnames
            }).ToList();
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
