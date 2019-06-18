using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestioncomprasAPI.Models.Model.StoredProcedures.EmpresaProveedora
{
    public class SPSLTBEmpleadoResult
    {
        public int Id { get; set; }
        public int IdEmpleado { get; set; }
        public string NombreEmpleado { get; set; }
    }
}
