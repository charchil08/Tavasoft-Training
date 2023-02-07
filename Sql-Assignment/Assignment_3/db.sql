create table Employees(
	emp_id int primary key,
	emp_name nvarchar(50),
	job_name nvarchar(50),
	manager_id int foreign key references Employees(emp_id),
	hire_date date,
	salary numeric(7,2),
	commission numeric(6,2),
	dep_id int foreign key references Department(dep_id)
)
go


create table Department(
	dep_id int primary key,
	dep_name nvarchar(30),
	dep_location nvarchar(30)
)
go

insert into dbo.Department
        values (1001,'FINANCE','SYDNEY'),(2001,'AUDIT','MELBOURNE'),
(3001,'MARKETING','PERTH'),
(4001,'PRODUCTION','BRISBANE')
go

insert into dbo.Employees(emp_id,emp_name,job_name, manager_id, hire_date,salary,commission, dep_id) 
values (68319,'KAYLING','PRESIDENT',null,'1991-11-18',6000.00,null,1001)
,(66928,'BLAZE','MANAGER',68319,'1991-05-01',2750.00,null,3001)
    ,(67832,'CLARE','MANAGER',68319,'1991-06-09',2550.00,null,1001)
    ,(65646,'JONAS','MANAGER',68319,'1991-04-02',2957.00,null,2001)
    ,(64989,'ADELYN','SALESMAN',66928,'1991-02-20',1700.00,400.00,3001)
    ,(65271,'WADE','SALESMAN',66928,'1991-02-22',1350.00,600.00,3001)
    ,(66564,'MADDEN','SALESMAN',66928,'1991-09-28',1350.00,1500.00,3001)
    ,(68454,'TUCKER','SALESMAN',66928,'1991-09-08',1600.00,0.00,3001)
    ,(68736,'ADNRES','CLERK',67858,'1997-05-23',1200.00,null,2001)
    ,(69000,'JULIUS','CLERK',66928,'1991-12-03',1050.00,null,3001)
    ,(69324,'MARKER','CLERK',67832,'1992-01-23',1400.00,null,1001)
    ,(67858,'SCARLET','ANALYST',65646,'1997-04-19',3100.00,null,2001)
    ,(69062,'FRANK','ANALYST',65646,'1991-12-03',3100.00,null,2001)
    ,(63679,'SANDRINE','CLERK',69062,'1990-12-18',900.00,null,2001)
go
