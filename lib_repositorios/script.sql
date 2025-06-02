CREATE DATABASE db_ArancelesDef;
GO
USE db_ArancelesDef;
GO

CREATE TABLE [Paises] 
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[Nombre] NVARCHAR(100) NOT NULL,
	[Moneda] NVARCHAR(300) NOT NULL,
);
GO
CREATE TABLE [Empresas](
	[Id] INT PRIMARY KEY IDENTITY (1,1) NOT NULL,
	[Nombre] NVARCHAR(50) not null,
	[Id_Pais] INT not null,
	FOREIGN KEY ([Id_Pais]) REFERENCES [Paises] ([Id])
);
GO
CREATE TABLE [TiposDeAranceles]
(
	[Id] INT PRIMARY KEY IDENTITY (1,1) NOT NULL,
    [Nombre] NVARCHAR (90) NOT NULL,
    [FechaVigencia] SMALLDATETIME NOT NULL,
)
GO
CREATE TABLE [TiposDeProductos] --Tabla 4
(
	[Id] INT PRIMARY KEY IDENTITY (1,1) NOT NULL,
    [Nombre] NVARCHAR (90) NOT NULL,
    [EntidadRegulatoria] NVARCHAR (90) NOT NULL
)
GO
CREATE TABLE [Productos] --Tabla 3
(
	--5 Atributos
	[Id] INT PRIMARY KEY IDENTITY (1,1) NOT NULL,
	[Id_Empresa]  INT FOREIGN KEY ([Id_Empresa] ) REFERENCES [Empresas] (Id),
    [Id_TipoProducto] INT FOREIGN KEY ([Id_TipoProducto] ) REFERENCES [TiposDeProductos] (Id),
    [Nombre] NVARCHAR (100) NOT NULL,
    [PrecioUnitario] DECIMAL (15,2) NOT NULL
)
GO
CREATE TABLE [Ordenes] --Tabla 5
(
	--7 Atributos

	[Id] INT PRIMARY KEY IDENTITY (1,1) NOT NULL,
	[Id_PaisOrigen]  INT FOREIGN KEY ([Id_PaisOrigen] ) REFERENCES [Paises] (Id),
	[Id_PaisDestino]  INT FOREIGN KEY ([Id_PaisDestino] ) REFERENCES [Paises] (Id),
	[Id_Producto]  INT FOREIGN KEY ([Id_Producto] ) REFERENCES [Productos] (Id),
	[Cod] NVARCHAR (90) NOT NULL,
    [CantidadUnidades] INT NOT NULL,
    [Fecha] SMALLDATETIME NOT NULL
)
GO
CREATE TABLE [Aranceles] --Tabla 7
(
	--4 Atributos
	[Id] INT PRIMARY KEY IDENTITY (1,1) NOT NULL,
	[Cod] NVARCHAR (90) NOT NULL,
	[Id_Orden]  INT FOREIGN KEY ([Id_Orden] ) REFERENCES [Ordenes] (Id),
	[Id_TipoDeArancel]  INT FOREIGN KEY ([Id_TipoDeArancel] ) REFERENCES [TiposDeAranceles] (Id),
    [PorcentajeDelArancel] DECIMAL (15,2) NOT NULL
)
GO
CREATE TABLE [Facturas] --Tabla 8
(
	--4 Atributos

	[Id] INT PRIMARY KEY IDENTITY (1,1) NOT NULL,
	[Cod] NVARCHAR (90) NOT NULL,
	[Id_Arancel]  INT FOREIGN KEY ([Id_Arancel] ) REFERENCES [Aranceles] (Id),
    [Fecha] SMALLDATETIME NOT NULL,
    [PagoTotalCop] DECIMAL (15,2) NOT NULL
)
GO
------------------------------------------------------------------------------------------
INSERT INTO [Paises] ([Nombre],[Moneda])
VALUES ('China', 'Yuan');
GO
INSERT INTO [Empresas] ([Nombre],[Id_Pais])
VALUES ('Nike', 3); --Id_Pais : 1
GO
INSERT INTO [TiposDeAranceles] ([Nombre],[FechaVigencia])
VALUES ('Ad Valorem', GETDATE()); 
GO
INSERT INTO [TiposDeProductos] ([Nombre],[EntidadRegulatoria])
VALUES ('Alimentos','Invima'); 
GO
INSERT INTO [Productos] ([Nombre],[PrecioUnitario],[Id_Empresa],[Id_TipoProducto])
VALUES ('Sudadera',65000,2,1); --Recuerda colocar 1,1
GO
INSERT INTO [Ordenes] ([Cod],[CantidadUnidades],[Fecha],[Id_PaisDestino],[Id_PaisOrigen],[Id_Producto])
VALUES ('ABC001',321,GETDATE(),3,4,2); --Recuerda colocar 1,1
GO
INSERT INTO [Aranceles] ([Cod],[Id_TipoDeArancel],[Id_Orden],[PorcentajeDelArancel])
VALUES ('BFG212',1,3,13); --Recuerda colocar 1,1
GO
INSERT INTO [Facturas] ([Cod],[Id_Arancel],[PagoTotalCop],[Fecha])
VALUES ('AFA123',1,34123123123,GETDATE()); --Recuerda colocar 1,1
GO
------------------------------------------------------------------------------------------
SELECT * FROM [Paises];
SELECT * FROM [Empresas];
SELECT * FROM [TiposDeAranceles];
SELECT * FROM [TiposDeProductos];
SELECT * FROM [Productos];
SELECT * FROM [Ordenes];
SELECT * FROM [Aranceles];
SELECT * FROM [Facturas];
----------------------------------------------------------------------------------------------
CREATE TABLE [Roles]
(
	[Id] INT PRIMARY KEY IDENTITY (1,1) NOT NULL,
	[Cod] NVARCHAR (90) NOT NULL,
	[Nombre]  NVARCHAR (90) NOT NULL
)
GO
CREATE TABLE [Usuarios]
(
	[Id] INT PRIMARY KEY IDENTITY (1,1) NOT NULL,
	[Cod] NVARCHAR (90) NOT NULL,
	[Usuario] NVARCHAR (90) NOT NULL,
	[Contraseña] NVARCHAR (90) NOT NULL,
	[Id_Rol]  INT FOREIGN KEY ([Id_Rol] ) REFERENCES [Roles] (Id),
)
GO
CREATE TABLE [Auditorias]
(
	[Id] INT PRIMARY KEY IDENTITY (1,1) NOT NULL,
	[Cod] NVARCHAR (90) NOT NULL,
	[Accion] NVARCHAR (90) NOT NULL,
	[Entidad] NVARCHAR (90) NOT NULL,
	[Informacion] NVARCHAR (90) NOT NULL,
	[Id_Usuario]  INT FOREIGN KEY ([Id_Usuario] ) REFERENCES [Usuarios] (Id),
	[Fecha] SMALLDATETIME  NOT NULL,
)
GO
-----------------------
INSERT INTO [Roles] ([Nombre],[Cod])
VALUES ('Administrador', 'ADM');
GO
INSERT INTO [Usuarios] ([Usuario],[Contraseña],[Cod],[Id_Rol])
VALUES ('Admin', '123','ADM001',1);
GO
INSERT INTO [Auditorias] ([Cod],[Accion],[Entidad],[Informacion],[Id_Usuario],[Fecha])
VALUES ('Prueba', 'Guardar','Paises','Colombia/PesoColombiano',1,GETDATE());
GO
-----------------------
SELECT * FROM [Roles];
SELECT * FROM [Usuarios];
SELECT * FROM [Auditorias];
