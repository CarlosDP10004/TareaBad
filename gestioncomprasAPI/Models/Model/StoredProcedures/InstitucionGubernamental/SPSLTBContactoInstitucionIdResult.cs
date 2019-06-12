using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestioncomprasAPI.Models.Model.StoredProcedures.InstitucionGubernamental
{
    public class SPSLTBContactoInstitucionIdResult
    {
        public int Id { get; set; }
        public int IdLista { get; set; }
        public int IdContactoInstitucion { get; set; }
        public string NombreLista { get; set; }
        public string DescripcionContacto { get; set; }
    }
}
