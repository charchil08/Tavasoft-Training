use sample
go

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


select dbo.fnCalculateAge('11/20/2001')
go

