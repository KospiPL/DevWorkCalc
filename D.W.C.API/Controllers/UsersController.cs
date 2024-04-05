using D.W.C.API.D.W.C.Service;
using D.W.C.Lib.D.W.C.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace D.W.C.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UzytkownicyController : ControllerBase
    {
        private readonly MyDatabaseContext _context;

        public UzytkownicyController(MyDatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Uzytkownicy
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Uzytkownik>>> GetUzytkownicy()
        {
            return await _context.Uzytkownicy.ToListAsync();
        }

        // GET: api/Uzytkownicy/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Uzytkownik>> GetUzytkownik(int id)
        {
            var uzytkownik = await _context.Uzytkownicy.FindAsync(id);

            if (uzytkownik == null)
            {
                return NotFound();
            }

            return uzytkownik;
        }

        // POST: api/Uzytkownicy
        [HttpPost]
        public async Task<ActionResult<Uzytkownik>> PostUzytkownik(Uzytkownik uzytkownik)
        {
            _context.Uzytkownicy.Add(uzytkownik);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUzytkownik", new { id = uzytkownik.ID }, uzytkownik);
        }

        // PUT: api/Uzytkownicy/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUzytkownik(int id, Uzytkownik uzytkownik)
        {
            if (id != uzytkownik.ID)
            {
                return BadRequest();
            }

            _context.Entry(uzytkownik).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UzytkownikExists(id))
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

        // DELETE: api/Uzytkownicy/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUzytkownik(int id)
        {
            var uzytkownik = await _context.Uzytkownicy.FindAsync(id);
            if (uzytkownik == null)
            {
                return NotFound();
            }

            _context.Uzytkownicy.Remove(uzytkownik);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UzytkownikExists(int id)
        {
            return _context.Uzytkownicy.Any(e => e.ID == id);
        }
    }
}
