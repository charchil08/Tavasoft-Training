
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
		(2, 'Peshawar')

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




INSERT INTO [dbo].[mission] ([mission_theme_id], [city_id], [country_id], [title], [short_description], [description], [start_date], [end_date], [mission_type_id], [status], [organization_name], [organization_detail], [availability_id])
VALUES
(1, 2, 1, 'Teaching English in rural areas', 'Volunteer to teach English in rural areas', 'We are looking for volunteers to teach English to students in remote rural areas. No previous teaching experience required. Accommodation and meals will be provided.', '2023-05-01', '2023-06-30', 1, 0, 'ABC Organization', 'Helping students to achieve their dreams', 2),

(2, 4, 1, 'Building homes for the homeless', 'Help us build homes for the homeless', 'Join us in building affordable homes for the homeless. We need volunteers who are willing to get their hands dirty and help with construction work. Accommodation and meals will be provided.', '2023-06-01', '2023-08-31', 1, 1, 'XYZ Foundation', 'Providing shelter for the homeless', 1),

(3, 6, 1, 'Medical mission in a developing country', 'Join us in providing medical care in a developing country', 'We are looking for medical professionals to join us in providing medical care to underserved communities in a developing country. Accommodation and meals will be provided.', '2023-09-01', '2023-09-15', 1, 0, 'Medical Missions International', 'Improving global health', 3),

(4, 8, 1, 'Environmental conservation project', 'Help us protect the environment', 'Join us in our efforts to protect the environment by participating in various conservation activities such as tree planting, wildlife monitoring, and environmental education. Accommodation and meals will be provided.', '2023-08-01', '2023-08-31', 1, 1, 'Green Earth Organization', 'Safeguarding the planet for future generations', 2),

(5, 10, 1, 'Disaster relief mission', 'Assist in disaster relief efforts', 'We are looking for volunteers to help with disaster relief efforts in areas affected by natural disasters. Tasks may include distributing relief supplies, providing medical care, and coordinating with local authorities. Accommodation and meals will be provided.', '2023-07-01', '2023-07-15', 1, 0, 'Red Cross', 'Responding to emergencies worldwide', 1),

(6, 11, 1, 'Orphanage volunteering', 'Volunteer at an orphanage', 'Join us in volunteering at an orphanage and help provide care and support to orphaned children. Tasks may include teaching, playing with the children, and assisting with daily activities. Accommodation and meals will be provided.', '2023-10-01', '2023-11-30', 2, 1, 'Hope Foundation', 'Supporting orphaned children', 2);

go


INSERT INTO [dbo].[mission_document] ([mission_id], [document_name], [doucment_type], [document_path])
VALUES
(4, 'Mission Image 1', 'image', 'D://CHARCHIL//Tavasoft-Training//Community Investment Dotnet Mvc//CommunityInvestment//CommunityInvestment//wwwroot//assets//CSR-initiative-stands-for-Coffee--and-Farmer-Equity.png'),
(2, 'Mission Image 2', 'image', 'D://CHARCHIL//Tavasoft-Training//Community Investment Dotnet Mvc//CommunityInvestment//CommunityInvestment//wwwroot//assets//Education-Supplies-for-Every--Pair-of-Shoes-Sold.png'),
(3, 'Mission Image 3', 'image', 'D://CHARCHIL//Tavasoft-Training//Community Investment Dotnet Mvc//CommunityInvestment//CommunityInvestment//wwwroot//assets//Grow-Trees-On-the-path-to-environment-sustainability.png'),
(7, 'Mission Image 4', 'image', 'https://example.com/mission-image-4.jpg'),
(5, 'Mission Image 5', 'image', 'https://example.com/mission-image-5.jpg'),
(6, 'Mission Image 6', 'image', 'https://example.com/mission-image-6.jpg')
go


INSERT INTO [dbo].[mission] ([mission_theme_id], [city_id], [country_id], [title], [short_description], [description], [start_date], [end_date], [mission_type_id], [status], [organization_name], [organization_detail], [availability_id])
VALUES
(1, 8, 1, 'Teaching English in rural areas', 'Volunteer to teach English in rural areas', 'We are looking for volunteers to teach English to students in remote rural areas. No previous teaching experience required. Accommodation and meals will be provided.', '2023-05-01', '2023-06-30', 1, 0, 'ABC Organization', 'Helping students to achieve their dreams', 2),

(1, 8, 1, 'Building homes for the homeless', 'Help us build homes for the homeless', 'Join us in building affordable homes for the homeless. We need volunteers who are willing to get their hands dirty and help with construction work. Accommodation and meals will be provided.', '2023-06-01', '2023-08-31', 1, 1, 'XYZ Foundation', 'Providing shelter for the homeless', 1),

(2, 8, 1, 'Medical mission in a developing country', 'Join us in providing medical care in a developing country', 'We are looking for medical professionals to join us in providing medical care to underserved communities in a developing country. Accommodation and meals will be provided.', '2023-09-01', '2023-09-15', 1, 0, 'Medical Missions International', 'Improving global health', 3),

(2, 11, 1, 'Environmental conservation project', 'Help us protect the environment', 'Join us in our efforts to protect the environment by participating in various conservation activities such as tree planting, wildlife monitoring, and environmental education. Accommodation and meals will be provided.', '2023-08-01', '2023-08-31', 1, 1, 'Green Earth Organization', 'Safeguarding the planet for future generations', 2),

(3, 8, 1, 'Disaster relief mission', 'Assist in disaster relief efforts', 'We are looking for volunteers to help with disaster relief efforts in areas affected by natural disasters. Tasks may include distributing relief supplies, providing medical care, and coordinating with local authorities. Accommodation and meals will be provided.', '2023-07-01', '2023-07-15', 1, 0, 'Red Cross', 'Responding to emergencies worldwide', 1),

(3, 2, 1, 'Orphanage volunteering', 'Volunteer at an orphanage', 'Join us in volunteering at an orphanage and help provide care and support to orphaned children. Tasks may include teaching, playing with the children, and assisting with daily activities. Accommodation and meals will be provided.', '2023-10-01', '2023-11-30', 2, 1, 'Hope Foundation', 'Supporting orphaned children', 2);
go


INSERT INTO [dbo].[mission_document] ([mission_id], [document_name], [doucment_type], [document_path])
VALUES
(8, 'Mission Image 1', 'image', 'D://CHARCHIL//Tavasoft-Training//Community Investment Dotnet Mvc//CommunityInvestment//CommunityInvestment//wwwroot//assets//CSR-initiative-stands-for-Coffee--and-Farmer-Equity.png'),
(9, 'Mission Image 2', 'image', 'D://CHARCHIL//Tavasoft-Training//Community Investment Dotnet Mvc//CommunityInvestment//CommunityInvestment//wwwroot//assets//Education-Supplies-for-Every--Pair-of-Shoes-Sold.png'),
(10, 'Mission Image 3', 'image', 'D://CHARCHIL//Tavasoft-Training//Community Investment Dotnet Mvc//CommunityInvestment//CommunityInvestment//wwwroot//assets//Grow-Trees-On-the-path-to-environment-sustainability.png'),
(11, 'Mission Image 4', 'image', 'https://example.com/mission-image-4.jpg'),
(12, 'Mission Image 5', 'image', 'https://example.com/mission-image-5.jpg'),
(13, 'Mission Image 6', 'image', 'https://example.com/mission-image-6.jpg')
go
