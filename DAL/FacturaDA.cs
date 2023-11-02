using DAL.Helpers;
using DAO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DAL
{
    public class FacturaDA
    {
        public static List<Factura> GetAll()
        {
            SqlCommand cmd = DataAccess.CrearSQLComando("SP_FAC_Facturas");
            DataAccess.ParameterAdd(cmd, "@Opcion", SqlDbType.Int, 4);
            DataTable dt = DataAccess.EjecutarSQLSelect(cmd);
            var list = dt.Rows.Cast<DataRow>().Select(i => new Factura()
            {
                Id = Convert.ToInt32(i["id"]),
                fh_Factura = Convert.ToDateTime(i["fh_Factura"]),
                nu_Factura = Convert.ToInt32(i["nu_Factura"]),
                id_Cliente = Convert.ToInt32(i["id_Cliente"]),
                nu_Articulos = Convert.ToInt32(i["nu_Articulos"]),
                imp_Subtotal = Convert.ToDecimal(i["imp_Subtotal"]),
                imp_TotalImpuestos = Convert.ToDecimal(i["imp_TotalImpuestos"]),
                imp_Total = Convert.ToDecimal(i["imp_Total"])
            }).ToList();
            return list;
        }
        public static List<Factura> GetAll(int id, int numero)
        {
            SqlCommand cmd = DataAccess.CrearSQLComando("SP_FAC_Facturas");
            DataAccess.ParameterAdd(cmd, "@Opcion", SqlDbType.Int, 5);
            DataAccess.ParameterAdd(cmd, "@id_Cliente", SqlDbType.Int, id);
            DataAccess.ParameterAdd(cmd, "@nu_Factura", SqlDbType.Int, numero);
            DataTable dt = DataAccess.EjecutarSQLSelect(cmd);
            var list = dt.Rows.Cast<DataRow>().Select(i => new Factura()
            {
                Id = Convert.ToInt32(i["id"]),
                fh_Factura = Convert.ToDateTime(i["fh_Factura"]),
                nu_Factura = Convert.ToInt32(i["nu_Factura"]),
                id_Cliente = Convert.ToInt32(i["id_Cliente"]),
                nu_Articulos = Convert.ToInt32(i["nu_Articulos"]),
                imp_Subtotal = Convert.ToDecimal(i["imp_Subtotal"]),
                imp_TotalImpuestos = Convert.ToDecimal(i["imp_TotalImpuestos"]),
                imp_Total = Convert.ToDecimal(i["imp_Total"])
            }).ToList();
            return list;
        }

        public static bool UpSert(Factura data, XmlDocument xml)
        {
            SqlCommand cmd = DataAccess.CrearSQLComando("SP_FAC_Facturas");
            DataAccess.ParameterAdd(cmd, "@Opcion", SqlDbType.Int, 1);
            DataAccess.ParameterAdd(cmd, "@id", SqlDbType.Int, data.Id);
            DataAccess.ParameterAdd(cmd, "@fh_Factura", SqlDbType.Date, data.fh_Factura);
            DataAccess.ParameterAdd(cmd, "@nu_Factura", SqlDbType.Int, data.nu_Factura);
            DataAccess.ParameterAdd(cmd, "@id_Cliente", SqlDbType.Int, data.id_Cliente);
            DataAccess.ParameterAdd(cmd, "@nu_Articulos", SqlDbType.Int, data.nu_Articulos);
            DataAccess.ParameterAdd(cmd, "@imp_Subtotal", SqlDbType.Decimal, data.imp_Subtotal);
            DataAccess.ParameterAdd(cmd, "@imp_TotalImpuestos", SqlDbType.Decimal, data.imp_TotalImpuestos);
            DataAccess.ParameterAdd(cmd, "@imp_Total", SqlDbType.Decimal, data.imp_Total);
            DataAccess.ParameterAdd(cmd, "@xml", SqlDbType.Xml, xml.InnerXml);
            return DataAccess.EjecutarSQLNonQuery(cmd) > 0;
        }

        public static Factura Get(int id)
        {
            SqlCommand cmd = DataAccess.CrearSQLComando("SP_FAC_Facturas");
            DataAccess.ParameterAdd(cmd, "@Opcion", SqlDbType.Int, 4);
            DataAccess.ParameterAdd(cmd, "@id", SqlDbType.Int, id);
            DataTable dt = DataAccess.EjecutarSQLSelect(cmd);
            var data = dt.Rows.Cast<DataRow>().Select(i => new Factura()
            {
                Id = Convert.ToInt32(i["id"]),
                fh_Factura = Convert.ToDateTime(i["fh_Factura"]),
                nu_Factura = Convert.ToInt32(i["nu_Factura"]),
                id_Cliente = Convert.ToInt32(i["id_Cliente"]),
                nu_Articulos = Convert.ToInt32(i["nu_Articulos"]),
                imp_Subtotal = Convert.ToDecimal(i["imp_Subtotal"]),
                imp_TotalImpuestos = Convert.ToDecimal(i["imp_TotalImpuestos"]),
                imp_Total = Convert.ToDecimal(i["imp_Total"])
            }).FirstOrDefault();
            return data;
        }

        public static bool Delete(int id)
        {
            SqlCommand cmd = DataAccess.CrearSQLComando("SP_FAC_Facturas");
            DataAccess.ParameterAdd(cmd, "@Opcion", SqlDbType.Int, 3);
            DataAccess.ParameterAdd(cmd, "@id", SqlDbType.Int, id);
            return DataAccess.EjecutarSQLNonQuery(cmd) > 0;
        }
    }

}
