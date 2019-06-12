using gestioncomprasAPI.Models.Model.StoredProcedures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestioncomprasAPI.Models.Model.ComplexType
{
    public class RolesUsuarioDetalle
    {
        public RolesUsuarioDetalle() { }

        public RolesUsuarioDetalle(int idrol, string nombrerol, List<SPSLTBPermisoxRolResult> permisosResult)
        {
            IdRol = idrol;
            NombreRol  = nombrerol;

            permisosDetalle = new List<PermisoDetalle>();
            if (permisosResult != null) {
                permisosDetalle = permisosResult.Select(
                    x => new PermisoDetalle(x)
                    ).ToList();
            }
        }

        public int IdRol { get; set; }
        public string NombreRol { get; set; }
        public List<PermisoDetalle> permisosDetalle { get;set;}

    }

    
}
