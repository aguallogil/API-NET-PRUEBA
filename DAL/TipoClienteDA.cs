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
    public class TipoClienteDA
    {
        public static List<TipoCliente> GetAll()
        {
            SqlCommand cmd = DataAccess.CrearSQLComando("SP_FAC_TiposClientes");
            DataAccess.ParameterAdd(cmd, "@Opcion", SqlDbType.Int, 4);
            DataTable dt = DataAccess.EjecutarSQLSelect(cmd);
            var list = dt.Rows.Cast<DataRow>().Select(i => new TipoCliente()
            {
                Id = Convert.ToInt32(i["id"]),
                de_TipoCliente = Convert.ToString(i["de_TipoCliente"])
            }).ToList();
            return list;
        }
        public static bool UpSert(TipoCliente data)
        {
            SqlCommand cmd = DataAccess.CrearSQLComando("SP_FAC_TiposClientes");
            DataAccess.ParameterAdd(cmd, "@Opcion", SqlDbType.Int, data.Id == 0 ? 1 : 2);
            DataAccess.ParameterAdd(cmd, "@id", SqlDbType.Int, data.Id);
            DataAccess.ParameterAdd(cmd, "@de_TipoCliente", SqlDbType.VarChar, data.de_TipoCliente);
            return DataAccess.EjecutarSQLNonQuery(cmd) > 0;
        }
        public static TipoCliente Get(int id)
        {
            SqlCommand cmd = DataAccess.CrearSQLComando("SP_FAC_TiposClientes");
            DataAccess.ParameterAdd(cmd, "@Opcion", SqlDbType.Int, 4);
            DataAccess.ParameterAdd(cmd, "@id", SqlDbType.Int, id);
            DataTable dt = DataAccess.EjecutarSQLSelect(cmd);
            var data = dt.Rows.Cast<DataRow>().Select(i => new TipoCliente()
            {
                Id = Convert.ToInt32(i["Id"]),
                de_TipoCliente = Convert.ToString(i["de_TipoCliente"])
            }).FirstOrDefault();
            return data;
        }
        public static bool Delete(int id)
        {
            SqlCommand cmd = DataAccess.CrearSQLComando("SP_FAC_TiposClientes");
            DataAccess.ParameterAdd(cmd, "@Opcion", SqlDbType.Int, 3);
            DataAccess.ParameterAdd(cmd, "@id", SqlDbType.Int, id);
            return DataAccess.EjecutarSQLNonQuery(cmd) > 0;
        }
    }
}
