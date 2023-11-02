using DAL.Helpers;
using DAO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ClienteDA
    {
        public static List<Cliente> GetAll()
        {
            SqlCommand cmd = DataAccess.CrearSQLComando("SP_fac_Clientes");
            DataAccess.ParameterAdd(cmd, "@Opcion", SqlDbType.Int, 4);
            DataTable dt = DataAccess.EjecutarSQLSelect(cmd);
            var list = dt.Rows.Cast<DataRow>().Select(i => new Cliente()
            {
                Id = Convert.ToInt32(i["id"]),
                de_RazonSocial = Convert.ToString(i["de_RazonSocial"]),
                id_TipoCliente = Convert.ToInt32(i["id_TipoCliente"]),
                RFC = Convert.ToString(i["RFC"]),
                fh_Registro = Convert.ToDateTime(i["fh_Registro"]),
                de_TipoCliente=TipoClienteDA.Get(Convert.ToInt32(i["id_TipoCliente"])).de_TipoCliente
            }).ToList();
            return list;
        }

        public static bool UpSert(Cliente data)
        {
            SqlCommand cmd = DataAccess.CrearSQLComando("SP_fac_Clientes");
            DataAccess.ParameterAdd(cmd, "@Opcion", SqlDbType.Int, data.Id == 0 ? 1 : 2);
            DataAccess.ParameterAdd(cmd, "@id", SqlDbType.Int, data.Id);
            DataAccess.ParameterAdd(cmd, "@de_RazonSocial", SqlDbType.VarChar, data.de_RazonSocial);
            DataAccess.ParameterAdd(cmd, "@id_TipoCliente", SqlDbType.Int, data.id_TipoCliente);
            DataAccess.ParameterAdd(cmd, "@RFC", SqlDbType.Char, data.RFC);

            return DataAccess.EjecutarSQLNonQuery(cmd) > 0;
        }

        public static Cliente Get(int id)
        {
            SqlCommand cmd = DataAccess.CrearSQLComando("SP_fac_Clientes");
            DataAccess.ParameterAdd(cmd, "@Opcion", SqlDbType.Int, 4);
            DataAccess.ParameterAdd(cmd, "@id", SqlDbType.Int, id);
            DataTable dt = DataAccess.EjecutarSQLSelect(cmd);
            var data = dt.Rows.Cast<DataRow>().Select(i => new Cliente()
            {
                Id = Convert.ToInt32(i["Id"]),
                de_RazonSocial = Convert.ToString(i["de_RazonSocial"]),
                id_TipoCliente = Convert.ToInt32(i["id_TipoCliente"]),
                RFC = Convert.ToString(i["RFC"]),
                fh_Registro = Convert.ToDateTime(i["fh_Registro"])
            }).FirstOrDefault();
            return data;
        }

        public static bool Delete(int id)
        {
            SqlCommand cmd = DataAccess.CrearSQLComando("SP_fac_Clientes");
            DataAccess.ParameterAdd(cmd, "@Opcion", SqlDbType.Int, 3);
            DataAccess.ParameterAdd(cmd, "@id", SqlDbType.Int, id);
            return DataAccess.EjecutarSQLNonQuery(cmd) > 0;
        }
    }

}
