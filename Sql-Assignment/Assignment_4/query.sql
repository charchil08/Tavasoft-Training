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

-- 1. Create a stored procedure in the Northwind database that will calculate the average
-- value of Freight for a specified customer.Then, a business rule will be added that will
-- be triggered before every Update and Insert command in the Orders controller,and
-- will use the stored procedure to verify that the Freight does not exceed the average
-- freight. If it does, a message will be displayed and the command will be cancelled.


 
-- 2. write a SQL query to Create Stored procedure in the Northwind database to retrieve
-- Employee Sales by Country
CREATE OR ALTER PROC spEmployeeSalesByCountry
	@country nvarchar(30)
AS
BEGIN
	select E.Country "Employee Country", CONCAT(E.LastName,' ',E.FirstName) "Employee Name", O.ShippedDate, O.OrderID, OS.Subtotal "Sale Amount"
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
SELECT * FROM Orders;
SELECT * FROM [Order Details];

-- cust_id, emp_id, ord_date, req_date, ship_date, ship_via, frieght, ship_name, ship_add, ship_city, ship_region,ship_postal_code, ship_country
-- [product_id, unit_price, quantity, discount=0]

-- 7. write a SQL query to Create Stored procedure in the Northwind database to update
-- Customer Order Details