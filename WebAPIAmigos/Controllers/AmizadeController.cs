using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.Domain;

namespace WebAPIAmigos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AmizadeController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public AmizadeController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/Amizade
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Amizade>>> GetAmizade()
        {
            return await _context.Amizade.Include(x => x.PessoaEamigo).Include(x => x.PessoaEamigo.Pais).Include(x => x.PessoaEamigo.Estado).ToListAsync();
        }

        // GET: api/Amizade/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Amizade>>> GetTodosAmigos(int id)
        {
            return Ok(await _context.Amizade.Include(x => x.PessoaEamigo.Estado).Include(x => x.PessoaEamigo.Pais).Where(x => x.PessoaId != id).ToListAsync());
        }
    

        // PUT: api/Amizade/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAmizade(int id, Amizade amizade)
        {
            if (id != amizade.PessoaId)
            {
                return BadRequest();
            }

            _context.Entry(amizade).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AmizadeExists(id))
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

        // POST: api/Amizade
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Amizade>> PostAmizade(Amizade amizade)
        {
            var amizade2 = new Amizade
            {
                PessoaId = amizade.AmigoId,
                PessoaEamigo = amizade.PessoaEamigo,
                AmigoId = amizade.PessoaId
            };

            _context.Amizade.Add(amizade);
            var person = _context.Amigos.Find(amizade.PessoaId);
            person.Amizades ??= new List<Amizade>();
            person.Amizades.Add(amizade);

            _context.Amizade.Add(amizade2);
            var friend = _context.Amigos.Find(amizade.AmigoId);
            friend.Amizades ??= new List<Amizade>();
            friend.Amizades.Add(amizade2);
            try {await _context.SaveChangesAsync();}
            catch (DbUpdateException)
            {
                if (AmizadeExists(amizade.PessoaId))
                {
                    return Conflict();
                }
                else {throw;}
            }

            return CreatedAtAction("GetAmizade", new { id = amizade.PessoaId }, amizade);
        }

        // DELETE: api/Amizade/5
        [HttpDelete("{id}/{id2}")]
        public async Task<ActionResult<Amizade>> DeleteAmizade(int id, int id2)
        {
            var amizade = await _context.Amizade.FirstOrDefaultAsync(x => x.PessoaId == id && x.AmigoId == id2);
            var amizade1 = await _context.Amizade.FirstOrDefaultAsync(x => x.AmigoId == id && x.PessoaId == id2);
            if (amizade == null)
            {
                return NotFound();
            }

            _context.Amizade.Remove(amizade);
            _context.Amizade.Remove(amizade1);
            await _context.SaveChangesAsync();

            return amizade;
        }

        private bool AmizadeExists(int id)
        {
            return _context.Amizade.Any(e => e.PessoaId == id);
        }
    }
}
