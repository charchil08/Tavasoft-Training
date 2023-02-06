-- 1. write a SQL query to find the salesperson and customer who reside in the same city.
-- Return Salesman, cust_name and city
SELECT s.name 'SalesPerson', c.cust_name 'Customer', c.city
from salesman as s
inner join customer c
on s.city = c.city
order by city;


-- 2. write a SQL query to find those orders where the order amount exists between 500
-- and 2000. Return ord_no, purch_amt, cust_name, city
SELECT  o.ord_no, o.purch_amt, c.cust_name, c.city
FROM orders o
inner join customer c
on c.customer_id = o.customer_id
where o.purch_amt between 500 and 2000; 



-- 3. write a SQL query to find the salesperson(s) and the customer(s) he represents.
-- Return Customer Name, city, Salesman, commission
select c.cust_name 'Customer name', c.city, s.name 'Salesman name', s.commission 
from customer c
inner join salesman s
on s.salesman_id = c.salesman_id
order by c.cust_name;



-- 4. write a SQL query to find salespeople who received commissions of more than 12
-- percent from the company. Return Customer Name, customer city, Salesman,
-- commission.
select c.cust_name 'Customer', c.city, s.name 'Salesman', s.commission
from salesman s
inner join customer c
on s.salesman_id = c.salesman_id
where s.commission > .12
order by s.name;

-- 5. write a SQL query to locate those salespeople who do not live in the same city where
-- their customers live and have received a commission of more than 12% from the
-- company. Return Customer Name, customer city, Salesman, salesman city,
-- commission
select c.cust_name 'Customer name', c.city 'Customer city', s.name 'Salesman name', s.city 'Salesman city'
from salesman s
inner join customer c
on c.salesman_id = s.salesman_id
where (s.city <> c.city) and (s.commission > .12);
 


-- 6. write a SQL query to find the details of an order. Return ord_no, ord_date,
-- purch_amt, Customer Name, grade, Salesman, commission
select o.purch_amt, c.cust_name 'Customer name', c.grade, s.name 'Salesman name', s.commission
from orders o
inner join customer c on c.customer_id = o.customer_id
inner join salesman s on s.salesman_id = o.salesman_id
order by o.purch_amt;



-- 7. Write a SQL statement to join the tables salesman, customer and orders so that the
-- same column of each table appears once and only the relational rows are returned.
select o.ord_no, o.purch_amt, o.ord_date, c.customer_id, c.cust_name 'Customer name', c.city 'Customer city', c.grade,
s.salesman_id, s.name 'Salesman name', s.city 'Salesman city', s.commission
from orders o
inner join customer c on c.customer_id = o.customer_id
inner join salesman s on s.salesman_id = o.salesman_id;


-- 8. write a SQL query to display the customer name, customer city, grade, salesman,salesman city. 
-- The results should be sorted by ascending customer_id.
select c.customer_id,c.cust_name 'Customer name', c.city 'Customer city', 
ISNULL(c.grade,' ') 'Grade', s.salesman_id, s.name 'Salesman name', s.city 'Salesman city'
from customer c
inner join salesman s on s.salesman_id = c.salesman_id
order by c.customer_id;



-- 9. write a SQL query to find those customers with a grade less than 300. Return
-- cust_name, customer city, grade, Salesman, salesmancity. The result should be
-- ordered by ascending customer_id.
select c.cust_name, c.city 'Customer city', COALESCE(c.grade, '') Grade, 
s.salesman_id, s.name 'Salesman name', s.city 'Salesman city'
from customer c
inner join salesman s on s.salesman_id = c.salesman_id
where c.grade is null or c.grade < 300
order by c.grade;


-- 10. Write a SQL statement to make a report with customer name, city, order number,
-- order date, and order amount in ascending order according to the order date to
-- determine whether any of the existing customers have placed an order or not
SELECT c.cust_name 'Customer name', c.city,  COALESCE(o.ord_no, '') [order no], o.ord_date, o.purch_amt
from customer c
left join orders o on c.customer_id = o.customer_id
order by o.ord_date;



-- 11. Write a SQL statement to generate a report with customer name, city, order number,
-- order date, order amount, salesperson name, and commission to determine if any of
-- the existing customers have not placed orders or if they have placed orders through
-- their salesman or by themselves
select c.cust_name, c.city [customer city], o.ord_no, o.ord_date, o.purch_amt, s.name [salesman name], s.commission
from customer c
left join orders o on o.customer_id = c.customer_id
left join salesman s on s.salesman_id = o.salesman_id
order by c.cust_name;





-- 12. Write a SQL statement to generate a list in ascending order of salespersons who
-- work either for one or more customers or have not yet joined any of the customers
select s.name 'Salesman', s.city, c.cust_name [customer], c.city 
from salesman s
left join customer c on c.salesman_id = s.salesman_id
order by s.salesman_id;


--13. write a SQL query to list all salespersons along with customer name, city, grade,
--order number, date, and amount.
SELECT s.name, c.cust_name, c.city [cust_city], c.grade, o.ord_no, o.ord_date, o.purch_amt
from salesman s
left join customer c on c.salesman_id = s.salesman_id
left join orders o on o.customer_id = c.customer_id;


--14. Write a SQL statement to make a list for the salesmen who either work for one or
-- more customers or yet to join any of the customers. The customer may have placed,
-- either one or more orders on or above order amount 2000 and must have a grade, or
-- he may not have placed any order to the associated supplier.
select * 
from salesman s
left join customer c on c.salesman_id = s.salesman_id
left join orders o on o.customer_id = c.customer_id
where ((c.grade is not null) AND (o.purch_amt >=2000)) OR (c.customer_id ); 


-- 15. Write a SQL statement to generate a list of all the salesmen who either work for one
-- or more customers or have yet to join any of them. The customer may have placed
-- one or more orders at or above order amount 2000, and must have a grade, or he
-- may not have placed any orders to the associated supplier.

-- 16. Write a SQL statement to generate a report with the customer name, city, order no.
-- order date, purchase amount for only those customers on the list who must have a
-- grade and placed one or more orders
-- or 
-- which order(s) have been placed by the customer who neither is on the list nor has a grade.

select c.cust_name, c.city,o.ord_no, o.ord_date, o.purch_amt
from orders o
left join customer c on c.customer_id = o.customer_id
where (c.grade is not null);


-- 17. Write a SQL query to combine each row of the salesman table with each row of the
-- customer table
select *
from salesman s 
inner join customer c on 1=1;



18. Write a SQL statement to create a Cartesian product between salesperson and
customer, i.e. each salesperson will appear for all customers and vice versa for that
salesperson who belongs to that city.
select *
from salesman s 
inner join customer c on 1=1
where s.city is not null;

-- 19. Write a SQL statement to create a Cartesian product between salesperson and
-- customer, i.e. each salesperson will appear for every customer and vice versa for
-- those salesmen who belong to a city and customers who require a grade
select *
from salesman s
inner join customer c on 1=1
where (s.city is not null) and (c.grade is not null);



-- 20. Write a SQL statement to make a Cartesian product between salesman and
-- customer i.e. each salesman will appear for all customers and vice versa for those
-- salesmen who must belong to a city which is not the same as his customer and the
-- customers should have their own gradeselect *from salesman sinner join customer c on 1=1where (s.city is not null) AND (s.city <> c.city) AND (c.grade is not null);