USE DevLab
GO
create PROCEDURE SP_FAC_Facturas
(
    @Opcion int = -1,
    @id int = -1,
    @fh_Factura date = NULL,
    @nu_Factura int = -1,
    @id_Cliente int = -1,
    @nu_Articulos int = 0,
    @imp_Subtotal decimal(12,2) = 0.00,
    @imp_TotalImpuestos decimal(12,2) = 0.00,
    @imp_Total decimal(12,2) = 0.00,
	@xml xml=''
)
AS
BEGIN
    if(@Opcion = 1) -- Opción para cuando se da de alta
    begin
		--obtenemos el ultimo id porque queremos usar la misma transaccion para meter el encabezado y detalles y este debe pasar el id para los hijos
		declare @id_Nuevo int=(select coalesce(max(id),0)+1 from FAC_Facturas);

		--insertamos el encabezado
        insert into FAC_Facturas(id,fh_Factura, nu_Factura, id_Cliente, nu_Articulos, imp_Subtotal, imp_TotalImpuestos, imp_Total)
        values(@id_Nuevo,@fh_Factura, @nu_Factura, @id_Cliente, @nu_Articulos, @imp_Subtotal, @imp_TotalImpuestos, @imp_Total)

		--obetenemos los detalles por medio del xml
		INSERT INTO FAC_FacturaDetalles
		select
			@id_Nuevo,
			tbl.col.value('id_Producto[1]','int')as id_Producto,
			tbl.col.value('nu_Cantidad[1]','int')as nu_Cantidad,
			tbl.col.value('imp_PrecioUnitario[1]','decimal(12,2)')as imp_PrecioUnitario,
			tbl.col.value('imp_SubTotal[1]','decimal(12,2)')as imp_SubTotal,
			tbl.col.value('notas[1]','varchar(250)')as notas
		 from @xml.nodes('//Container//Detalle')as tbl(col);

    end

    if(@Opcion = 2) -- Opción para actualizar
    begin
        update FAC_Facturas
        set 
            fh_Factura = @fh_Factura,
            nu_Factura = @nu_Factura,
            id_Cliente = @id_Cliente,
            nu_Articulos = @nu_Articulos,
            imp_Subtotal = @imp_Subtotal,
            imp_TotalImpuestos = @imp_TotalImpuestos,
            imp_Total = @imp_Total
        where id = @id
    end

    if(@Opcion = 3) -- Opción para borrar
    begin
        DELETE FROM FAC_Facturas WHERE id = @id
    end

    if(@Opcion = 4) -- Opción para consultas
    begin
        select * from FAC_Facturas
        where id = case @id when -1 then id else @id end
    end
	 if(@Opcion = 5) -- Opción para consultas mas detalladas
    begin
        select 
		* 
		from FAC_Facturas
        where id = case @id when -1 then id else @id end
		and id_Cliente=case @id_Cliente when -1 then id_Cliente else @id_Cliente end
		and nu_Factura= case @nu_Factura when -1 then nu_Factura else @nu_Factura end
    end
END
