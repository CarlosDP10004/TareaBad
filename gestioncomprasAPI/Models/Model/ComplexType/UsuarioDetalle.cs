using gestioncomprasAPI.Models.Model.StoredProcedures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestioncomprasAPI.Models.Model.ComplexType
{
    public class UsuarioDetalle
    {

        public UsuarioDetalle() { }
        public UsuarioDetalle(Usuario usuario, List<RolesUsuarioDetalle> rolesResult)
        {
            IdUsuario = usuario.Idusuario;
            CorreoElectronico = usuario.CorreoElectronico;
            Clave = usuario.Clave;
            IdPersona = usuario.IdPersona;
            Estado = usuario.Estado;
            Intento = usuario.Intento;
            ListaRoles = rolesResult;         

        }

        public int IdUsuario { get; set; }
        public string CorreoElectronico { get; set; }
        public string Clave { get; set; }
        public int IdPersona { get; set; }
        public bool Estado { get; set; }
        public int Intento { get; set; }
        public List<RolesUsuarioDetalle> ListaRoles { get; set; }
    }
}
