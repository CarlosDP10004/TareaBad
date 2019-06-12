using gestioncomprasAPI.Models.Model.StoredProcedures.EmpresaProveedora;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestioncomprasAPI.Models.Model.ComplexType.EmpresaProveedoraCollection
{
    public class EmpresaDetalle
    {
        public EmpresaDetalle() {

        }

        public EmpresaDetalle(EmpresaProveedora empresa, List<SPSLTBContactoEmpresaIdResult> contactoResult) {
            IdEmpresaProveedora = empresa.IdEmpresaProveedora;
            NombreEmpresa = empresa.NombreEmpresa;
            DireccionEmpresa = empresa.DireccionEmpresa;
            Responsable = empresa.Responsable;
            Nitempresa = empresa.Nitempresa;
            LogotipoEmpresa = empresa.LogotipoEmpresa;
            MontoPermitido = empresa.MontoPermitido;
            ContactoEmpresaDetalles = new List<ContactoEmpresaDetalle>();
            if (contactoResult != null) {
                ContactoEmpresaDetalles = contactoResult.Select(
                    x => new ContactoEmpresaDetalle(x)
                    ).ToList();
            }
        }

        public int IdEmpresaProveedora { get; set; }
        public string NombreEmpresa { get; set; }
        public string DireccionEmpresa { get; set; }
        public int Responsable { get; set; }
        public string Nitempresa { get; set; }
        public string LogotipoEmpresa { get; set; }
        public decimal? MontoPermitido { get; set; }
        public List<ContactoEmpresaDetalle> ContactoEmpresaDetalles { get; set; }
    }
}
