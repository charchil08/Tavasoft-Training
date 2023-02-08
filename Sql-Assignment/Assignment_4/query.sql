use Northwind;
go

-- sp_help "Order Details";
-- sp_help "Products"
-- sp_help "Categories"
-- select * from Categories;
-- select * from "Order Details"
-- select * from "Orders"
-- 
-- select p.ProductName, ROUND(SUM(od.UnitPrice*od.Quantity*(1-od.Discount)),0) 
-- from "Order Details" od
-- inner join Orders o on o.OrderID = od.OrderID
-- inner join Products p on p.ProductID = od.ProductID
-- inner join Categories c on c.CategoryID = p.CategoryID
-- where c.CategoryName = 'Beverages'
-- group by p.ProductName
-- 
-- 
-- sp_helptext [Employee Sales by Country]

-- select c.CustomerID, c.City, o.OrderID, o.Freight, od.ProductID, od.Quantity, od.UnitPrice
-- from Customers c
-- inner join Orders o on o.CustomerID = c.CustomerID
-- inner join [Order Details] od on od.OrderID = o.OrderID
-- order by c.CustomerID;
-- 
-- select c.CustomerID, AVG(o.Freight)
-- from Customers c
-- inner join Orders o on o.CustomerID = c.CustomerID
-- inner join [Order Details] od on od.OrderID = o.OrderID
-- group by c.CustomerID
-- order by c.CustomerID;

-- select * from Orders where CustomerID='RATTC';
-- select * from Orders;
 
-- 1. Create a stored procedure in the Northwind database that will calculate the average
-- value of Freight for a specified customer.Then, a business rule will be added that will
-- be triggered before every Update and Insert command in the Orders controller,and
-- will use the stored procedure to verify that the Freight does not exceed the average
-- freight. If it does, a message will be displayed and the command will be cancelled.
create or alter proc spCalcAvgFreightByCustomer
@CustomerId varchar(5),
@AvgFreightOrders money output
as
begin
	select @AvgFreightOrders=AVG(Orders.Freight)
					  from Orders 
					  where Orders.CustomerID = @CustomerId;
	return @AvgFreightOrders;
end
go

create or alter trigger tr_insteadOfInsert_orders
on Orders
instead of insert
as
begin
	declare @AvgFreight money, @CustomerId nvarchar(5), @insertedFreight money 
	
	-- select * from inserted;

	select @CustomerId = CustomerId
	from Customers 
	where CustomerID in (select CustomerID from inserted);

	if(@CustomerId is null)
	begin
		Raiserror('Invalid customer id',16,1)
		return		
	end

	select @insertedFreight = Freight
	from inserted;

	exec dbo.spCalcAvgFreightByCustomer @CustomerId,@AvgFreightOrders=@AvgFreight output; 

	if(@AvgFreight < @insertedFreight)
	begin
		Raiserror('Freight amount excluded',16,1)
		return
	end

	Print 'tr_insteadOfInsert_orders'
	insert into Orders( CustomerID, EmployeeID, OrderDate, RequiredDate, ShipVia, Freight, ShipName, ShipAddress, ShipCity,ShipPostalCode, ShipCountry)
    select  CustomerID, EmployeeID, OrderDate, RequiredDate, ShipVia, Freight, ShipName, ShipAddress, ShipCity,ShipPostalCode, ShipCountry
	from inserted;

end
go

create or alter trigger tr_insteadOfUpdate_orders
on Orders
instead of update
as
begin
	declare @AvgFreight money, @CustomerId nvarchar(5), @insertedFreight money,@deletdFreight money, @OrderId int 
	
--	select * from inserted;
--	select * from deleted;

	select @CustomerId = CustomerId
	from Customers 
	where CustomerID in (select CustomerID from inserted);

	select @OrderId = OrderId
	from Orders 
	where OrderID in (select OrderID from inserted);

	if(@CustomerId is null or @OrderId is null )
	begin
		Raiserror('Invalid customer or order id',16,1)
		return		
	end

	select @insertedFreight = Freight
	from inserted;
	select @deletdFreight = Freight
	from deleted;
	
	exec dbo.spCalcAvgFreightByCustomer @CustomerId,@AvgFreightOrders=@AvgFreight output; 

	Print 'Deleted Freight ' +  CAST(@deletdFreight as varchar) + ' \n Inserted Freight : ' + CAST(@insertedFreight as varchar) + ' \n Updated : ' + CAST(@AvgFreight as varchar);
	
	if((@insertedFreight <> @deletdFreight) and (@AvgFreight < @insertedFreight))
	begin
		Raiserror('Freight amount excluded',16,1)
		return		
	end

	Print 'tr_insteadOfUpdate_orders'
	
	update Orders
	set Freight = @insertedFreight
	where OrderID = @OrderId;

end
go


insert into Orders ( CustomerID, EmployeeID, OrderDate, RequiredDate, ShipVia, Freight, ShipName, ShipAddress, ShipCity,ShipPostalCode, ShipCountry)
values('RATTC',2,'1998-05-12 00:00:00.000','1998-05-24 00:00:00.000',2,168,'Rattlesnake Canyon Grocery','2817 Milton Dr.','Albuquerque',87110, 'USA')
go

insert into Orders ( CustomerID, EmployeeID, OrderDate, RequiredDate, ShipVia, Freight, ShipName, ShipAddress, ShipCity,ShipPostalCode, ShipCountry)
values('RATTC',2,'1998-05-12 00:00:00.000','1998-05-24 00:00:00.000',2,111,'Rattlesnake Canyon Grocery','2817 Milton Dr.','Albuquerque',87110, 'USA')
go


update Orders
set Freight = 115.50
where OrderID = 11078;
go

select AVG(Orders.Freight)
from Orders 
where CustomerID = 'RATTC'
go

-- 2. write a SQL query to Create Stored procedure in the Northwind database to retrieve
-- Employee Sales by Country
CREATE OR ALTER PROC spEmployeeSalesByCountry
	@country nvarchar(30)
AS
BEGIN
	select O.ShipCountry "Shipping country", E.Country "Employee Country", CONCAT(E.LastName,' ',E.FirstName) "Employee Name", O.ShippedDate, O.OrderID, OS.Subtotal "Sale Amount"
	from Employees E
	inner join Orders O on O.EmployeeID = E.EmployeeID
	inner join [Order Subtotals] OS on OS.OrderID = O.OrderID
	where O.ShipCountry = @country
	order by O.EmployeeID
END
GO

EXEC dbo.spEmployeeSalesByCountry 'France'
go
-- 3. write a SQL query to Create Stored procedure in the Northwind database to retrieve
-- Sales by Year
CREATE OR ALTER PROC spSalesByYear
	@Year varchar(4)
AS
	IF @Year not in ('1996','1997','1998')
	BEGIN
		PRINT 'ENTER VALID YEAR: 1996,1997 OR 1998'
	END
	ELSE
	BEGIN
		SELECT o.*,os.Subtotal 
		FROM Orders o
		inner join "Order Subtotals" os on os.OrderID = o.OrderID
		WHERE DATENAME(YYYY, o.OrderDate) = @Year
	END
go

EXEC spSalesByYear '1996'
GO

-- 4. write a SQL query to Create Stored procedure in the Northwind database to retrieve
-- Sales By Category
CREATE OR ALTER PROC spSalesByCategory
	@category nvarchar(15)
AS
BEGIN
	select p.ProductName, ROUND(SUM(od.UnitPrice*od.Quantity*(1-od.Discount)),0) as "Total Sales"
	from "Order Details" od
	inner join Orders o on o.OrderID = od.OrderID
	inner join Products p on p.ProductID = od.ProductID
	inner join Categories c on c.CategoryID = p.CategoryID
	where LOWER(c.CategoryName) like ('%' + LOWER(@category) + '%')
	group by p.ProductName
END
GO

EXEC spSalesByCategory 'dairy'
go

-- 5. write a SQL query to Create Stored procedure in the Northwind database to retrieve
-- Ten Most Expensive Products
CREATE OR ALTER PROC spTenMostExpensiveProducts
AS
BEGIN
	select *
	from Products
	order by UnitPrice desc
	OFFSET 0 ROWS
	FETCH NEXT 10 ROWS ONLY;
END
GO

EXEC spTenMostExpensiveProducts
go

-- 6. write a SQL query to Create Stored procedure in the Northwind database to insert
-- Customer Order Details
create or alter proc spInsertNewOrder
@OrderID int ,
@ProductID int,
@UnitPrice money,
@Quantity smallInt,
@Discount real
as
insert into [dbo].[Order Details] values (@OrderID, @ProductID, @UnitPrice, @Quantity, @Discount)

--Execute --
DECLARE	@return_value int

EXEC	@return_value = [dbo].[spInsertNewOrder]
		@OrderID = 10249,
		@ProductID = 74,
		@UnitPrice = 34.80,
		@Quantity = 4,
		@Discount = 0.00
SELECT	'Return Value' = @return_value

-- SELECT * FROM Orders;
-- SELECT * FROM [Order Details];

-- cust_id, emp_id, ord_date, req_date, ship_date, ship_via, frieght, ship_name, ship_add, ship_city, ship_region,ship_postal_code, ship_country
-- [product_id, unit_price, quantity, discount=0]

-- 7. write a SQL query to Create Stored procedure in the Northwind database to update
-- Customer Order Details
create or alter procedure spUpdateOrder
@OrderID int ,
@ProductID int,
@Quantity smallInt
as
update [dbo].[Order Details] 
set 
Quantity = @Quantity
where OrderId = @OrderID AND ProductId = @ProductID

--execute --
DECLARE	@return_value int
EXEC	@return_value = [dbo].[spUpdateOrder]
		@OrderID = 10249,
		@ProductID = 74,
		@Quantity = 2

SELECT	'Return Value' = @return_value