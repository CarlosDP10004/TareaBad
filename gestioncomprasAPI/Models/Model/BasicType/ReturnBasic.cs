using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestioncomprasAPI.Models.Model.BasicType
{
    public class ReturnBasic
    {
        public ReturnBasic(string value) {
            Mensaje = value;
        }
        public string Mensaje { get; set; }
    }
}
