using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using gestioncomprasAPI.Models;
using System.Data.SqlClient;

namespace gestioncomprasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermisoesController : ControllerBase
    {
        private readonly GestionComprasContext _context;

        public PermisoesController(GestionComprasContext context)
        {
            _context = context;
        }

        // GET: api/Permisoes
        [HttpGet]
        public IActionResult GetPermiso()
        {
            //return _context.Permiso.Include("RolPermiso").ToList();
            //return _context.Permiso.Where(x => x.IdPermiso == 2).Include("RolPermiso").ToList();
            //return _context.Permiso;
            SqlParameter[] parameters = new SqlParameter[]
                {
                  //new SqlParameter("_id", 1)
                 };
            //var result = _context.SPPermisoRecuperarTodoResult.FromSql("EXEC dbo.ObtenerTodosLosPermisos", parameters)
            //    .Select(x => new Permiso() {
            //     IdPermiso = x.IdPermiso,
            //     NombrePermiso = x.NombrePermiso
            //    }).ToList();
            var result = _context.Permiso.FromSql("EXEC dbo.ObtenerTodosLosPermisos", parameters).ToList();

            return Ok(result);
        }

        // GET: api/Permisoes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPermiso([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var permiso = await _context.Permiso.FindAsync(id);

            if (permiso == null)
            {
                return NotFound();
            }

            return Ok(permiso);
        }

        // PUT: api/Permisoes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPermiso([FromRoute] int id, [FromBody] Permiso permiso)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != permiso.IdPermiso)
            {
                return BadRequest();
            }

            _context.Entry(permiso).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PermisoExists(id))
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

        // POST: api/Permisoes
        [HttpPost]
        public async Task<IActionResult> PostPermiso([FromBody] Permiso permiso)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Permiso.Add(permiso);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPermiso", new { id = permiso.IdPermiso }, permiso);
        }

        // DELETE: api/Permisoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePermiso([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var permiso = await _context.Permiso.FindAsync(id);
            if (permiso == null)
            {
                return NotFound();
            }

            _context.Permiso.Remove(permiso);
            await _context.SaveChangesAsync();

            return Ok(permiso);
        }

        private bool PermisoExists(int id)
        {
            return _context.Permiso.Any(e => e.IdPermiso == id);
        }
    }
}