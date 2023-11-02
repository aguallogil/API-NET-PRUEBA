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
    public class ProductoDA
    {
        public static List<Producto> GetAll()
        {
            SqlCommand cmd = DataAccess.CrearSQLComando("SP_FAC_Productos");
            DataAccess.ParameterAdd(cmd, "@Opcion", SqlDbType.Int, 4);
            DataTable dt = DataAccess.EjecutarSQLSelect(cmd);
            var list = dt.Rows.Cast<DataRow>().Select(i => new Producto()
            {
                Id = Convert.ToInt32(i["id"]),
                de_Producto = Convert.ToString(i["de_Producto"]),
                nu_Cantidad= Convert.ToInt32(i["nu_Cantidad"]),
                imp_Precio = Convert.ToDecimal(i["imp_Precio"]),
                img_Producto = i["img_Producto"] == DBNull.Value ? null : (byte[])i["img_Producto"],
                sn_Estatus = Convert.ToBoolean(i["sn_Estatus"]),
                img_Producto64= "data:image/jpeg;base64," + ByteArrayToBase64((byte[])i["img_Producto"])
            }).ToList();
            return list;
        }
        public static string ByteArrayToBase64(byte[] byteArray)
        {
            return Convert.ToBase64String(byteArray);
        }

        public static bool UpSert(Producto data)
        {
            SqlCommand cmd = DataAccess.CrearSQLComando("SP_FAC_Productos");
            DataAccess.ParameterAdd(cmd, "@Opcion", SqlDbType.Int, data.Id == 0 ? 1 : 2);
            DataAccess.ParameterAdd(cmd, "@id", SqlDbType.Int, data.Id);
            DataAccess.ParameterAdd(cmd, "@de_Producto", SqlDbType.VarChar, data.de_Producto);
            DataAccess.ParameterAdd(cmd, "@nu_Cantidad", SqlDbType.Int, data.nu_Cantidad);
            DataAccess.ParameterAdd(cmd, "@imp_Precio", SqlDbType.Decimal, data.imp_Precio);
            DataAccess.ParameterAdd(cmd, "@img_Producto", SqlDbType.Image, data.img_Producto);
            DataAccess.ParameterAdd(cmd, "@sn_Estatus", SqlDbType.Bit, data.sn_Estatus);

            return DataAccess.EjecutarSQLNonQuery(cmd) > 0;
        }

        public static Producto Get(int id)
        {
            SqlCommand cmd = DataAccess.CrearSQLComando("SP_FAC_Productos");
            DataAccess.ParameterAdd(cmd, "@Opcion", SqlDbType.Int, 4);
            DataAccess.ParameterAdd(cmd, "@id", SqlDbType.Int, id);
            DataTable dt = DataAccess.EjecutarSQLSelect(cmd);
            var data = dt.Rows.Cast<DataRow>().Select(i => new Producto()
            {
                Id = Convert.ToInt32(i["id"]),
                de_Producto = Convert.ToString(i["de_Producto"]),
                nu_Cantidad = Convert.ToInt32(i["nu_Cantidad"]),
                imp_Precio = Convert.ToDecimal(i["imp_Precio"]),
                sn_Estatus = Convert.ToBoolean(i["sn_Estatus"]),
                img_Producto = i["img_Producto"] == DBNull.Value ? null : (byte[])i["img_Producto"],
                img_Producto64 = "data:image/jpeg;base64," + ByteArrayToBase64((byte[])i["img_Producto"])
            }).FirstOrDefault();
            return data;
        }

        public static bool Delete(int id)
        {
            SqlCommand cmd = DataAccess.CrearSQLComando("SP_FAC_Productos");
            DataAccess.ParameterAdd(cmd, "@Opcion", SqlDbType.Int, 3);
            DataAccess.ParameterAdd(cmd, "@id", SqlDbType.Int, id);
            return DataAccess.EjecutarSQLNonQuery(cmd) > 0;
        }
    }

}
