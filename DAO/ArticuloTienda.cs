using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class ArticuloTienda
    {
        public int id_ArticuloTienda { get; set; }
        public int id_Articulo { get; set; }
        public int id_Tienda { get;set; }
        public int nu_Cantidad { get; set; }
        public DateTime fh_Fecha { get; set; }  
    }
}
