using System;
using System.Collections.Generic;

namespace gestioncomprasAPI.Models
{
    public partial class UsuarioRol
    {
        public int IdUsuarioRol { get; set; }
        public int IdUsuario { get; set; }
        public int IdRol { get; set; }

        public Rol IdRolNavigation { get; set; }
        public Usuario IdUsuarioNavigation { get; set; }
    }
}
