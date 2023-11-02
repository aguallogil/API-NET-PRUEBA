using DAL.Helpers;
using DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UsuarioDA
    {
        public static Usuario Login(Usuario data)
        {
            Usuario user = null;
            SqlCommand cmd = DataAccess.CrearSQLComando("SP_Usuarios");
            DataAccess.ParameterAdd(cmd, "@Opcion", SqlDbType.Int, 4);
            DataAccess.ParameterAdd(cmd, "@de_Usuario", SqlDbType.VarChar, data.de_Usuario);
            DataAccess.ParameterAdd(cmd, "@de_Password", SqlDbType.VarChar, data.de_Password);
            DataTable dt = DataAccess.EjecutarSQLSelect(cmd);
            if (dt.Rows.Count > 0)
            {
                user = new Usuario
                {
                    id_Usuario = Convert.ToInt32(dt.Rows[0]["id_Usuario"]),
                    de_Usuario= Convert.ToString(dt.Rows[0]["de_Usuario"]),
                    de_Password=Convert.ToString(dt.Rows[0]["de_Password"]),
                };
            }
            return user;
         }
    }
}
