using System;
using System.Collections.Generic;

namespace gestioncomprasAPI.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            Bitacora = new HashSet<Bitacora>();
            InstitucionGubernamental = new HashSet<InstitucionGubernamental>();
            UsuarioRol = new HashSet<UsuarioRol>();
        }

        public int Idusuario { get; set; }
        public string CorreoElectronico { get; set; }
        public string Clave { get; set; }
        public int IdPersona { get; set; }
        public bool Estado { get; set; }
        public int Intento { get; set; }

        public Persona IdPersonaNavigation { get; set; }
        public ICollection<Bitacora> Bitacora { get; set; }
        public ICollection<InstitucionGubernamental> InstitucionGubernamental { get; set; }
        public ICollection<UsuarioRol> UsuarioRol { get; set; }
    }
}
