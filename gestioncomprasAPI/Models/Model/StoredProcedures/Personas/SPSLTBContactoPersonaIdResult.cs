﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestioncomprasAPI.Models.Model.StoredProcedures
{
    public class SPSLTBContactoPersonaIdResult
    {
        public int Id { get; set; }
        public int IdLista { get; set; }
        public int IdContactoPersona { get; set; }
        public string NombreLista { get; set; }
        public string DescripcionContacto { get; set; }
    }
}
