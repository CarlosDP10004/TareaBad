using System;
using System.Collections.Generic;

namespace gestioncomprasAPI.Models
{
    public partial class Rol
    {
        public Rol()
        {
            RolPermiso = new HashSet<RolPermiso>();
            UsuarioRol = new HashSet<UsuarioRol>();
        }

        public int IdRol { get; set; }
        public string NombreRol { get; set; }

        public ICollection<RolPermiso> RolPermiso { get; set; }
        public ICollection<UsuarioRol> UsuarioRol { get; set; }
    }
}
