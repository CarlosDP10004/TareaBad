using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using gestioncomprasAPI.Models;
using gestioncomprasAPI.Models.Model.BasicType;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace gestioncomprasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly GestionComprasContext _context;

        private UsuariosController session;

        public HomeController(GestionComprasContext context)
        {
            _context = context;
        }

        //POST: api/Home/5
        [HttpPost]
        public IActionResult LogueoUsuario([FromBody] UsuarioLoginBasic usuario)
        {
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@_correoElectronico", usuario.CorreoElectronico),
                new SqlParameter("@_clave", usuario.Clave)
            };

            var result = _context.SPSLTBUsuarioAutenticar.FromSql("EXEC dbo.SPSLTBUsuarioAutenticar @_correoElectronico, @_clave", parameters).FirstOrDefault();
            switch (result.Id)
            {
                case 0:
                    return NotFound("El usuario no se encuentra registrado en la BD");                  
                case 1:
                    return NotFound("Su contraseña es incorrecta");
                case 2:
                    return NotFound("Su usuario ha sido bloqueado, comuniquese con HelpDesk");
                case 3:
                    return NotFound("Su usuario ha sido bloqueado, comuniquese con HelpDesk");
                default:
                    return Ok("Usuario autenticado");
                    
                    //try
                    //{
                    //    var aux = _context.Usuario.Where(x => x.CorreoElectronico == usuario.CorreoElectronico).FirstOrDefault();
                    //    return Ok(session.GetUsuario(aux.Idusuario));
                    //}
                    //catch (Exception)
                    //{

                    //    throw;
                    //}
                    
            }

        }
    }
}