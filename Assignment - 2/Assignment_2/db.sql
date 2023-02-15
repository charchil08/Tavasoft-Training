
create table salesman (
	salesman_id int primary key,
	name varchar(30),
	city varchar(30),
	commission numeric(4,2)
)
Go

insert into salesman values( 5001,'James Hoog','New York',0.15)
insert into salesman values( 5002,'Nail Knite','Paris',0.13)
insert into salesman values( 5005,'Pit Alex','London',0.11)
insert into salesman values( 5006,'Mc Lyon','Paris',0.14)
insert into salesman values( 5007,'Paul Adam','Rome',0.13)
insert into salesman values( 5003,'Lauson Hen','San Jose',0.12)
go

create table customer (
customer_id int Identity(3001,1) primary key,
cust_name varchar(50),
city varchar(50),
grade int,
salesman_id int foreign key references salesman(salesman_id)
)
go

insert into customer(cust_name,city,grade,salesman_id)
values
('Brad Guzan','London',null,5005),
('Nick Rimando','New York',100,5001),
('Jozy Altidor','Moscow',200,5007),
('Fabian Johnson','Paris',300,5006),
('Graham Zusi','California',200,5002),
('Chirag', 'bvn',100,5001),
('Brad Davis', 'New York',200,5001),
('Julian Green','London',300,5002),
('Geoff Cameron','Berlin',100,5003)
('Charchil Kajaliya', 'Bvn', 300, 5002)
go



create table orders(
ord_no int primary key,
purch_amt float,
ord_date date,
customer_id int foreign key references customer(customer_id),
salesman_id int foreign key references salesman(salesman_id)
)
go

insert into orders values(70009,270.65,'2012-09-10',3001,5005)
insert into orders values(70002,65.26,'2012-10-05',3002,5001)
insert into orders values(70004,110.50,'2012-08-17',3009,5003)
insert into orders values(70005,2400.60,'2012-07-27',3007,5001)
insert into orders values(70008,5760.00,'2012-09-10',3002,5001)
insert into orders values(70010,1983.43,'2012-10-10',3004,5006)
insert into orders values(70003,2480.40,'2012-10-10',3009,5003)
insert into orders values(70011,75.29,'2012-08-17',3003,5007)
insert into orders values(70013,3045.60,'2012-04-25',3002,5001)
insert into orders values(70001,150.50,'2012-10-05',3005,5002)
insert into orders values(70007,948.50,'2012-09-10',3005,5002)
insert into orders values(70012,250.45,'2012-06-27',3008,5002)
go

select * from customer;
select * from orders;
select * from salesman;