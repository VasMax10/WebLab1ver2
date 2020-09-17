using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web_lab1_fandom.Models;

namespace web_lab1_fandom.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Casts1Controller : ControllerBase
    {
        private readonly FandomContext _context;

        public Casts1Controller(FandomContext context)
        {
            _context = context;
        }
        public class UserCast : Casts
        {
            public string ActorName { get; set; }
            public string ActorPhoto { get; set; }
            public string CharacterName { get; set; }
            public string CharacterPhoto { get; set; }
            public UserCast(Casts casts, string aName, string aPhoto, string cName, string cPhoto)
            {
                ID = casts.ID;
                ActorID = casts.ActorID;
                CharacterID = casts.CharacterID;
                FirstAppereance = casts.FirstAppereance;
                LastAppereance = casts.LastAppereance;
                Actor = casts.Actor;
                Character = casts.Character;
                ActorName = aName;
                ActorPhoto = aPhoto;
                CharacterName = cName;
                CharacterPhoto = cPhoto;
            }
        }


        // GET: api/Casts1
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserCast>>> GetCasts(string seriesName)
        {
            var list = _context.Casts.Select(c => new UserCast(c, c.Actor.Name, c.Actor.Photo, c.Character.Name, c.Character.Photo));
            if (seriesName != null)
            {

            }
            return await list.ToListAsync();
        }

        // GET: api/Casts1/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Casts>> GetCasts(int id)
        {
            var casts = await _context.Casts.FindAsync(id);

            if (casts == null)
            {
                return NotFound();
            }

            return casts;
        }

        // PUT: api/Casts1/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCasts(int id, Casts casts)
        {
            if (id != casts.ID)
            {
                return BadRequest();
            }

            _context.Entry(casts).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CastsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Casts1
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Casts>> PostCasts(Casts casts)
        {
            _context.Casts.Add(casts);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCasts", new { id = casts.ID }, casts);
        }

        // DELETE: api/Casts1/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Casts>> DeleteCasts(int id)
        {
            var casts = await _context.Casts.FindAsync(id);
            if (casts == null)
            {
                return NotFound();
            }

            _context.Casts.Remove(casts);
            await _context.SaveChangesAsync();

            return casts;
        }

        private bool CastsExists(int id)
        {
            return _context.Casts.Any(e => e.ID == id);
        }
    }
}
