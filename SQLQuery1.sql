Create DATABASE Input
GO

USE Input

DROP TABLE Customer;
DROP TABLE Programmer;
DROP TABLE Orders;
go

CREATE TABLE Customer(
	login nchar(20) not null,
	name nchar(40) not null,
	pass nchar(16) not null
	CONSTRAINT PK_Customer PRIMARY KEY (Login)
)  
GO

CREATE TABLE Programmer(
	login nchar(20) not null,
	name nchar(40) not null,
	pass nchar(16) not null
	CONSTRAINT PK_Programmer PRIMARY KEY (Login)
)  
GO

CREATE TABLE Orders(
	id int identity(1,1) not null,
	customer nchar(20) not null,
	title nchar(22) not null,
	description nvarchar(max) not null,
	price money not null,
	status nchar(20) not null,
	programmer nchar(20) null,
	CONSTRAINT PK_Orders_ID PRIMARY KEY(id),
	CONSTRAINT FK_Orders_Customer FOREIGN KEY (customer) REFERENCES Customer(login),
	CONSTRAINT FK_Orders_Programmer FOREIGN KEY (programmer) REFERENCES Programmer(login),
)
GO



INSERT INTO Orders(customer,title,description,price,status,programmer)
VALUES
('Adler','Тест','Тест', 20, 'Занят', 'Adler')

update Orders set status = 'Занят' where id = '3'
update Orders set programmer = 'Adler' where id = '3'

select id, title, price, status from Orders where status = N'Открытый' or programmer = 'Adler'

select * from Orders