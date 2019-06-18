using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestioncomprasAPI.Models.Model.StoredProcedures.Producto
{
    public class SPSLTBProductoPorEmpresaResult
    {
        public int Id { get; set; }
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; }
    }
}
