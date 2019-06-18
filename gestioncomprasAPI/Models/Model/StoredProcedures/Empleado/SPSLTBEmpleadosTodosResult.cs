using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestioncomprasAPI.Models.Model.StoredProcedures.Empleado
{
    public class SPSLTBEmpleadosTodosResult
    {
        public int Id { get; set; }
        public int IdEmpleado { get; set; }
        public string NombreEmpresa { get; set; }
        public string NombreCompleto { get; set; }
        public string NombreLista { get; set; }
    }
}
