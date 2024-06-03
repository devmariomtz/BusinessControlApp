using BusinessControlApp.Models;
using BusinessControlApp.Models.DB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;


namespace BusinessControlApp.Controllers
{
    public class UserController : Controller
    {
        private readonly BusinessControlDBContext _context;
        private readonly IMapper _mapper;

        public UserController(BusinessControlDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        // GET: User/Create
        [Authorize(Roles="Admin")]
        public async Task<ActionResult> Create()
        {
            var userTypesDB = await _context.UserTypes.ToListAsync();
            var userTypes = _mapper.Map<List<UserTypeViewModel>>(userTypesDB);
            ViewBag.UserTypes = userTypes.Select(ut => new SelectListItem
            {
                Value = ut.Id.ToString(),
                Text = ut.Type
            }).ToList();

            return View();
        }

        // POST: User/Create
        [Authorize(Roles="Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User _user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(_user);
                _context.SaveChanges();
                // redireccionar a la vista de usuarios
                return RedirectToAction("Users", "Home");
            }
            // retornar que no se pudo crear el usuario
            return BadRequest();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Names,Lastnames,Email,UserTypeId,Password")] User user)
        {
            //validar que el modelo sea valido
            if (ModelState.IsValid)
            {
                try
                {
                    // actualizar el usuario
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    // validar que el usuario exista
                    if (!UserExists(user.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                // redireccionar a la vista de usuarios
                return RedirectToAction("Users", "Home");
            }
            return View(user);
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var userViewModel = _mapper.Map<UserViewModel>(user);
            var usesTypersDB = await _context.UserTypes.ToListAsync();
            // bag de usuarios
            var types = _mapper.Map<List<UserTypeViewModel>>(usesTypersDB);
            ViewBag.UsersTypes = types.Select(ut => new SelectListItem
            {
                Value = ut.Id.ToString(),
                Text = ut.Type.ToString()
            }).ToList();
            return View(userViewModel);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Save(UserViewModel _user)
        {
            if (ModelState.IsValid)
            {
                _context.Update(_user);
                await _context.SaveChangesAsync();
                return RedirectToAction("Users", "Home");
            }
            return BadRequest();
        }


        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("Users", "Home");
            }
            catch
            {
                // retornar que no se pudo eliminar el usuario
                return BadRequest();
            }
        }

    }
}
