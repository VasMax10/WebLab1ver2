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
    public class SeriesController : Controller
    {
        private readonly FandomContext _context;
        [Obsolete]
        private readonly IHostingEnvironment _env;

        [Obsolete]
        public SeriesController(FandomContext context,IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: Series
        public async Task<IActionResult> Index()
        {
            return View(await _context.Series.ToListAsync());
        }

        // GET: Series/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var series = await _context.Series
                .FirstOrDefaultAsync(m => m.ID == id);
            if (series == null)
            {
                return NotFound();
            }

            //return View(series);
            return RedirectToAction("Index", "Casts", new { seriesID = series.ID});
        }

        // GET: Series/Characters/5
        public async Task<IActionResult> Characters(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var series = await _context.Series
                .FirstOrDefaultAsync(m => m.ID == id);
            if (series == null)
            {
                return NotFound();
            }

            //return View(series);
            return RedirectToAction("Index", "Characters", new { seriesID = series.ID});
        }

        // GET: Series/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Series/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Obsolete]
        public async Task<IActionResult> Create(IFormFile Poster, IFormFile BackImage, [Bind("ID,Name,Premiere,IsEnded,MainColor,SecondColor,Info")] Series series)
        {
            if (Poster != null && Poster.Length > 0)
            {
                var imagePath = @"\Upload\Images\Series\Posters\";
                var uploadPath = _env.WebRootPath + imagePath;

                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }
                var uniqFileName = Guid.NewGuid().ToString();
                var filename = Path.GetFileName(uniqFileName + "." + Poster.FileName.Split(".")[1].ToLower());

                string fullPath = uploadPath + filename;

                imagePath = imagePath + @"\";

                var filePath = Path.Combine(imagePath, filename);
                using (var fileStream = new FileStream(fullPath, FileMode.Create))
                {
                    await Poster.CopyToAsync(fileStream);
                }
                series.Poster = filePath;
            }
            if (BackImage != null && BackImage.Length > 0)
            {
                var imagePath = @"\Upload\Images\Series\BackImages\";
                var uploadPath = _env.WebRootPath + imagePath;

                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }
                var uniqFileName = Guid.NewGuid().ToString();
                var filename = Path.GetFileName(uniqFileName + "." + BackImage.FileName.Split(".")[1].ToLower());

                string fullPath = uploadPath + filename;

                imagePath = imagePath + @"\";

                var filePath = Path.Combine(imagePath, filename);
                using (var fileStream = new FileStream(fullPath, FileMode.Create))
                {
                    await BackImage.CopyToAsync(fileStream);
                }
                series.BackImage = filePath;
            }
            if (ModelState.IsValid)
            {
                _context.Add(series);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(series);
        }

        // GET: Series/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var series = await _context.Series.FindAsync(id);
            if (series == null)
            {
                return NotFound();
            }
            return View(series);
        }

        // POST: Series/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Obsolete]
        public async Task<IActionResult> Edit(int id, IFormFile Poster, IFormFile BackImage, [Bind("ID,Name,Poster,Premiere,IsEnded,BackImage,MainColor,SecondColor,Info")] Series series)
        {
            if (id != series.ID)
            {
                return NotFound();
            }

            if (Poster != null && Poster.Length > 0)
            {
                if (series.Poster != null)
                {
                    var PhotoPath = _env.WebRootPath + series.Poster;
                    if (System.IO.File.Exists(PhotoPath))
                    {
                        System.IO.File.Delete(PhotoPath);
                    }
                }
                var imagePath = @"\Upload\Images\Series\Posters\";
                var uploadPath = _env.WebRootPath + imagePath;

                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }
                var uniqFileName = Guid.NewGuid().ToString();
                var filename = Path.GetFileName(uniqFileName + "." + Poster.FileName.Split(".")[1].ToLower());

                string fullPath = uploadPath + filename;

                imagePath = imagePath + @"\";

                var filePath = Path.Combine(imagePath, filename);
                using (var fileStream = new FileStream(fullPath, FileMode.Create))
                {
                    await Poster.CopyToAsync(fileStream);
                }
                series.Poster = filePath;
            }
            if (BackImage != null && BackImage.Length > 0)
            {
                if (series.BackImage != null)
                {
                    var PhotoPath = _env.WebRootPath + series.BackImage;
                    if (System.IO.File.Exists(PhotoPath))
                    {
                        System.IO.File.Delete(PhotoPath);
                    }
                }
                var imagePath = @"\Upload\Images\Series\BackImages\";
                var uploadPath = _env.WebRootPath + imagePath;

                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }
                var uniqFileName = Guid.NewGuid().ToString();
                var filename = Path.GetFileName(uniqFileName + "." + BackImage.FileName.Split(".")[1].ToLower());

                string fullPath = uploadPath + filename;

                imagePath = imagePath + @"\";

                var filePath = Path.Combine(imagePath, filename);
                using (var fileStream = new FileStream(fullPath, FileMode.Create))
                {
                    await BackImage.CopyToAsync(fileStream);
                }
                series.BackImage = filePath;
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(series);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SeriesExists(series.ID))
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
            return View(series);
        }

        // GET: Series/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var series = await _context.Series
                .FirstOrDefaultAsync(m => m.ID == id);
            if (series == null)
            {
                return NotFound();
            }

            return View(series);
        }

        // POST: Series/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Obsolete]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var series = await _context.Series.FindAsync(id);
            if (series.Poster != null)
            {
                var PosterPath = _env.WebRootPath + series.Poster;
                if (System.IO.File.Exists(PosterPath))
                {
                    System.IO.File.Delete(PosterPath);
                }
            }

            if (series.BackImage != null)
            {
                var BackImage = _env.WebRootPath + series.BackImage;
                if (System.IO.File.Exists(BackImage))
                {
                    System.IO.File.Delete(BackImage);
                }
            }
            _context.Series.Remove(series);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SeriesExists(int id)
        {
            return _context.Series.Any(e => e.ID == id);
        }
    }
}
