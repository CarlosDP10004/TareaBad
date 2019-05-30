using System;
using System.Collections.Generic;

namespace gestioncomprasAPI.Models
{
    public partial class Permiso
    {
        public Permiso()
        {
            RolPermiso = new HashSet<RolPermiso>();
        }

        public int IdPermiso { get; set; }
        public string NombrePermiso { get; set; }

        public ICollection<RolPermiso> RolPermiso { get; set; }
    }
}
