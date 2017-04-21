ALTER TABLE T_ComboVenta DROP CONSTRAINT FK_T_ComboVenta7
GO
ALTER TABLE T_ComboVenta DROP CONSTRAINT FK_T_ComboVenta6
GO
ALTER TABLE T_1 DROP CONSTRAINT FK_T_14
GO
ALTER TABLE T_1 DROP CONSTRAINT FK_T_15
GO
ALTER TABLE T_InformacionNutricional DROP CONSTRAINT FK_T_InformacionNutricional8
GO
ALTER TABLE T_ArticuloProducto DROP CONSTRAINT FK_T_ArticuloProducto0
GO
ALTER TABLE T_ArticuloProducto DROP CONSTRAINT FK_T_ArticuloProducto1
GO
ALTER TABLE T_0 DROP CONSTRAINT FK_T_02
GO
ALTER TABLE T_0 DROP CONSTRAINT FK_T_03
GO
IF EXISTS (SELECT name FROM sysobjects WHERE name = 'T_Articulo' AND type = 'U')
	DROP TABLE T_Articulo
GO
IF EXISTS (SELECT name FROM sysobjects WHERE name = 'T_ComboVenta' AND type = 'U')
	DROP TABLE T_ComboVenta
GO
IF EXISTS (SELECT name FROM sysobjects WHERE name = 'T_1' AND type = 'U')
	DROP TABLE T_1
GO
IF EXISTS (SELECT name FROM sysobjects WHERE name = 'T_InformacionNutricional' AND type = 'U')
	DROP TABLE T_InformacionNutricional
GO
IF EXISTS (SELECT name FROM sysobjects WHERE name = 'T_Combo' AND type = 'U')
	DROP TABLE T_Combo
GO
IF EXISTS (SELECT name FROM sysobjects WHERE name = 'T_ArticuloProducto' AND type = 'U')
	DROP TABLE T_ArticuloProducto
GO
IF EXISTS (SELECT name FROM sysobjects WHERE name = 'T_Carta' AND type = 'U')
	DROP TABLE T_Carta
GO
IF EXISTS (SELECT name FROM sysobjects WHERE name = 'T_Venta' AND type = 'U')
	DROP TABLE T_Venta
GO
IF EXISTS (SELECT name FROM sysobjects WHERE name = 'T_Producto' AND type = 'U')
	DROP TABLE T_Producto
GO
IF EXISTS (SELECT name FROM sysobjects WHERE name = 'T_0' AND type = 'U')
	DROP TABLE T_0
GO
CREATE TABLE T_Carta (
	codCarta INT IDENTITY NOT NULL,
	estado BIT NOT NULL,
	fechaBaja SMALLINT NOT NULL,
	CONSTRAINT PK_T_Carta2 PRIMARY KEY NONCLUSTERED (codCarta)
	)
GO
CREATE TABLE T_Combo (
	codCombo INT NOT NULL,
	descripcion VARCHAR ( 255 ) NOT NULL,
	precio SMALLINT NOT NULL,
	CONSTRAINT PK_T_Combo3 PRIMARY KEY NONCLUSTERED (codCombo)
	)
GO
CREATE TABLE T_ComboVenta (
	cantidad INT NOT NULL,
	monto SMALLINT NOT NULL,
	codVenta INT NOT NULL,
	codCombo INT NOT NULL,
	CONSTRAINT PK_T_ComboVenta7 PRIMARY KEY NONCLUSTERED (codVenta, codCombo)
	)
GO
CREATE INDEX TC_T_ComboVenta7 ON T_ComboVenta (codCombo )
GO
CREATE INDEX TC_T_ComboVenta6 ON T_ComboVenta (codVenta )
GO
CREATE TABLE T_ArticuloProducto (
	cantidad INT NOT NULL,
	costo SMALLINT NOT NULL,
	codProducto INT NOT NULL,
	codArticulo INT NOT NULL,
	CONSTRAINT PK_T_ArticuloProducto6 PRIMARY KEY NONCLUSTERED (codProducto, codArticulo)
	)
GO
CREATE INDEX TC_T_ArticuloProducto1 ON T_ArticuloProducto (codArticulo )
GO
CREATE INDEX TC_T_ArticuloProducto0 ON T_ArticuloProducto (codProducto )
GO
CREATE TABLE T_Articulo (
	codArticulo INT IDENTITY NOT NULL,
	nombre VARCHAR ( 255 ) NOT NULL,
	descripcion VARCHAR ( 255 ) NOT NULL,
	unidadMedida VARCHAR ( 255 ) NOT NULL,
	tipoArticulo VARCHAR ( 255 ) NOT NULL,
	costoxUM NUMERIC ( 13, 2 ) NOT NULL,
	CONSTRAINT PK_T_Articulo5 PRIMARY KEY NONCLUSTERED (codArticulo)
	)
GO
CREATE TABLE T_InformacionNutricional (
	codigoInfNut INT IDENTITY NOT NULL,
	calorias SMALLINT NOT NULL,
	proteinas SMALLINT NOT NULL,
	carbohidratos SMALLINT NOT NULL,
	grasas SMALLINT NOT NULL,
	codArticulo INT NOT NULL,
	CONSTRAINT TC_T_InformacionNutricional9 UNIQUE NONCLUSTERED (codArticulo),
	CONSTRAINT PK_T_InformacionNutricional0 PRIMARY KEY NONCLUSTERED (codArticulo, codigoInfNut)
	)
GO
CREATE INDEX TC_T_InformacionNutricional10 ON T_InformacionNutricional (codArticulo )
GO
CREATE TABLE T_1 (
	codCombo INT NOT NULL,
	codProducto INT NOT NULL,
	CONSTRAINT PK_T_19 PRIMARY KEY NONCLUSTERED (codCombo, codProducto)
	)
GO
CREATE INDEX TC_T_14 ON T_1 (codCombo )
GO
CREATE INDEX TC_T_15 ON T_1 (codProducto )
GO
CREATE TABLE T_0 (
	codCarta INT NOT NULL,
	codCombo INT NOT NULL,
	CONSTRAINT PK_T_08 PRIMARY KEY NONCLUSTERED (codCarta, codCombo)
	)
GO
CREATE INDEX TC_T_02 ON T_0 (codCarta )
GO
CREATE INDEX TC_T_03 ON T_0 (codCombo )
GO
CREATE TABLE T_Venta (
	codVenta INT NOT NULL,
	montoTotal SMALLINT NOT NULL,
	fecha SMALLINT NOT NULL,
	CONSTRAINT PK_T_Venta4 PRIMARY KEY NONCLUSTERED (codVenta)
	)
GO
CREATE TABLE T_Producto (
	codProducto INT IDENTITY NOT NULL,
	nombre VARCHAR ( 255 ) NOT NULL,
	elaboracion SMALLINT NOT NULL,
	costo SMALLINT NOT NULL,
	umbralCosto SMALLINT NOT NULL,
	precio SMALLINT NOT NULL,
	estado BIT NOT NULL,
	calorias NUMERIC ( 13, 2 ) NOT NULL,
	proteinas NUMERIC ( 13, 2 ) NOT NULL,
	carbohidratos NUMERIC ( 13, 2 ) NOT NULL,
	grasas NUMERIC ( 13, 2 ) NOT NULL,
	CONSTRAINT PK_T_Producto1 PRIMARY KEY NONCLUSTERED (codProducto)
	)
GO
ALTER TABLE T_ComboVenta ADD CONSTRAINT FK_T_ComboVenta7 FOREIGN KEY (codCombo) REFERENCES T_Combo (codCombo) 
GO
ALTER TABLE T_ComboVenta ADD CONSTRAINT FK_T_ComboVenta6 FOREIGN KEY (codVenta) REFERENCES T_Venta (codVenta) 
GO
ALTER TABLE T_1 ADD CONSTRAINT FK_T_14 FOREIGN KEY (codCombo) REFERENCES T_Combo (codCombo) 
GO
ALTER TABLE T_1 ADD CONSTRAINT FK_T_15 FOREIGN KEY (codProducto) REFERENCES T_Producto (codProducto) 
GO
ALTER TABLE T_InformacionNutricional ADD CONSTRAINT FK_T_InformacionNutricional8 FOREIGN KEY (codArticulo) REFERENCES T_Articulo (codArticulo) 
GO
ALTER TABLE T_ArticuloProducto ADD CONSTRAINT FK_T_ArticuloProducto0 FOREIGN KEY (codProducto) REFERENCES T_Producto (codProducto) 
GO
ALTER TABLE T_ArticuloProducto ADD CONSTRAINT FK_T_ArticuloProducto1 FOREIGN KEY (codArticulo) REFERENCES T_Articulo (codArticulo) 
GO
ALTER TABLE T_0 ADD CONSTRAINT FK_T_02 FOREIGN KEY (codCarta) REFERENCES T_Carta (codCarta) 
GO
ALTER TABLE T_0 ADD CONSTRAINT FK_T_03 FOREIGN KEY (codCombo) REFERENCES T_Combo (codCombo) 
GO

