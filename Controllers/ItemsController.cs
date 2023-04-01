using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webApi.Models;

namespace webApi.Controllers
{
    [Route("api/Items")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly ItemsContext _context;

        public ItemsController(ItemsContext context)
        {
            _context = context;
        }

        // GET: api/Items
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Items>>> GetTodoItems()
        {
          if (_context.TodoItems == null)
          {
              return NotFound();
          }
            return await _context.TodoItems.ToListAsync();
        }

        // GET: api/Items/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Items>> GetItems(long id)
        {
          if (_context.TodoItems == null)
          {
              return NotFound();
          }
            var items = await _context.TodoItems.FindAsync(id);

            if (items == null)
            {
                return NotFound();
            }

            return items;
        }

        // PUT: api/Items/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItems(long id, Items items)
        {
            if (id != items.Id)
            {
                return BadRequest();
            }

            _context.Entry(items).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemsExists(id))
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

        // POST: api/Items
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Items>> PostItems(Items items)
        {
          if (_context.TodoItems == null)
          {
              return Problem("Entity set 'ItemsContext.TodoItems'  is null.");
          }
            _context.TodoItems.Add(items);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetItems", new { id = items.Id }, items);
        }

        // DELETE: api/Items/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItems(long id)
        {
            if (_context.TodoItems == null)
            {
                return NotFound();
            }
            var items = await _context.TodoItems.FindAsync(id);
            if (items == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(items);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ItemsExists(long id)
        {
            return (_context.TodoItems?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
