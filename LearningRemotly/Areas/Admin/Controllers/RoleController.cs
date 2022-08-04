using LearningRemotly.Data;
using LearningRemotly.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningRemotly.Controllers
{
    [Area("Admin")]
    public class RoleController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(ApplicationDbContext context, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
        }

        // GET: RoleController
        public async Task<ActionResult> Index()
        {
            var roles = await _context.Roles.Select(r=> new RoleViewModel() {Id = r.Id, Name =r.Name }).ToListAsync();

            return View(roles);
        }

        // GET: RoleController/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var role = await _context.Roles.FirstOrDefaultAsync(r=>r.Id == id);

            if (role == null)
            {
                return NotFound();
            }

            var roleViewModel = new RoleViewModel()
            {
                Name = role.Name
            };

            return View(roleViewModel);
        }

        // GET: RoleController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RoleController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RoleViewModel roleViewModel)
        {
            try
            {
                if (!string.IsNullOrEmpty(roleViewModel.Name))
                {
                    await _roleManager.CreateAsync(new IdentityRole { Name = roleViewModel.Name });
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RoleController/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Id == id);

            if (role == null)
            {
                return NotFound();
            }

            var roleViewModel = new RoleViewModel()
            {
                Name = role.Name
            };

            return View(roleViewModel);
        }

        // POST: RoleController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(string id, RoleViewModel roleViewModel)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest();
                }

                var existRole = await _context.Roles.FindAsync(id);

                if (existRole == null)
                {
                    return NotFound();
                }

                existRole.Name = roleViewModel.Name;

                _context.Update(existRole);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RoleController/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Id == id);

            if (role == null)
            {
                return NotFound();
            }

            var roleViewModel = new RoleViewModel()
            {
                Name = role.Name
            };

            return View(roleViewModel);

        }

        // POST: RoleController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string id, RoleViewModel roleViewModel)
        {
            try
            {
                var Role = await _context.Roles.FindAsync(id);

                 _context.Roles.Remove(Role);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
