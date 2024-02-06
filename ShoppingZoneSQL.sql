create database ShoppingZone

use ShoppingZone

create table tbl_UserRegister
(
	UserId int Primary key identity(1,1),
	Name varchar(50),
	Email varchar(100),
	Password varchar(50),
	);
select * from tbl_UserRegister

create table tbl_Categories
(
	CatId int primary key not null identity,
	Name varchar(50),
)

create table tbl_Products
(
	ProdId int primary key not null,
	ProdName varchar(100),
	ProdDesc varchar(200),
	Price int,
	Image nvarchar(max),
	CatId int foreign key references tbl_Categories(CatId)
)

create table tbl_order
(
	OrderId int primary key identity not null,
	ProdID int foreign key references tbl_products(ProdId),
	Contact varchar(50),
	Address varchar(100),
	Price int,
	Qty int,
	Total int,
	OrderDate Date,
	InvoiceId int foreign key references tbl_Invoice(InvoiceId)
)
create table tbl_Invoice
(
	InvoiceId int primary key identity not null,
	UserId int foreign key references tbl_UserRegister(UserId),
	Bill int,
	Payment varchar(50),
	InvoiceDate date
)