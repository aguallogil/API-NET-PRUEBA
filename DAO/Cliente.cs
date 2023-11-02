using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{

    public class Cliente
    {
        public int Id { get; set; }
        public string de_RazonSocial { get; set; }
        public int id_TipoCliente { get; set; }
        public string RFC { get; set; }
        public DateTime fh_Registro { get; set; }

        public string? de_TipoCliente { get; set; }
    }

}
