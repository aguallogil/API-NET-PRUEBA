USE DevLab
GO
CREATE PROCEDURE SP_FAC_TiposClientes
(
    @Opcion INT = -1,
    @id INT = -1,
    @de_TipoCliente VARCHAR(250) = ''
)
AS
BEGIN
    IF (@Opcion = 1) -- Opci�n para dar de alta un nuevo tipo de cliente
    BEGIN
        INSERT INTO FAC_TiposClientes (de_TipoCliente)
        VALUES (@de_TipoCliente)
    END
    ELSE IF (@Opcion = 2) -- Opci�n para actualizar un tipo de cliente existente
    BEGIN
        UPDATE FAC_TiposClientes
        SET de_TipoCliente = @de_TipoCliente
        WHERE id = @id
    END
    ELSE IF (@Opcion = 3) -- Opci�n para borrar un tipo de cliente
    BEGIN
        -- Aseg�rate de manejar las relaciones antes de borrar
        -- Por ejemplo, si hay relaciones con clientes que usan este tipo.
        DELETE FROM FAC_TiposClientes WHERE id = @id
    END
    ELSE IF (@Opcion = 4) -- Opci�n para consultar tipos de clientes
    BEGIN
        SELECT *
        FROM FAC_TiposClientes
        WHERE id = CASE @id WHEN -1 THEN id ELSE @id END
    END
END
