using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using gestioncomprasAPI.Models;
using System.Data.SqlClient;

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
        public IActionResult GetBitacora()
        {
            SqlParameter[] parameters = new SqlParameter[] { };
            var bitacora = _context.SPSLTBBitacora.FromSql("EXEC dbo.SPSLTBBitacora", parameters).ToList();
            return Ok(bitacora);
        }

    }
}