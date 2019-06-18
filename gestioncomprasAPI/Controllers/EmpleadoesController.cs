using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using gestioncomprasAPI.Models;
using System.Data.SqlClient;
using gestioncomprasAPI.Models.Model.BasicType;
using Microsoft.AspNetCore.Authorization;

namespace gestioncomprasAPI.Controllers
{
    [Authorize]
    [ApiController]
    public class EmpleadoesController : ControllerBase
    {
        private readonly GestionComprasContext _context;

        public EmpleadoesController(GestionComprasContext context)
        {
            _context = context;
        }

        // GET: api/Empleadoes
        [HttpGet]
        [Route("api/Empleadoes")]
        public IActionResult GetEmpleado()
        {
            SqlParameter[] parameters = new SqlParameter[] { };

            var query = _context.SPSLTBEmpleadoTodos.FromSql("EXEC dbo.SPSLTBEmpleado", parameters).ToList();
            return Ok(query);
        }


        // POST: api/Empleadoes
        [HttpPost]
        [Route("api/Empleadoes")]
        public IActionResult PostEmpleado([FromBody] EmpleadoDetalleBasic empleado)
        {
            int idUsuario = HttpContext.GetUserClaim();
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@_idEmpresa", empleado.IdEmpresa),
                new SqlParameter("@_idPersona", empleado.IdPersona),
                new SqlParameter("@_idPuesto", empleado.IdPuesto),
                new SqlParameter("@_usuarioSession", idUsuario)
            };

            var query = _context.SPINTBEmpleado.FromSql("EXEC dbo.SPINTBEmpleado @_idEmpresa, @_idPersona, " +
                "@_idPuesto, @_usuarioSession", parameters).FirstOrDefault();
            ReturnBasic returnBasic = new ReturnBasic("El Empleado se ha registrado existosamente");
            try
            {
                if (query == null)
                {
                    returnBasic.Mensaje = "La petición no fue procesada correctamente";
                    return BadRequest(returnBasic);
                }
                _context.SaveChanges();
                return Ok(returnBasic);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpGet("api/Empleadoes/EmpresasDisponibles")]

        public IActionResult GetEmpresasParaEmpleados() {
            SqlParameter[] parameter = new SqlParameter[] { };
            var query = _context.SPSLTBEmpresasEmpleados.FromSql("EXEC dbo.SPSLTBEmpresasEmpleados", parameter).ToList();
            return Ok(query);
        }

        [HttpGet("api/Empleadoes/ObtenerPuestos")]
        public IActionResult GetPuestos() {
            SqlParameter[] parameters = new SqlParameter[] { };
            var query = _context.SPSLTBListaPuestos.FromSql("EXEC dbo.SPSLTBListaPuestos", parameters).ToList();
            return Ok(query);
        }

        [HttpGet("api/Empleadoes/ObtenerPersonasNoEmpleadas")]
        public IActionResult GetPersonasNoEmpleadas() {
            SqlParameter[] parameters = new SqlParameter[] { };
            var query = _context.SPSLUsuariosNoEmpleados.FromSql("EXEC dbo.SPSLUsuariosNoEmpleados", parameters).ToList();
            return Ok(query);
        }

        private bool EmpleadoExists(int id)
        {
            return _context.Empleado.Any(e => e.IdEmpleado == id);
        }
    }
}