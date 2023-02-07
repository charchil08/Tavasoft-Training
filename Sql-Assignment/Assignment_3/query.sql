use Assignment3
go

select * from Employees;

-- 1. write a SQL query to find Employees who have the biggest salary in their Department

select dp.dep_id, ep.salary, ep.emp_name, dp.dep_name
from Department dp
left join Employees ep on ep.dep_id = dp.dep_id
where 
ep.salary in (select dep_id,MAX(salary) as "sal"  from Employees group by dep_id) or 
ep.salary is null ;

-- 2nd option by creating view
create view vwMaxSalaryByDepartment
AS
select d.dep_id,MAX(salary) as 'salary'
from Employees e
right join Department d on d.dep_id = e.dep_id
group by d.dep_id

SELECT d.dep_id, d.dep_name,e.emp_name, dbo.vwMaxSalaryByDepartment.salary 
FROM dbo.vwMaxSalaryByDepartment
LEFT JOIN Employees e on e.salary = dbo.vwMaxSalaryByDepartment.salary
inner JOIN Department d on d.dep_id = dbo.vwMaxSalaryByDepartment.dep_id;

-- 2. write a SQL query to find Departments that have less than 3 people in it
select d.dep_id,count(e.emp_id) as 'count', d.dep_name
from dbo.Employees e
right join Department d on d.dep_id = e.dep_id
group by d.dep_id, d.dep_name
having count(emp_id) < 3;
go

-- 3. write a SQL query to find All Department along with the number of people there
select e.dep_id, d.dep_name,count(e.emp_id) as 'count'
from dbo.Employees e
right join Department d on d.dep_id = e.dep_id
group by e.dep_id, d.dep_name;
go-- 4. write a SQL query to find All Department along with the total salary thereselect e.dep_id, d.dep_name,SUM(e.salary) as 'salary'
from dbo.Employees e
right join Department d on d.dep_id = e.dep_id
group by e.dep_id, d.dep_name;
go