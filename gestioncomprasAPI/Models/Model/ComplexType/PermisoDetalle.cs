using gestioncomprasAPI.Models.Model.StoredProcedures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestioncomprasAPI.Models.Model.ComplexType
{
    public class PermisoDetalle
    {
        public PermisoDetalle() { }

        public PermisoDetalle(SPSLTBPermisoxRolResult permisosResult) {
            IdPermiso = permisosResult.IdPermiso;
            NombrePermiso = permisosResult.NombrePermiso;
        }

        public int IdPermiso { get; set; }
        public string NombrePermiso { get; set; }
    }
}
