using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;
 

namespace WebApp
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartsController : ControllerBase
    {
        private readonly FandomContext _context;

        public ChartsController(FandomContext context)
        {
            _context = context;
        }
        [HttpGet("JsonDataCast/{id}")]
        public JsonResult JsonDataCast(int id)
        {
            var casts = (_context.Casts.Where(c => c.Character.SeriesID == id).Include(a => a.Actor).Include(c => c.Character).ToList());
            //var Themes = _context.Themes.Include(s => s.Sets).ToList();
            List<object> castSet = new List<object>();

            // castSet.Add(new[] { "Actor", "Character", "First Appereance", "Last Appereance"});
            castSet.Add(new[] {"Character", "From" , "To"});
            foreach (var c in casts)
            {
                 castSet.Add(new object[] { c.Character.Name, c.FirstAppereance, c.LastAppereance });
            }
            return new JsonResult(castSet);
        }
    }
}
