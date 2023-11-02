using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class ClienteArticulo
    {
        public int id_ClienteArticulo { get; set; }
        public int id_Cliente { get;set; }
        public int id_Articulo { get; set; }
        public int id_Tienda { get; set; }
        public int nu_Cantidad { get; set; }
        public DateTime fh_Fecha { get; set; }

        public decimal imp_Precio { get; set; }
        public string? img_Imagen { get; set; }
    }
}
