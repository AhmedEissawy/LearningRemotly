using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LearningRemotly.Data;
using LearningRemotly.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace LearningRemotly.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CourseController : Controller
    {

        private readonly ApplicationDbContext _context;

        private readonly IHostingEnvironment _host;

        public CourseController(ApplicationDbContext context, IHostingEnvironment host)
        {
            _context = context;
            _host = host;
        }

        // GET: Admin/Course
        public async Task<IActionResult> Index()
        {
            var courses = await _context.Courses.Include(c => c.Category).Include(c=>c.Teacher).ToListAsync();

            return View(courses);
        }

        // GET: Admin/Course/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.Category)
                .Include(c => c.Teacher)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Admin/Course/Create
        public IActionResult Create()
        {

            ViewBag.category = _context.Categories.ToList();
            ViewBag.teacher = _context.Teachers.ToList();

            return View();
        }

        // POST: Admin/Course/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Course course)
        {
            if (ModelState.IsValid)
            {
                 UploadPhoto(course);
                 course.Creation_Date = DateTime.Now;
                _context.Courses.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.category = _context.Categories.ToList();
            ViewBag.teacher = _context.Teachers.ToList();
            return View(course);
        }

        // GET: Admin/Course/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.Category)
                .Include(c => c.Teacher)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (course == null)
            {
                return NotFound();
            }
            ViewBag.category = _context.Categories.ToList();
            ViewBag.teacher = _context.Teachers.ToList();
            return View(course);
        }

        // POST: Admin/Course/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Course course)
        {
            if (id != course.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    course.Creation_Date = DateTime.Now;
                     UploadPhoto(course);
                    _context.Courses.Update(course);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.category = _context.Categories.ToList();
            ViewBag.teacher = _context.Teachers.ToList();
            return View(course);
        }

        // GET: Admin/Course/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.Category)
                .Include(c => c.Teacher)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Admin/Course/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.Id == id);
        }

        void UploadPhoto(Course model)
        {
            if (model.File != null)
            {
                string uploadFolder = Path.Combine(_host.WebRootPath, "Images/Courses");
                string uniqueFileName = Guid.NewGuid() + ".jpg";
                string filePath = Path.Combine(uploadFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.File.CopyTo(fileStream);
                }
                model.Image = uniqueFileName;
            }
        }
    }
}
