using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestioncomprasAPI.Models.Model.StoredProcedures.EmpresaProveedora
{
    public class SPSLTBEmpresaProveedoraAutorizadaResult
    {
        public int Id { get; set; }
        public int IdEmpresaProveedora { get; set; }
        public string NombreEmpresa { get; set; }
    }
}
