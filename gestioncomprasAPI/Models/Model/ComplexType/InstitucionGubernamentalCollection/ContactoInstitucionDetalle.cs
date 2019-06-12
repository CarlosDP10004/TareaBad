using gestioncomprasAPI.Models.Model.StoredProcedures.InstitucionGubernamental;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestioncomprasAPI.Models.Model.ComplexType.InstitucionGubernamentalCollection
{
    public class ContactoInstitucionDetalle
    {
        public ContactoInstitucionDetalle() { }

        public ContactoInstitucionDetalle(SPSLTBContactoInstitucionIdResult contactoResult) {
            IdLista = contactoResult.IdLista;
            IdContactoInstitucion = contactoResult.IdContactoInstitucion;
            NombreLista = contactoResult.NombreLista;
            DescripcionContacto = contactoResult.DescripcionContacto;
        }

        public int Id { get; set; }
        public int IdLista { get; set; }
        public int IdContactoInstitucion { get; set; }
        public string NombreLista { get; set; }
        public string DescripcionContacto { get; set; }
    }
}
