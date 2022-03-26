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
