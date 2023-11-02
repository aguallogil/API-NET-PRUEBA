using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class FacturaDetalle
    {
        public int Id { get; set; }
        public int Id_Factura { get; set; }
        public int Id_Producto { get; set; }
        public int Nu_Cantidad { get; set; }
        public decimal Imp_PrecioUnitario { get; set; }
        public decimal Imp_SubTotal { get; set; }
        public string Notas { get; set; }
    }

}
