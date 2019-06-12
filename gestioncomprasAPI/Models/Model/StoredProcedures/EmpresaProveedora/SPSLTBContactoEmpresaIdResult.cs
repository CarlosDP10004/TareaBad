using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestioncomprasAPI.Models.Model.StoredProcedures.EmpresaProveedora
{
    public class SPSLTBContactoEmpresaIdResult
    {
        public int Id { get; set; }
        public int IdContactoEmpresa { get; set; }
        public int IdLista { get; set; }        
        public string NombreLista { get; set; }
        public string DescripcionContacto { get; set; }
    }
}
