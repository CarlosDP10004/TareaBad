using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using gestioncomprasAPI.Models;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Authorization;
using System.Threading;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace gestioncomprasAPI.Controllers
{
    [Authorize]
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

            int idUsuario = HttpContext.GetUserClaim();

            SqlParameter[] parameters = new SqlParameter[] { };
            var bitacora = _context.SPSLTBBitacora.FromSql("EXEC dbo.SPSLTBBitacora", parameters).ToList();
            return Ok(bitacora);
        }
    }
}