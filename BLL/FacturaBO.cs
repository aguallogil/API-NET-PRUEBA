using DAL.Helpers;
using DAL;
using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace BLL
{
    public class FacturaBO
    {
        public static async Task<Response> UpSert(Factura data)
        {
            var xml = CreateXmlFromDetails(data.detalles);
            string _message = string.Empty;
            int _statusCode = 0;
            DataAccess.TransactionWrapper(() =>
            {
                var result = FacturaDA.UpSert(data,xml);
                if (result)
                {
                    _message = "Guardado correctamente.";
                    _statusCode = 200;
                }
                else
                {
                    _message = "Hubo un problema al guardar.";
                    _statusCode = 409;
                }
            });

            return new Response()
            {
                StatusCode = _statusCode,
                Message = _message
            };
        }

        public static List<Factura> GetAll()
        {
            return FacturaDA.GetAll();
        }
        public static List<Factura> GetAll(int id, int numero)
        {
            return FacturaDA.GetAll(id,numero);
        }

        public static Factura Get(int id)
        {
            return FacturaDA.Get(id);
        }

        public static async Task<Response> Delete(int id)
        {
            string _message = string.Empty;
            int _statusCode = 0;
            DataAccess.TransactionWrapper(() =>
            {
                var result = FacturaDA.Delete(id);
                if (result)
                {
                    _message = "Se eliminó correctamente";
                    _statusCode = 200;
                }
                else
                {
                    _message = "Hubo un problema al eliminar";
                    _statusCode = 409;
                }
            });

            return new Response()
            {
                StatusCode = _statusCode,
                Message = _message
            };
        }
        public static XmlDocument CreateXmlFromDetails(List<FacturaDetalle> detalles)
        {
            XmlDocument xmlDoc = new XmlDocument();

            // Crear nodo raíz
            XmlNode containerNode = xmlDoc.CreateElement("Container");

            // Iterar sobre cada detalle y añadir al nodo raíz
            foreach (var detalle in detalles)
            {
                XmlNode detalleNode = xmlDoc.CreateElement("Detalle");

                XmlNode idProductoNode = xmlDoc.CreateElement("id_Producto");
                idProductoNode.InnerText = detalle.Id_Producto.ToString();
                detalleNode.AppendChild(idProductoNode);

                XmlNode nuCantidadNode = xmlDoc.CreateElement("nu_Cantidad");
                nuCantidadNode.InnerText = detalle.Nu_Cantidad.ToString();
                detalleNode.AppendChild(nuCantidadNode);

                XmlNode precioUnitarioNode = xmlDoc.CreateElement("imp_PrecioUnitario");
                precioUnitarioNode.InnerText = detalle.Imp_PrecioUnitario.ToString();
                detalleNode.AppendChild(precioUnitarioNode);

                XmlNode subTotalNode = xmlDoc.CreateElement("imp_SubTotal");
                subTotalNode.InnerText = detalle.Imp_SubTotal.ToString();
                detalleNode.AppendChild(subTotalNode);

                XmlNode notasNode = xmlDoc.CreateElement("notas");
                notasNode.InnerText = detalle.Notas;
                detalleNode.AppendChild(notasNode);

                containerNode.AppendChild(detalleNode);
            }

            xmlDoc.AppendChild(containerNode);
            return xmlDoc;
        }

    }

}
