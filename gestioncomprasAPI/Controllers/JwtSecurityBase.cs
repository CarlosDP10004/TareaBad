using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace gestioncomprasAPI.Controllers
{
    public static class JwtSecurityBase
    {
        /// <summary>
        /// Obtiene el Id según contexto de seguridad
        /// </summary>
        /// <param name="context"></param>
        /// <param name="type">Tipo de Claim a obtener</param>
        /// <returns>Id del usuario</returns>
        public static int GetUserClaim(this HttpContext context, string type = @"http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")
        {
            var identity = (ClaimsIdentity)context.User.Identity;
            var claims = identity?.Claims;

            var idUsuario = claims?.FirstOrDefault(x => x.Type == type)?.Value ?? "0";

            return int.Parse(idUsuario);
        }
    }
}
