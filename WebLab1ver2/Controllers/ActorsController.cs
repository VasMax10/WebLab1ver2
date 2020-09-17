using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using web_lab1_fandom.Models;

namespace web_lab1_fandom.Controllers
{
    public class ActorsController : Controller
    {
        private readonly FandomContext _context;
        [Obsolete]
        private readonly IHostingEnvironment _env;

        [Obsolete]
        public ActorsController(FandomContext context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: Actors
        public async Task<IActionResult> Index()
        {
            return View(await _context.Actors.ToListAsync());
        }

        // GET: Actors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actors = await _context.Actors
                .FirstOrDefaultAsync(m => m.ID == id);
            if (actors == null)
            {
                return NotFound();
            }

            return View(actors);
        }

        // GET: Actors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Actors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Obsolete]
        public async Task<IActionResult> Create(IFormFile Photo, [Bind("ID,Name,Birthdate,Info")] Actors actors)
        {
            if (Photo != null && Photo.Length > 0)
            {
                var imagePath = @"\Upload\Images\Actors\";
                var uploadPath = _env.WebRootPath + imagePath;

                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }
                var uniqFileName = Guid.NewGuid().ToString();
                var filename = Path.GetFileName(uniqFileName + "." + Photo.FileName.Split(".")[1].ToLower());
                
                string fullPath = uploadPath + filename;

                imagePath = imagePath + @"\";

                var filePath = Path.Combine(imagePath, filename);
                using (var fileStream = new FileStream(fullPath, FileMode.Create))
                {
                    await Photo.CopyToAsync(fileStream);
                }
                actors.Photo = filePath;
            }
            if (ModelState.IsValid)
            {
                _context.Add(actors);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(actors);
        }

        // GET: Actors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actors = await _context.Actors.FindAsync(id);
            if (actors == null)
            {
                return NotFound();
            }
            return View(actors);
        }

        // POST: Actors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Obsolete]
        public async Task<IActionResult> Edit(int id, IFormFile Photo, [Bind("ID,Name,Photo,Birthdate,Info")] Actors actors)
        {
            if (id != actors.ID)
            {
                return NotFound();
            }
            if (Photo != null && Photo.Length > 0)
            {
                if (actors.Photo != null)
                {
                    var PhotoPath = _env.WebRootPath + actors.Photo;
                    if (System.IO.File.Exists(PhotoPath))
                    {
                        System.IO.File.Delete(PhotoPath);
                    }
                }
                var imagePath = @"\Upload\Images\Actors\";
                var uploadPath = _env.WebRootPath + imagePath;

                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }
                var uniqFileName = Guid.NewGuid().ToString();
                var filename = Path.GetFileName(uniqFileName + "." + Photo.FileName.Split(".")[1].ToLower());

                string fullPath = uploadPath + filename;

                imagePath = imagePath + @"\";

                var filePath = Path.Combine(imagePath, filename);
                using (var fileStream = new FileStream(fullPath, FileMode.Create))
                {
                    await Photo.CopyToAsync(fileStream);
                }
                actors.Photo = filePath;
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(actors);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActorsExists(actors.ID))
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
            return View(actors);
        }

        // GET: Actors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actors = await _context.Actors
                .FirstOrDefaultAsync(m => m.ID == id);
            
            if (actors == null)
            {
                return NotFound();
            }

            return View(actors);
        }

        // POST: Actors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Obsolete]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var actors = await _context.Actors.FindAsync(id);
            if (actors.Photo != null)
            {
                var PhotoPath = _env.WebRootPath + actors.Photo;
                if (System.IO.File.Exists(PhotoPath))
                {
                    System.IO.File.Delete(PhotoPath);
                }
            }
            _context.Actors.Remove(actors);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActorsExists(int id)
        {
            return _context.Actors.Any(e => e.ID == id);
        }
    }
}
