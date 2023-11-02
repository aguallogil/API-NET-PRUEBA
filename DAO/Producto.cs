using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class Producto
    {
        public int Id { get; set; }
        public string de_Producto { get; set; }
        public int nu_Cantidad { get; set; }
        public decimal imp_Precio { get; set; }
        public byte[]? img_Producto { get; set; }
        public string? img_Producto64 { get; set; }
        public bool sn_Estatus { get; set; }
    }

}
