using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class Usuario
    {
        public int id_Usuario { get; set; }
        public string de_Usuario { get; set; }
        public string de_Password { get; set; }

        //VARIABLES APRA TOKEN
        public string? Access_Token { get; set; }
        public string? Error_Description { get; set; }
        public DateTime Expire_Date { get; set; }
        public int Expires_In { get; set; }
    }
}
