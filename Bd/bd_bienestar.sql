--Creación de Base de Datos
CREATE DATABASE db_bienestar;
USE db_bienestar;
go
--Creación de la tabla producto
CREATE TABLE tbl_Product(
	Id INT IDENTITY (1,1) NOT NULL,
	Name NVARCHAR(100)NOT NULL,
	Price MONEY NULL,
	Creation DATETIME2 DEFAULT getdate(),
	Modification DATETIME2 NULL,
	PRIMARY KEY(Id)
)
go

--Procedimiento almacenado lista de productos
CREATE PROCEDURE sp_getProducts
AS
BEGIN
	DECLARE
		@msg VARCHAR(300) = ''
	
	BEGIN TRY

	SELECT *FROM tbl_Product;

	END TRY
	BEGIN CATCH
		IF(LEN(@msg) = 0)
			BEGIN
				SET @msg = (SELECT SUBSTRING(ERROR_MESSAGE(), 1, 300))
			END
		RAISERROR(@msg, 16, 1)
	END CATCH
END
go

--Procedimiento almacenado lista detalle de un producto
CREATE PROCEDURE sp_getProduct(
	@idProduct INT = 0
)
AS
BEGIN
	DECLARE
		@msg VARCHAR(300) = ''
	
	BEGIN TRY
	
	SELECT *FROM tbl_Product WHERE Id=@idProduct;

	END TRY
	BEGIN CATCH
		IF(LEN(@msg) = 0)
			BEGIN
				SET @msg = (SELECT SUBSTRING(ERROR_MESSAGE(), 1, 300))
			END
		RAISERROR(@msg, 16, 1)
	END CATCH
END
go

--Procedimiento almacenado agrega producto
CREATE PROCEDURE sp_insertProduct(
	@Name NVARCHAR(100)='',
	@Price MONEY =0
)
AS
BEGIN
	DECLARE
		@msg VARCHAR(300) = ''
	
	BEGIN TRY
	IF NOT EXISTS ( SELECT 1 FROM tbl_Product WHERE Name = @Name)
	BEGIN
		INSERT INTO tbl_Product (Name, Price) VALUES (@Name,@Price);
		SELECT *FROM tbl_Product WHERE Name=@Name;
	END
	BEGIN
	SELECT 0 AS Id;
	END 

	
	END TRY
	BEGIN CATCH
		IF(LEN(@msg) = 0)
			BEGIN
				SET @msg = (SELECT SUBSTRING(ERROR_MESSAGE(), 1, 300))
			END
		RAISERROR(@msg, 16, 1)
	END CATCH
END

go

--Procedimiento almacenado actualiza producto
CREATE PROCEDURE sp_updateProduct(
	@Id INT =0,
	@Name NVARCHAR(100)='',
	@Price MONEY =0
)
AS
BEGIN
	DECLARE
		@msg VARCHAR(300) = ''
	
	BEGIN TRY

	IF EXISTS ( SELECT 1 FROM tbl_Product WHERE Id = @Id)
	BEGIN
		UPDATE tbl_Product SET Name=@Name, Price=@Price, Modification= getdate() WHERE Id=@Id;
		SELECT *FROM tbl_Product WHERE Id=@Id;
	END
	BEGIN
	SELECT 0 AS Id;
	END 
	

	END TRY
	BEGIN CATCH
		IF(LEN(@msg) = 0)
			BEGIN
				SET @msg = (SELECT SUBSTRING(ERROR_MESSAGE(), 1, 300))
			END
		RAISERROR(@msg, 16, 1)
	END CATCH
END
go
--Procedimiento almacenado elimina producto
CREATE PROCEDURE sp_deleteProduct(
	@Id INT =0
)
AS
BEGIN
	DECLARE
		@msg VARCHAR(300) = ''
	
	BEGIN TRY

	IF EXISTS ( SELECT 1 FROM tbl_Product WHERE Id = @Id)
	BEGIN
		DELETE FROM tbl_Product WHERE Id=@Id;
		SELECT 1 AS Mensaje;
	END
	ELSE
	BEGIN
		SELECT 0 AS Mensaje;
	END

	END TRY
	BEGIN CATCH
		IF(LEN(@msg) = 0)
			BEGIN
				SET @msg = (SELECT SUBSTRING(ERROR_MESSAGE(), 1, 300))
			END
		RAISERROR(@msg, 16, 1)
	END CATCH
END
go
exec sp_insertProduct 'Durazno Chico',12.00;
exec sp_updateProduct 15,'Durazno Grande',16.00;
exec sp_deleteProduct 14
exec sp_getProducts
exec sp_getProduct 5