using BusinessControlApp.Models;
using BusinessControlApp.Models.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using AutoMapper;
using System.Numerics;
using System.Drawing.Printing;


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

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,CreationDate,UserId")] Business business)
        {
            //validar que el modelo sea valido
            if (ModelState.IsValid)
            {
                try
                {
                    // actualizar el usuario
                    _context.Update(business);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    // validar que el usuario exista
                    if (!BusinessExists(business.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                // redireccionar a la vista de usuarios
                return RedirectToAction("Business", "Home");
            }
            return View(business);
        }

        private bool BusinessExists(int id)
        {
            return _context.Business.Any(e => e.Id == id);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            // validar que el id no sea nulo
            if (id == null)
            {
                return NotFound();
            }
            // buscar el usuario por el id
            var business = await _context.Business.FindAsync(id);
            // validar que el usuario no sea nulo
            if (business == null)
            {
                return NotFound();
            }
            // retornar la vista con el usuario
            var businessViewModel = _mapper.Map<BusinessViewModel>(business);
            var usuariosDB = await _context.Users.Where(u => u.UserType.Id == 2).ToListAsync();
            // bag de usuarios
            var users = _mapper.Map<List<UserViewModel>>(usuariosDB);
            ViewBag.Users = users.Select(ut => new SelectListItem
            {
                Value = ut.Id.ToString(),
                Text = ut.Names + " " + ut.Lastnames
            }).ToList();
            return View(businessViewModel);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Save(BusinessViewModel _business)
        {
            if (ModelState.IsValid)
            {
                _context.Update(_business);
                await _context.SaveChangesAsync();
                return RedirectToAction("Business", "Home");
            }
            return BadRequest();
        }

        // POST: Business/Create
        [Authorize(Roles="Admin")]
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

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            // usuarios pero de typo user
            var usersDB = await _context.Users.Where(u => u.UserType.Id == 2).ToListAsync();
            var users = _mapper.Map<List<UserViewModel>>(usersDB);
            ViewBag.Users = users.Select(ut => new SelectListItem
            {
                Value = ut.Id.ToString(),
                Text = ut.Names + " " + ut.Lastnames
            }).ToList();
            return View();
        }

        [Authorize(Roles = "Admin")]
        // api para eliminar un business
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                var business = await _context.Business.FindAsync(id);
                _context.Business.Remove(business);
                await _context.SaveChangesAsync();
                return RedirectToAction("Business", "Home");
            }
            catch
            {
                // retornar que no se pudo eliminar el usuario
                return BadRequest();
            }
        }

        // Options for ERROR
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
