using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class Articulo
    {
        public int id_Articulo { get; set; }
        public string de_Descripcion { get; set; }
        public decimal imp_Precio { get; set; } 
        public string? img_Imagen { get; set; }
        public int nu_Cantidad { get; set; }
        public int nu_Existencia { get; set; }


    }
}
