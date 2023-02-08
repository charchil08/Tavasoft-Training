use sample
go

-- scaler function
CREATE OR ALTER FUNCTION fnCalculateAge(@DOB datetime)
RETURNS nvarchar(30)
AS
BEGIN
	DECLARE @Temp_Date datetime, @Year int, @Month int, @Day int,@Age nvarchar(30)
	SET @Temp_Date = @DOB
	
	-- calculate year
	SET @Year = DATEDIFF(YEAR, @Temp_Date,GETDATE()) - 
				CASE 
					WHEN (MONTH(GETDATE()) < MONTH(@Temp_Date)) OR
						(MONTH(@Temp_Date) = MONTH(GETDATE()) AND DAY(@Temp_Date) > DAY(GETDATE()))
					THEN 1 ELSE 0
				END
	SET @Temp_Date = DATEADD(YEAR,@Year,@Temp_Date)
	
	-- calculate month from remaining date
	SET @Month = DATEDIFF(MONTH, @Temp_Date, GETDATE()) - 
				 CASE
					WHEN (DAY(GETDATE()) < DAY(@Temp_Date))
					THEN 1 ELSE 0
				END
	SET @Temp_Date = DATEADD(MONTH,@Month,@Temp_Date)
	
	-- calculate day
	SET @Day = DATEDIFF(DAY, @Temp_Date, GETDATE())
	
	SET @Age = CAST(@Year as nvarchar(4)) + ' Years,' + CAST(@Month as nvarchar(2)) + ' Months and ' + CAST(@Day as nvarchar(2)) + ' Days' 
	return @Age
END
go

select dbo.fnCalculateAge('11/20/2001') as Age
go

select POWER(2.32,3)
go

-- 1 to 5
select CAST((RAND() * 5)+1 as int)
go

select * from tblEmployee;
go

-- inline table function
CREATE OR ALTER FUNCTION fnIEmpWithManagerName()
RETURNS TABLE
AS
RETURN (
	SELECT e.name, COALESCE(ep.name, 'No manager') as 'manager'
	from tblEmployee e
	left join tblEmployee ep on ep.id = e.managerId
);
go

select * from dbo.fnIEmpWithManagerName()
go

-- multi statement table function
CREATE OR ALTER FUNCTION fnMEmpWithManagerName()
RETURNS @tblDetails Table(emp_name varchar(10), mgr_name varchar(10))
AS
BEGIN
	INSERT INTO @tblDetails
	SELECT e.name, COALESCE(ep.name, 'No manager')
	from tblEmployee e
	left join tblEmployee ep on ep.id = e.managerId

	RETURN
END
GO

SELECT * FROM dbo.fnMEmpWithManagerName()
go