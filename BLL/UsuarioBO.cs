using DAL;
using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class UsuarioBO
    {
        public static Usuario Login(Usuario data)
        {
            return UsuarioDA.Login(data);
        }
    }
}
