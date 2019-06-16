using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using gestioncomprasAPI.Models;
using gestioncomprasAPI.Models.Model.BasicType;
using gestioncomprasAPI.Models.Model.ComplexType;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace gestioncomprasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly GestionComprasContext _context;

        //public HomeController()
        //{
        //    _context = context;
        //}
        private IConfiguration _config;
   
        public HomeController(IConfiguration config, GestionComprasContext context)
        {
            _config = config;
            _context = context;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody]Usuario login)
        {
            var user = AuthenticateUser(login);

            if (user.Item2 != null)
            {
                var tokenString = GenerateJSONWebToken(user.Item2);
                return StatusCode(200, new { token = tokenString });
            }

            return StatusCode(401, user.Item1);
        }

        private string GenerateJSONWebToken(Usuario userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {            
            new Claim(JwtRegisteredClaimNames.Email, userInfo.CorreoElectronico),
            new Claim(JwtRegisteredClaimNames.NameId, userInfo.Idusuario.ToString())
            };

            var token = new JwtSecurityToken(issuer:_config["Jwt:Issuer"],
              audience: _config["Jwt:Issuer"],
              claims: claims,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        //private Usuario AuthenticateUser(Usuario login)
        //{
        //    Usuario user = null;

        //    //Validate the User Credentials  
        //    //Demo Purpose, I have Passed HardCoded User Information  
        //    if (login.CorreoElectronico != null)
        //    {

        //        user = new Usuario {Idusuario = login.Idusuario, CorreoElectronico = login.CorreoElectronico, Clave = login.Clave, Estado = login.Estado};
        //    }
        //    return user;
        //}


        private (string, Usuario) AuthenticateUser(Usuario usuario)
        {
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@_correoElectronico", usuario.CorreoElectronico),
                new SqlParameter("@_clave", usuario.Clave)
            };

            var result = _context.SPSLTBUsuarioAutenticar.FromSql("EXEC dbo.SPSLTBUsuarioAutenticar @_correoElectronico, @_clave", parameters).FirstOrDefault();
            switch (result.Id)
            {
                case 0:
                    return ("El usuario no se encuentra registrado en la BD", null);
                case 1:
                    return ("Su contraseña es incorrecta", null);
                case 2:
                case 3:
                    return ("Su usuario ha sido bloqueado, comuniquese con HelpDesk", null);
                default:
                    var usuarioEncontrado = new Usuario() {
                        CorreoElectronico = usuario.CorreoElectronico,
                        Idusuario = result.Idusuario
                    };

                    return ("Usuario valido", usuarioEncontrado);
            }
        }



        ////GET: api/Home/5
        //[HttpGet("{id}")]
        //public IActionResult GetUsuarioSession([FromRoute] int id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    SqlParameter[] parameters = new SqlParameter[] {
        //        new SqlParameter("@_id", id)
        //    };
        //    var roles = _context.SPSLTBRolxUsuario.FromSql("EXEC dbo.SPSLTBRolxUsuario @_id", parameters).ToList();
        //    var rolesLista = new List<RolesUsuarioDetalle>();
        //    foreach (var Rol in roles)
        //    {
        //        SqlParameter[] par = new SqlParameter[] { new SqlParameter("@_idRol", Rol.IdRol) };
        //        var per = _context.SPSLTBPermisoxRol.FromSql("EXEC dbo.SPSLTBPermisoxRol @_idRol", par).ToList();
        //        var aux = new RolesUsuarioDetalle(Rol.IdRol, Rol.NombreRol, per);
        //        rolesLista.Add(aux);
        //    }
        //    var usuario = _context.Usuario.FromSql("EXEC dbo.SPSLTBUsuarioId @_id", parameters).FirstOrDefault();

        //    if (usuario == null)
        //    {
        //        return NotFound($"La persona con el identificador {id} no se encuentra en la base de datos.");
        //    }
        //    else
        //    {
        //        UsuarioDetalle usuarioDetalle = new UsuarioDetalle(usuario, rolesLista);
        //        return Ok(usuarioDetalle);
        //    }
        //}


    }

    
}

