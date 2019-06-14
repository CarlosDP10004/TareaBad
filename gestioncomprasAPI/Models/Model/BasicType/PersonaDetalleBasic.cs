using gestioncomprasAPI.Models.Model;
using gestioncomprasAPI.Models.Model.StoredProcedures;
using System;
using System.Linq;
using System.Collections.Generic;

namespace gestioncomprasAPI.Models.Model.BasicType

{
    public class PersonaDetalleBasic
    {
        public PersonaDetalleBasic()
        {

        }
        public string NombrePersona { get; set; }
        public string ApellidoPersona { get; set; }
        public string Dui { get; set; }
        public string Nit { get; set; }
        public string Isss { get; set; }
        public string FotoPerfil { get; set; }
        public string TelefonoFijo { get; set; }
        public string TelefonoMovil { get; set; }
        public string CorreoElectronico { get; set; }
        public int UsuarioSession { get; set; }

    }
}
