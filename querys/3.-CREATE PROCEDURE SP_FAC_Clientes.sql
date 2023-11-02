USE DevLab
GO
create PROCEDURE SP_FAC_Clientes
(
    @Opcion INT = -1,
    @id int = -1,
    @de_RazonSocial VARCHAR(250) = '',
    @id_TipoCliente INT = -1,
    @RFC CHAR(13) = '',
    @fh_Registro DATETIME = NULL
)
AS
BEGIN
    IF (@Opcion = 1) -- Opción para dar de alta un nuevo cliente
    BEGIN
        INSERT INTO FAC_Clientes (de_RazonSocial, id_TipoCliente, RFC, fh_Registro)
        VALUES (@de_RazonSocial, @id_TipoCliente, @RFC, GETDATE())
    END
    ELSE IF (@Opcion = 2) -- Opción para actualizar un cliente existente
    BEGIN
        UPDATE FAC_Clientes
        SET de_RazonSocial = @de_RazonSocial,
            id_TipoCliente = @id_TipoCliente,
            RFC = @RFC
        WHERE id = @id
    END
    ELSE IF (@Opcion = 3) -- Opción para borrar un cliente
    BEGIN
        -- Asegúrate de manejar las relaciones antes de borrar
        -- Por ejemplo, si hay relaciones con facturas u otros registros relacionados.
        DELETE FROM FAC_Clientes WHERE id = @id
    END
    ELSE IF (@Opcion = 4) -- Opción para consultar clientes
    BEGIN
        SELECT *
        FROM FAC_Clientes
        WHERE id = CASE @id WHEN -1 THEN id ELSE @id END
    END
END
