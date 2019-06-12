using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using gestioncomprasAPI.Models;

namespace gestioncomprasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BitacorasController : ControllerBase
    {
        private readonly GestionComprasContext _context;

        public BitacorasController(GestionComprasContext context)
        {
            _context = context;
        }

        // GET: api/Bitacoras
        [HttpGet]
        public IEnumerable<Bitacora> GetBitacora()
        {
            return _context.Bitacora;
        }

        // GET: api/Bitacoras/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBitacora([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bitacora = await _context.Bitacora.FindAsync(id);

            if (bitacora == null)
            {
                return NotFound();
            }

            return Ok(bitacora);
        }

        // PUT: api/Bitacoras/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBitacora([FromRoute] int id, [FromBody] Bitacora bitacora)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bitacora.IdRegistroBitacora)
            {
                return BadRequest();
            }

            _context.Entry(bitacora).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BitacoraExists(id))
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

        // POST: api/Bitacoras
        [HttpPost]
        public async Task<IActionResult> PostBitacora([FromBody] Bitacora bitacora)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Bitacora.Add(bitacora);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBitacora", new { id = bitacora.IdRegistroBitacora }, bitacora);
        }

        // DELETE: api/Bitacoras/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBitacora([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bitacora = await _context.Bitacora.FindAsync(id);
            if (bitacora == null)
            {
                return NotFound();
            }

            _context.Bitacora.Remove(bitacora);
            await _context.SaveChangesAsync();

            return Ok(bitacora);
        }

        private bool BitacoraExists(int id)
        {
            return _context.Bitacora.Any(e => e.IdRegistroBitacora == id);
        }
    }
}