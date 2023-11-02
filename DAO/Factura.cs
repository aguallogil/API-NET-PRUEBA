using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class Factura
    {
        public int Id { get; set; }
        public DateTime fh_Factura { get; set; }
        public int nu_Factura { get; set; }
        public int id_Cliente { get; set; }
        public int nu_Articulos { get; set; }
        public decimal imp_Subtotal { get; set; }
        public decimal imp_TotalImpuestos { get; set; }
        public decimal imp_Total { get; set; }

        public List<FacturaDetalle> detalles { get; set; }
    }

}
