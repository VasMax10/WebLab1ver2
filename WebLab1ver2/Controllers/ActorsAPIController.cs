using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorsAPIController : ControllerBase
    {
        private readonly FandomContext _context;

        public ActorsAPIController(FandomContext context)
        {
            _context = context;
        }

        // GET: api/ActorsAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Actors>>> GetActors()
        {
            return await _context.Actors.ToListAsync();
        }

        // GET: api/ActorsAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Actors>> GetActors(int id)
        {
            var actors = await _context.Actors.FindAsync(id);

            if (actors == null)
            {
                return NotFound();
            }

            return actors;
        }

        // PUT: api/ActorsAPI/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActors(int id, Actors actors)
        {
            if (id != actors.ID)
            {
                return BadRequest();
            }

            _context.Entry(actors).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActorsExists(id))
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

        // POST: api/ActorsAPI
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Actors>> PostActors(Actors actors)
        {
            _context.Actors.Add(actors);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetActors", new { id = actors.ID }, actors);
        }

        // DELETE: api/ActorsAPI/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Actors>> DeleteActors(int id)
        {
            var actors = await _context.Actors.FindAsync(id);
            if (actors == null)
            {
                return NotFound();
            }

            _context.Actors.Remove(actors);
            await _context.SaveChangesAsync();

            return actors;
        }

        private bool ActorsExists(int id)
        {
            return _context.Actors.Any(e => e.ID == id);
        }
    }
}
