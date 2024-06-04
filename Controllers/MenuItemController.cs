using AutoMapper;
using BusinessControlApp.Models;
using BusinessControlApp.Models.DB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BusinessControlApp.Controllers
{
    [Authorize]
    public class MenuItemController : Controller
    {
        private readonly BusinessControlDBContext _context;

        private readonly ILogger<HomeController> _logger;
        private readonly IMapper _mapper;

        public MenuItemController(BusinessControlDBContext context, ILogger<HomeController> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        // POST: Business/Create
        [Authorize(Roles = "User")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MenuItem _item)
        {
            if (ModelState.IsValid)
            {
                _context.Add(_item);
                _context.SaveChanges();
                // redireccionar a la vista de usuarios
                return RedirectToAction("BusinessMenuItems", "Home");
            }
            // retornar que no se pudo crear el usuario
            return BadRequest();
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> Create()
        {
            // usuarios pero de typo user
            var catogoriesDB = await _context.Categories.ToListAsync();
            var categories = _mapper.Map<List<CategoryViewModel>>(catogoriesDB);
            ViewBag.Categories = categories.Select(ut => new SelectListItem
            {
                Value = ut.Id.ToString(),
                Text = ut.Name
            }).ToList();
            var businessesDB = await _context.Business.ToListAsync();
            var businesses = _mapper.Map<List<BusinessViewModel>>(businessesDB);
            ViewBag.Businesses = businesses.Select(ut => new SelectListItem
            {
                Value = ut.Id.ToString(),
                Text = ut.Name
            }).ToList();
            return View();
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Price,CategoryId,BusinessId")] MenuItem item)
        {
            //validar que el modelo sea valido
            if (ModelState.IsValid)
            {
                try
                {
                    // actualizar el usuario
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    // validar que el usuario exista
                    if (!ItemExist(item.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                // redireccionar a la vista de usuarios
                return RedirectToAction("BusinessMenuItems", "Home");
            }
            return View(item);
        }

        private bool ItemExist(int id)
        {
            return _context.MenuItems.Any(e => e.Id == id);
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> Edit(int? id)
        {
            // validar que el id no sea nulo
            if (id == null)
            {
                return NotFound();
            }
            // buscar el usuario por el id
            var item = await _context.MenuItems.FindAsync(id);
            // validar que el usuario no sea nulo
            if (item == null)
            {
                return NotFound();
            }
            // retornar la vista con el usuario
            var itemViewModel = _mapper.Map<MenuItemViewModel>(item);
            var catogoriesDB = await _context.Categories.ToListAsync();
            var categories = _mapper.Map<List<CategoryViewModel>>(catogoriesDB);
            ViewBag.Categories = categories.Select(ut => new SelectListItem
            {
                Value = ut.Id.ToString(),
                Text = ut.Name
            }).ToList();
            var businessesDB = await _context.Business.ToListAsync();
            var businesses = _mapper.Map<List<BusinessViewModel>>(businessesDB);
            ViewBag.Businesses = businesses.Select(ut => new SelectListItem
            {
                Value = ut.Id.ToString(),
                Text = ut.Name
            }).ToList();
            return View(itemViewModel);
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> Save(MenuItemController _item)
        {
            if (ModelState.IsValid)
            {
                _context.Update(_item);
                await _context.SaveChangesAsync();
                return RedirectToAction("BusinessMenuItems", "Home");
            }
            return BadRequest();
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                var item = await _context.MenuItems.FindAsync(id);
                _context.MenuItems.Remove(item);
                await _context.SaveChangesAsync();
                return RedirectToAction("BusinessMenuItems", "Home");
            }
            catch
            {
                // retornar que no se pudo eliminar el usuario
                return BadRequest();
            }
        }
    }
}
