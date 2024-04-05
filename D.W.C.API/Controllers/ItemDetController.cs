using D.W.C.API.D.W.C.Service;
using D.W.C.Lib.D.W.C.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace D.W.C.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemDetController : ControllerBase
    {
        private readonly MyDatabaseContext _context;

        public ItemDetController(MyDatabaseContext context)
        {
            _context = context;
        }

        // GET: api/ItemDet
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkItemDetails>>> GetItemDet()
        {
            return await _context.workItemDetails.ToListAsync();
        }

        // GET: api/ItemDet/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkItemDetails>> GetItemDet(int id)
        {
            var itemDet = await _context.workItemDetails.FindAsync(id);

            if (itemDet == null)
            {
                return NotFound();
            }

            return itemDet;
        }

        // PUT: api/ItemDet/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItemDet(int id, WorkItemDetails itemDet)
        {
            if (id != itemDet.Id)
            {
                return BadRequest();
            }

            _context.Entry(itemDet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemDetExists(id))
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

        // POST: api/ItemDet
        [HttpPost]
        public async Task<ActionResult<WorkItemDetails>> PostItemDet(WorkItemDetails itemDet)
        {
            _context.workItemDetails.Add(itemDet);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetItemDet), new { id = itemDet.Id }, itemDet);
        }

        // DELETE: api/ItemDet/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemDet(int id)
        {
            var itemDet = await _context.workItemDetails.FindAsync(id);
            if (itemDet == null)
            {
                return NotFound();
            }

            _context.workItemDetails.Remove(itemDet);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ItemDetExists(int id)
        {
            return _context.workItemDetails.Any(e => e.Id == id);
        }
    }
}
