use Assignment3
go

-- 1. write a SQL query to find Employees who have the biggest salary in their Department
select dep_id,MAX(salary) [Highest Salary]
from dbo.Employees
group by dep_id;

-- 2. write a SQL query to find Departments that have less than 3 people in it
select dep_id,count(emp_id) as 'count'
from dbo.Employees
group by dep_id
having count(emp_id) < 3;

3. write a SQL query to find All Department along with the number of people there
4. write a SQL query to find All Department along with the total salary there