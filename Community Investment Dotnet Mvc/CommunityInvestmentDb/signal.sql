
use CommunityInvestment
go

select * from dbo.[user]
go


truncate table dbo.password_reset
go

select * from dbo.password_reset
go
-- hlxjrtuyfztfxmii

delete from dbo.[user] 
where user_id in (7)
go

exec spGetAllMissions @Countries='5,1', @Cities='11,42'

select country_id,count(*) from city c
where country_id in (5,1)
group by country_id


select value from string_split('5,1',',')



exec spGetAllMissions @PageIndex=1, @Skills='1', @SortColumn='lowest_available_seats', @SortOrder='DESC'
go

select mission.mission_id from mission
inner join mission_skill on mission.mission_id = mission_skill.mission_id
where skill_id = 1

exec spGetAllMissions @SortColumn='start_date', @SortOrder = 'DESC', @PageSize=20
go

exec spGetAllMissions @SortColumn='lowest_available_seats', @SortOrder='DESC'

sp_help time_mission 


insert into mission_skill (mission_id, skill_id)
values (2,1),(2,2), (3,1), (3,2), (3,3), (4,4), (4,5), (5,1), (5,4), (5,3), (6,1), (6,5)
go


select c.city_id from city c
			inner join country cnt on cnt.country_id = c.country_id
			where c.country_id in (5,1)


select * from mission_document

update mission_document
set document_path = './assets/Animal-welfare-&-save-birds-campaign.png'
where mission_id <= 5;

update mission_document
set document_path = './assets/CSR-initiative-stands-for-Coffee--and-Farmer-Equity.png'
where mission_id <= 10 and mission_id>=6;

update mission_document
set document_path = './assets/Grow-Trees-On-the-path-to-environment-sustainability.png'
where mission_id <= 18 and mission_id>=15;


update mission_document
set document_path = './assets/Plantation-and-Afforestation-programme.png'
where mission_id > 18;

select * from mission_document

select * from city

INSERT INTO [dbo].[user] ([first_name], [last_name], [email], [password], [phone_number], [avatar], [why_i_volunteer], [employee_id], [department], [city_id], [country_id], [profile_text], [linked_in_url], [title], [status])
VALUES 
('John', 'Doe', 'john.doe1@example.com', 'password1', '+1-555-1234', './assets/user-img-large.png', 'I volunteer to help my community', 'E1234', 'IT', 1, 1, 'I am a software engineer with a passion for volunteering', 'https://www.linkedin.com/in/johndoe1', 'Software Engineer', 1),
('Jane', 'Doe', 'jane.doe2@example.com', 'password2', '+1-555-5678', './assets/volunteer1.png', 'I love giving back to my community', 'E5678', 'Marketing', 2, 1, 'I am a marketer with a passion for social good', 'https://www.linkedin.com/in/janedoe2', 'Marketing Manager', 1),
('Bob', 'Smith', 'bob.smith3@example.com', 'password3', '+1-555-9012', './assets/volunteer2.png', 'I want to make a difference in the world', 'E9012', 'Sales', 3, 1, 'I am a salesperson who loves to help others', 'https://www.linkedin.com/in/bobsmith3', 'Sales Representative', 1),
('Emily', 'Johnson', 'emily.johnson4@example.com', 'password4', '+1-555-3456', './assets/volunteer3.png', 'I enjoy helping my community', 'E3456', 'Customer Service', 4, 1, 'I am a customer service representative with a heart for service', 'https://www.linkedin.com/in/emilyjohnson4', 'Customer Service Representative', 1),
('Mike', 'Brown', 'mike.brown5@example.com', 'password5', '+1-555-7890', './assets/volunteer4.png', 'I believe in giving back to my community', 'E7890', 'Engineering', 5, 1, 'I am a mechanical engineer who loves to volunteer', 'https://www.linkedin.com/in/mikebrown5', 'Mechanical Engineer', 1),
('Sarah', 'Wilson', 'sarah.wilson6@example.com', 'password6', '+1-555-2345', './assets/volunteer5.png', 'I want to make a positive impact in my community', 'E2345', 'HR', 6, 1, 'I am an HR professional who cares about people', 'https://www.linkedin.com/in/sarahwilson6', 'HR Manager', 1),
('David', 'Davis', 'david.davis7@example.com', 'password7', '+1-555-6789', './assets/volunteer6.png', 'I volunteer to help those in need', 'E6789', 'IT', 7, 1, 'I am a software developer who loves to give back', 'https://www.linkedin.com/in/daviddavis7', 'Software Developer',1),
('John', 'Doe', 'john.doe@example.com', 'password1', '1234567890', './assets/volunteer7.png', 'I volunteer to help my community', 'E1234', 'Marketing', 1, 1, 'I am a marketing professional with 5 years of experience.', 'https://www.linkedin.com/in/john-doe/', 'Marketing Manager', 1),
('Jane', 'Smith', 'jane.smith@example.com', 'password2', '0987654321', './assets/volunteer8.png', 'I love to volunteer and give back to my community.', 'E5678', 'HR', 2, 1, 'I have been working in HR for 10 years and enjoy helping others.', 'https://www.linkedin.com/in/jane-smith/', 'HR Director', 1),
('Mike', 'Johnson', 'mike.johnson@example.com', 'password31', '5555555555', './assets/volunteer9.png', 'I enjoy volunteering for local charities.', 'E9012', 'Finance', 42, 5, 'I am a finance professional with a passion for helping others.', 'https://www.linkedin.com/in/mike-johnson/', 'Finance Manager', 1),
('Sarah', 'Williams', 'sarah.williams@example.com', 'password32', '7777777777', './assets/volunteer6.png', 'I volunteer at my local animal shelter on weekends.', 'E3456', 'Operations', 43, 5, 'I am an operations expert with experience managing large teams.', 'https://www.linkedin.com/in/sarah-williams/', 'Operations Director', 1),
('Sunita', 'Williams', 'sunita@example.com', 'password32', '8787878787', './assets/volunteer6.png', 'I volunteer at my local animal shelter on weekends.', 'E3456', 'Operations', 43, 5, 'I am an operations expert with experience managing large teams.', 'https://www.linkedin.com/in/sunita/', 'Senior Director', 1)
go

select * from mission
go
--update password

-- rating entry
INSERT INTO mission_rating (mission_id, user_id, rating)
SELECT m.mission_id, u.[user_id], ABS(CHECKSUM(NEWID())) % 5 + 1
FROM mission m
CROSS JOIN [user] u
WHERE u.user_id BETWEEN 3 AND 19
AND m.mission_id BETWEEN 2 AND 22
AND NOT EXISTS (
    SELECT 1
    FROM mission_rating mr
    WHERE mr.mission_id = m.mission_id
    AND mr.user_id = u.user_id
);

SELECT mission_id, AVG(CAST(rating AS decimal(10,2))) FROM mission_rating group by mission_id;

insert into mission_rating (mission_id, user_id, rating)
values (2,20,4)

select mission_id,sum(rating) from mission_rating group by mission_id;

select m.mission_id, m.title, m.short_description, m.[start_date], m.end_date, m.organization_name, m.city_id, c.[name], md.document_name, md.document_path, mty.mission_type_id, mty.[name], mt.mission_theme_id, mt.[title] , tm.total_seat , tm.enrolled_user , tm.deadline, AVG(mr.rating)
	from mission m
		inner join city c on c.city_id = m.city_id
		inner join mission_document md on md.mission_id = m.mission_id
		inner join mission_theme mt on mt.mission_theme_id = m.mission_theme_id
		inner join mission_type mty on mty.mission_type_id = m.mission_type_id
		inner join dbo.[time_mission] tm on tm.mission_id = m.mission_id
		inner join dbo.mission_rating mr on mr.mission_id = m.mission_id
	where m.mission_id = 2;


select * from time_mission
go

select * from mission
where mission_type_id = 2
go

alter table goal_mission
add achieved_goal_value int not null default 0
go

insert into goal_mission (mission_id, goal_objective_text, goal_value, achieved_goal_value)
values 
(7, 'orphange vol', 10000, 500),
(13, 'orphange vol 13', 8000, 4200),
(22, 'orphange vol 22', 9000, 0)
go

exec spGetMissionDetail @MissionId=2

select STRING_AGG(skill.skill_name, ', ') from mission_skill
inner join skill on skill.skill_id = mission_skill.skill_id
where mission_skill.mission_id = 2

select * from skill;
go

select * from mission_skill
go

select AVG(CAST(rating AS decimal(10,2))) FROM mission_rating 


--json based ->
--dictionary ->
