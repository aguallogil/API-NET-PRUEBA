USE DevLab
GO
create table FAC_TiposClientes(
id int identity(1,1),
de_TipoCliente varchar(250) not null,
primary key(id)
);

create table FAC_Clientes(
id int identity(1,1),
de_RazonSocial varchar(250) not null,
id_TipoCliente int,
RFC char(13) not null,
fh_Registro datetime,
primary key(id),
foreign key(id_TipoCliente) references FAC_TiposClientes(id)
);

create table FAC_Productos(
id int identity(1,1),
de_Producto varchar(250) not null,
nu_Cantidad int not null,
imp_Precio decimal(12,2)not null,
img_Producto image,
sn_Estatus bit,
primary key(id)
);

create table FAC_Facturas(
id int,
fh_Factura date,
nu_Factura int not null,
id_Cliente int,
nu_Articulos int not null,
imp_Subtotal decimal(12,2)not null,
imp_TotalImpuestos decimal(12,2) not null,
imp_Total decimal(12,2),
primary key(id),
foreign key(id_Cliente) references FAC_Clientes(id)
);

create table FAC_FacturaDetalles(
id int identity(1,1),
id_Factura int,
id_Producto int,
nu_Cantidad int,
imp_PrecioUnitario decimal(12,2),
imp_SubTotal decimal(12,2),
notas varchar(250),
primary key(id),
foreign key(id_Factura) references FAC_Facturas(id),
foreign key(id_Producto) references FAC_Productos(id)
);

CREATE TABLE Usuarios (
    id_Usuario INT identity(1,1),
    de_Usuario VARCHAR(255) NOT NULL,
    de_Password VARCHAR(255) NOT NULL,
	primary key(id_Usuario)
);

insert into Usuarios values('admin','123')
