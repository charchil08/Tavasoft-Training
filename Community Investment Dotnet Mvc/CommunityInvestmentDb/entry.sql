
	INSERT INTO dbo.country
		(name, ISO)
	VALUES
		('India', 'IN'),
		('Pakistan', 'PAK'),
		('China', 'CHN'),
		('Myanmar', 'MMR'),
		('Thailand', 'THA'),
		('Malaysia', 'MYS'),
		('Indonesia', 'IDN'),
		('Philippines', 'PHL'),
		('Australia', 'AUS'),
		('New Zealand', 'NZL');
	GO

	INSERT INTO dbo.city
		(country_Id, name)
	VALUES
		(1, 'Mumbai'),
		(1, 'Delhi'),
		(1, 'Bangalore'),
		(1, 'Hyderabad'),
		(1, 'Chennai'),
		(1, 'Kolkata'),
		(1, 'Pune'),
		(1, 'Ahmedabad'),
		(1, 'Surat'),
		(1, 'Jaipur'),
		(1, 'Bhavnagar');

	INSERT INTO dbo.city
		(country_Id, name)
	VALUES
		(2, 'Lahore'),
		(2, 'Karachi'),
		(2, 'Islamabad'),
		(2, 'Rawalpindi'),
		(2, 'Faisalabad'),
		(2, 'Multan'),
		(2, 'Gujranwala'),
		(2, 'Quetta'),
		(2, 'Peshawar'),
		(2, 'Hyderabad');

	INSERT INTO dbo.city
		(country_Id, name)
	VALUES
		(3, 'Shanghai'),
		(3, 'Beijing'),
		(3, 'Guangzhou'),
		(3, 'Shenzhen'),
		(3, 'Tianjin'),
		(3, 'Chongqing'),
		(3, 'Hangzhou'),
		(3, 'Nanjing'),
		(3, 'Wuhan'),
		(3, 'Chengdu');

	INSERT INTO dbo.city
		(country_Id, name)
	VALUES
		(4, 'Yangon'),
		(4, 'Mandalay'),
		(4, 'Naypyidaw'),
		(4, 'Mawlamyine'),
		(4, 'Taunggyi'),
		(4, 'Myitkyina'),
		(4, 'Pyay'),
		(4, 'Lashio'),
		(4, 'Pathein'),
		(4, 'Hpa-An');

	INSERT INTO dbo.city
		(country_Id, name)
	VALUES
		(5, 'Bangkok'),
		(5, 'Chiang Mai'),
		(5, 'Phuket'),
		(5, 'Pattaya'),
		(5, 'Krabi'),
		(5, 'Samui'),
		(5, 'Hua Hin'),
		(5, 'Chiang Rai'),
		(5, 'Nakhon Ratchasima'),
		(5, 'Udon Thani');
	go


	insert into dbo.[user] (first_name, last_name,email, password, phone_number, city_id, country_id, status)
	values
	('Charchil','Kajaliya','1@ci.com','12345678',9825012324,11,1,0); 

	insert into dbo.mission_theme (title, [status])
	values ('Education',1),
	('Health',1),
	('Children',1),
	('Environment',1),
	('Animal',1),
	('Nutrition',1)
	go


	INSERT INTO skill (skill_name, status)
	VALUES 
	  ('Performance', 1),
	  ('Employee relations', 1),
	  ('Computer S.', 1),
	  ('Diversity', 1),
	  ('Planning', 1);