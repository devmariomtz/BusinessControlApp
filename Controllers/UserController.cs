using BusinessControlApp.Models;
using BusinessControlApp.Models.DB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;


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

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: User/Create
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

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: User/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
