USE DevLab
GO
create PROCEDURE SP_FAC_Productos
(
    @Opcion int = -1,
    @id int = -1,
    @de_Producto varchar(250) = '',
	@nu_Cantidad int=-1,
    @imp_Precio decimal(12,2) = 0.00,
	@img_Producto image=null,
    @sn_Estatus bit = 0
)
AS
BEGIN
    if(@Opcion = 1) -- Opción para cuando se da de alta
    begin
        insert into FAC_Productos(de_Producto,nu_Cantidad, imp_Precio,img_Producto, sn_Estatus)
        values(@de_Producto,@nu_Cantidad, @imp_Precio,@img_Producto, @sn_Estatus)
    end

    if(@Opcion = 2) -- Opción para actualizar
    begin
        update FAC_Productos
        set 
            de_Producto = @de_Producto,
			nu_Cantidad=@nu_Cantidad,
            imp_Precio = @imp_Precio,
            sn_Estatus = @sn_Estatus,
			img_Producto=@img_Producto
        where id = @id
    end

    if(@Opcion = 3) -- Opción para borrar
    begin
        DELETE FROM FAC_Productos WHERE id = @id
    end

    if(@Opcion = 4) -- Opción para consultas
    begin
        select * from FAC_Productos
        where id = case @id when -1 then id else @id end
    end
END
