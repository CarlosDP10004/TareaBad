using gestioncomprasAPI.Models.Model;
using gestioncomprasAPI.Models.Model.StoredProcedures;
using System;
using System.Linq;
using System.Collections.Generic;

namespace gestioncomprasAPI.Models
{
    public class PersonaDetalle
    {
        public PersonaDetalle()
        {

        }

        public PersonaDetalle(Persona persona, List<SPSLTBContactoPersonaIdResult> contactoResult)
        {
            IdPersona = persona.IdPersona;
            NombrePersona = persona.NombrePersona;
            ApellidoPersona = persona.ApellidoPersona;
            Dui = persona.Dui;
            Nit = persona.Nit;
            Isss = persona.Isss;
            FotoPerfil = persona.FotoPerfil;
            
            ContactoPersonaDetalles = new List<ContactoPersonaDetalle>();
            if (contactoResult != null)
            {
                ContactoPersonaDetalles = contactoResult.Select(
                    x => new ContactoPersonaDetalle(x)
                    ).ToList();
            }
        }
        //public int Id { get; set; }
        public int IdPersona { get; set; }
        public string NombrePersona { get; set; }
        public string ApellidoPersona { get; set; }
        public string Dui { get; set; }
        public string Nit { get; set; }
        public string Isss { get; set; }
        public string FotoPerfil { get; set; }

        public List<ContactoPersonaDetalle> ContactoPersonaDetalles { get; set; }

        //public string DescripcionContacto { get; set; }
        //public string CorreoElectronico { get; set; }

    }
}
