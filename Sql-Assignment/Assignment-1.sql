-- SELECT * FROM dbo.Products;

-- 1. Write a query to get a Product list (id, name, unit price) where current products cost less than $20.
SELECT ProductID, ProductName, UnitPrice
FROM dbo.products
WHERE UnitPrice<20;


-- 2. Write a query to get Product list (id, name, unit price) where products cost between $15 and $25
SELECT ProductID, ProductName, UnitPrice
FROM dbo.products
WHERE UnitPrice BETWEEN 15 AND 25;


-- 3. Write a query to get Product list (name, unit price) of above average price.
SELECT ProductName, UnitPrice
FROM dbo.Products
WHERE UnitPrice > (SELECT AVG(UnitPrice) FROM dbo.Products);

-- 4. Write a query to get Product list (name, unit price) of ten most expensive products
SELECT TOP 10 ProductName, UnitPrice
FROM dbo.Products
ORDER BY UnitPrice DESC;

-- 5. Write a query to count current and discontinued products
SELECT COUNT(ProductName) as 'Current & Discontinued Products' 
FROM dbo.Products
GROUP BY Discontinued;


-- 6. Write a query to get Product list (name, units on order , units in stock) of stock is less than the quantity on order
SELECT ProductName, UnitsOnOrder, UnitsInStock
FROM dbo.Products
WHERE UnitsInStock < UnitsOnOrder;