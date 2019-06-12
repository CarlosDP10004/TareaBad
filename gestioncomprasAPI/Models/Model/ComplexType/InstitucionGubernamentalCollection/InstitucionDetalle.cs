using gestioncomprasAPI.Models.Model.StoredProcedures.InstitucionGubernamental;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestioncomprasAPI.Models.Model.ComplexType.InstitucionGubernamentalCollection
{
    public class InstitucionDetalle
    {
        public InstitucionDetalle() { }

        public InstitucionDetalle(InstitucionGubernamental institucion, List<SPSLTBContactoInstitucionIdResult> contactoResult) {
            IdInstitucionG = institucion.IdInstitucionG;
            NombreInstitucionG = institucion.NombreInstitucionG;
            EncargadoUACI = institucion.EncargadoUaci;
            LogotipoInstitucionG = institucion.LogotipoInstitucionG;
            ContactoInstitucionDetalles = new List<ContactoInstitucionDetalle>();
            if (contactoResult != null) {
                ContactoInstitucionDetalles = contactoResult.Select(
                    x => new ContactoInstitucionDetalle(x)
                    ).ToList();
            }

        }


        public int IdInstitucionG { get; set; }
        public string NombreInstitucionG { get; set; }
        public int EncargadoUACI { get; set; }
        public string LogotipoInstitucionG { get; set; }
        public List<ContactoInstitucionDetalle> ContactoInstitucionDetalles { get; set; }

}
}
