USE DevLab
GO
CREATE PROCEDURE SP_Usuarios
(
    @Opcion INT = -1,
    @id_Usuario INT = -1,
    @de_Usuario VARCHAR(255) = '',
    @de_Password VARCHAR(255) = ''
)
AS
BEGIN
    IF(@Opcion = 1) -- Opción para cuando se da de alta
    BEGIN
        INSERT INTO Usuarios(de_Usuario, de_Password)
        VALUES(@de_Usuario, @de_Password);
    END

    IF(@Opcion = 2) -- Opción para actualizar
    BEGIN
        UPDATE Usuarios
        SET 
            de_Usuario = @de_Usuario,
            de_Password = @de_Password
        WHERE id_Usuario = @id_Usuario;
    END

    IF(@Opcion = 3) -- Opción para borrar
    BEGIN
        DELETE FROM Usuarios WHERE id_Usuario = @id_Usuario;
    END

    IF(@Opcion = 4) -- Opción para consultas
    BEGIN
        SELECT * FROM Usuarios
        WHERE id_Usuario = CASE @id_Usuario WHEN -1 THEN id_Usuario ELSE @id_Usuario END
		and de_Usuario = CASE @de_Usuario WHEN '' THEN de_Usuario ELSE @de_Usuario END
		and de_Password = CASE @de_Password WHEN '' THEN de_Password ELSE @de_Password END
    END
END
