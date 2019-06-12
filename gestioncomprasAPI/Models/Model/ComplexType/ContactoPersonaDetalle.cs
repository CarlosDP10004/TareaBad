using gestioncomprasAPI.Models.Model.StoredProcedures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestioncomprasAPI.Models.Model
{
    [Serializable]
    public class ContactoPersonaDetalle
    {
        public ContactoPersonaDetalle()
        {

        }

        public ContactoPersonaDetalle(SPSLTBContactoPersonaIdResult contactoResult)
        {
            IdContactoPersona = contactoResult.IdContactoPersona;
            NombreLista = contactoResult.NombreLista;
            DescripcionContacto = contactoResult.DescripcionContacto;
            IdLista = contactoResult.IdLista;
        }
        public int Id { get; set; }
        public int IdLista { get; set; }
        public int IdContactoPersona { get; set; }
        public string NombreLista { get; set; }
        public string DescripcionContacto { get; set; }
    }
}
