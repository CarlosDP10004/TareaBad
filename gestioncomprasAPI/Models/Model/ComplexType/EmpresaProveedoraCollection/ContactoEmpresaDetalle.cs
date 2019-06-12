using gestioncomprasAPI.Models.Model.StoredProcedures.EmpresaProveedora;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestioncomprasAPI.Models.Model.ComplexType.EmpresaProveedoraCollection
{
    [Serializable]
    public class ContactoEmpresaDetalle
    {
        public ContactoEmpresaDetalle() {
        }

        public ContactoEmpresaDetalle(SPSLTBContactoEmpresaIdResult contactoResult) {
            IdContactoEmpresa = contactoResult.IdContactoEmpresa;
            IdLista = contactoResult.IdLista;
            NombreLista = contactoResult.NombreLista;
            DescripcionContacto = contactoResult.DescripcionContacto;
        }

        public int Id { get; set; }
        public int IdLista { get; set; }
        public int IdContactoEmpresa { get; set; }
        public string NombreLista { get; set; }
        public string DescripcionContacto { get; set; }

    }
}
