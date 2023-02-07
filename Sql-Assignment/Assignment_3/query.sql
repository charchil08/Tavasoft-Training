use Assignment3
go

-- 1. write a SQL query to find Employees who have the biggest salary in their Department
select dep_id,MAX(salary) [Highest Salary]
from dbo.Employees
group by dep_id;
go

-- 2. write a SQL query to find Departments that have less than 3 people in it
select dep_id,count(emp_id) as 'count'
from dbo.Employees
group by dep_id
having count(emp_id) < 3;
go

-- 3. write a SQL query to find All Department along with the number of people there
select e.dep_id, d.dep_name,count(e.emp_id) as 'count'
from dbo.Employees e
inner join Department d on d.dep_id = e.dep_id
group by e.dep_id, d.dep_name;
go-- 4. write a SQL query to find All Department along with the total salary thereselect e.dep_id, d.dep_name,SUM(e.salary) as 'salary'
from dbo.Employees e
inner join Department d on d.dep_id = e.dep_id
group by e.dep_id, d.dep_name;
go