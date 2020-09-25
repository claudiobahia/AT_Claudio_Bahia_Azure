using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIAmigos.Model;
using WebAPIAmigos.Repository;

namespace WebAPIAmigos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AmizadeController : ControllerBase
    {
        private readonly AmigoContext _context;

        public AmizadeController(AmigoContext context)
        {
            _context = context;
        }

        // GET: api/Amizade
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Amizade>>> GetAmizade()
        {
            return await _context.Amizade.Include(x => x.PessoaEamigo).ToListAsync();
        }

        // GET: api/Amizade/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Amizade>>> GetAmizade(int id)
        {
            var amizade = await _context.Amizade.Where(x => x.PessoaId == id).ToListAsync();

            if (amizade == null)
            {
                return NotFound();
            }

            List<Amizade> amizades = new List<Amizade>();

            foreach (Amizade u in amizade)
            {
                try
                {
                    Amigo amigo = await _context.Amigos.Include(x => x.Pais).Include(x => x.Estado).FirstOrDefaultAsync(x => x.Id == u.AmigoId);
                    amizades.Add(new Amizade
                    {
                        PessoaId = u.PessoaId,
                        AmigoId = u.AmigoId,
                        PessoaEamigo = amigo
                    });
                }
                catch (Exception _) { }
            }

            return amizades;
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
                PessoaId = amizade.PessoaId,
                PessoaEamigo = amizade.PessoaEamigo,
                AmigoId = amizade.AmigoId
            };

            _context.Amizade.Add(amizade);
            _context.Amizade.Add(amizade2); 
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AmizadeExists(amizade.PessoaId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
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
