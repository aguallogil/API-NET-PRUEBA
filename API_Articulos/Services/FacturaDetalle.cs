namespace PruebaAPI.Services
{
    public class FacturaDetalle
    {
        public int Id { get; set; }
        public int Id_Factura { get; set; }
        public int Id_Producto { get; set; }
        public int Nu_Cantidad { get; set; }
        public decimal Imp_PrecioUnitario { get; set; }
        public decimal Imp_SubTotal { get; set; }
        public decimal Notas { get; set; }
    }

}
