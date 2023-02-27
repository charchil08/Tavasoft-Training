create Database CommunityInvestment
go

use CommunityInvestment
go


-- ---------------------
-- Enum kind of tables
-- ---------------------

-- Create a new table called 'mission_type' in schema 'dbo'
-- Drop the table if it already exists
IF OBJECT_ID('dbo.mission_type', 'U') IS NOT NULL
DROP TABLE dbo.mission_type
GO
-- Create the table in the specified schema
CREATE TABLE dbo.mission_type
(
	mission_type_id TINYINT PRIMARY KEY IDENTITY(1,1) CHECK (mission_type_id > 0),
	[name] VARCHAR(16),
	created_at timestamp not null,
	updated_at datetime,
	deleted_at datetime
);
GO


-- Create a new table called 'availability' in schema 'dbo'
-- Drop the table if it already exists
IF OBJECT_ID('dbo.availability', 'U') IS NOT NULL
DROP TABLE dbo.availability
GO
-- Create the table in the specified schema
CREATE TABLE dbo.availability
(
	availability_id TINYINT PRIMARY KEY IDENTITY(1,1) CHECK (availability_id > 0),
	[name] VARCHAR(16),
	created_at timestamp not null,
	updated_at datetime,
	deleted_at datetime

);
GO



-- Create a new table called 'approval_status' in schema 'dbo'
-- Drop the table if it already exists
IF OBJECT_ID('dbo.approval_status', 'U') IS NOT NULL
DROP TABLE dbo.approval_status
GO

-- Create the table in the specified schema
CREATE TABLE dbo.approval_status
(
	approval_status_id TINYINT PRIMARY KEY IDENTITY(1,1) CHECK (approval_status_id > 0),
	[name] VARCHAR(16) NOT NULL,
	created_at timestamp not null,
	updated_at datetime,
	deleted_at datetime,
);
GO


-- Create a new table called 'story_status' in schema 'dbo'
-- Drop the table if it already exists
IF OBJECT_ID('dbo.story_status', 'U') IS NOT NULL
DROP TABLE dbo.story_status
GO
-- Create the table in the specified schema
CREATE TABLE dbo.story_status
(
	story_status_id TINYINT PRIMARY KEY IDENTITY(1,1) CHECK (story_status_id > 0),
	[name] VARCHAR(16) NOT NULL,
	created_at timestamp not null,
	updated_at datetime,
	deleted_at datetime,
);
GO



-- INSERT VALUES IN ABOVE ENUM KIND OF TABLES
-- SO THAT DEFAULT VALUE CAN BE SET IN MAIN TABLES.

INSERT INTO mission_type
	([name])
VALUES
	('TIME'),
	('GOAL')
GO


INSERT INTO dbo.availability
	([name])
VALUES
	('DAILY'),
	('WEEKLY'),
	('WEEK-END'),
	('MONTHLY')
GO


INSERT INTO dbo.approval_status
	([name])
VALUES
	('PENDING'),
	('APPROVED'),
	('DECLINED')
GO


INSERT INTO dbo.story_status
	([name])
VALUES
	('DRAFT'),
	('PENDING'),
	('PUBLISHED'),
	('DECLINED')
GO



-- #####################
-- MAIN DATABASE TABLES
-- #####################

create table [admin]
(
	admin_id bigint primary key identity(1,1) check (admin_id>0),
	first_name varchar(16),
	last_name varchar(16),
	email varchar(128) not null unique,
	password varchar(255) not null,
	created_at timestamp not null,
	updated_at datetime  ,
	deleted_at datetime
)
go

-- Create a new table called 'banner' in schema 'dbo'
-- Drop the table if it already exists
IF OBJECT_ID('dbo.banner', 'U') IS NOT NULL
DROP TABLE dbo.banner
GO
-- Create the table in the specified schema
CREATE TABLE dbo.banner
(
	banner_id bigint PRIMARY KEY IDENTITY(1,1) CHECK (banner_id > 0),
	image VARCHAR(512),
	[text] TEXT,
	sort_order int DEFAULT 0,
	created_at timestamp not null,
	updated_at datetime  ,
	deleted_at datetime
);
GO

-- Create a new table called 'country' in schema 'dbo'
-- Drop the table if it already exists
IF OBJECT_ID('dbo.country', 'U') IS NOT NULL
DROP TABLE dbo.country
GO
-- Create the table in the specified schema
CREATE TABLE dbo.country
(
	country_id bigint PRIMARY KEY IDENTITY(1,1) CHECK (country_id > 0),
	name VARCHAR(255) not null,
	ISO VARCHAR(16) not NULL,
	created_at timestamp not null,
	updated_at datetime  ,
	deleted_at datetime
);
GO

-- Create a new table called 'city' in schema 'dbo'
-- Drop the table if it already exists
IF OBJECT_ID('dbo.city', 'U') IS NOT NULL
DROP TABLE dbo.city
GO
-- Create the table in the specified schema
CREATE TABLE dbo.city
(
	city_id bigint PRIMARY KEY IDENTITY(1,1) CHECK (city_id > 0),
	country_id bigint,
	name VARCHAR(255) not null,
	created_at timestamp not null,
	updated_at datetime  ,
	deleted_at datetime
	,
);
GO

-- Create a new table called 'cms_page' in schema 'dbo'
-- Drop the table if it already exists
IF OBJECT_ID('dbo.cms_page', 'U') IS NOT NULL
DROP TABLE dbo.cms_page
GO
-- Create the table in the specified schema
CREATE TABLE dbo.cms_page
(
	cms_page_id bigint PRIMARY KEY IDENTITY(1,1) CHECK (cms_page_id > 0),
	title VARCHAR(255) not null,
	desciption text,
	slug varchar(255) not null,
	status BIT DEFAULT 1,
	created_at timestamp not null,
	updated_at datetime  ,
	deleted_at datetime
);
GO


-- Create a new table called 'mission_theme' in schema 'dbo'
-- Drop the table if it already exists
IF OBJECT_ID('dbo.mission_theme', 'U') IS NOT NULL
DROP TABLE dbo.mission_theme
GO
-- Create the table in the specified schema
CREATE TABLE dbo.mission_theme
(
	mission_theme_id bigint PRIMARY KEY IDENTITY(1,1) CHECK (mission_theme_id > 0),
	title VARCHAR(255) not null,
	status TINYINT not NULL,
	created_at timestamp not null,
	updated_at datetime  ,
	deleted_at datetime
);
GO


-- Create a new table called 'password_reset' in schema 'dbo'
-- Drop the table if it already exists
IF OBJECT_ID('dbo.password_reset', 'U') IS NOT NULL
DROP TABLE dbo.password_reset
GO
-- Create the table in the specified schema
CREATE TABLE dbo.password_reset
(
	email varchar(191) not NULL,
	token varchar(191) not NULL,
	created_at timestamp not null,
);
GO


-- Create a new table called 'skill' in schema 'dbo'
-- Drop the table if it already exists
IF OBJECT_ID('dbo.skill', 'U') IS NOT NULL
DROP TABLE dbo.skill
GO
-- Create the table in the specified schema
CREATE TABLE dbo.skill
(
	skill_id bigint PRIMARY KEY IDENTITY(1,1) CHECK (skill_id > 0),
	skill_name VARCHAR(64),
	status TINYINT not NULL DEFAULT 1,
	created_at timestamp not null,
	updated_at datetime  ,
	deleted_at datetime
);
GO


-- Create a new table called 'user' in schema 'dbo'
-- Drop the table if it already exists
IF OBJECT_ID('dbo.user', 'U') IS NOT NULL
DROP TABLE dbo.[user]
GO
-- Create the table in the specified schema
CREATE TABLE dbo.[user]
(
	[user_id] bigint PRIMARY KEY IDENTITY(1,1) CHECK (user_id > 0),
	first_name VARCHAR(16),
	last_name VARCHAR(16),
	email VARCHAR(128) not null,
	[password] VARCHAR(255) NOT NULL,
	phone_number int not NULL,
	avatar VARCHAR(2048),
	why_i_volunteer text,
	employee_id VARCHAR(16),
	department VARCHAR(16),
	city_id bigint not null,
	country_id bigint not NULL,
	profile_text text,
	linked_in_url VARCHAR(255),
	[title] VARCHAR(255),
	status bit not NULL DEFAULT 1,
	created_at timestamp not null,
	updated_at datetime,
	deleted_at datetime,

);
GO


-- Create a new table called 'user_skill' in schema 'dbo'
-- Drop the table if it already exists
IF OBJECT_ID('dbo.user_skill', 'U') IS NOT NULL
DROP TABLE dbo.user_skill
GO
-- Create the table in the specified schema
CREATE TABLE dbo.user_skill
(
	user_skill_id bigint PRIMARY KEY IDENTITY(1,1) CHECK (user_skill_id > 0),
	user_id bigint not null,
	skill_id bigint not null,
	created_at timestamp not null,
	updated_at datetime,
	deleted_at datetime
);
GO



-- Create a new table called 'mission' in schema 'dbo'
-- Drop the table if it already exists
IF OBJECT_ID('dbo.mission', 'U') IS NOT NULL
DROP TABLE dbo.mission
GO
-- Create the table in the specified schema
CREATE TABLE dbo.mission
(
	mission_id bigint PRIMARY KEY IDENTITY(1,1) CHECK (mission_id > 0),
	mission_theme_id bigint not NULL,
	city_id bigint not null,
	country_id bigint not null,
	title VARCHAR(128) not NULL,
	short_description text,
	[description] text,
	start_date DATETIME,
	end_date DATETIME,
	mission_type_id TINYINT not null,
	[status] bit not null DEFAULT 0,
	organization_name VARCHAR(255),
	organization_detail VARCHAR(255),
	availability_id TINYINT,
	created_at timestamp not null,
	updated_at datetime,
	deleted_at datetime,
);
GO



-- Create a new table called 'mission_application' in schema 'dbo'
-- Drop the table if it already exists
IF OBJECT_ID('dbo.mission_application', 'U') IS NOT NULL
DROP TABLE dbo.mission_application
GO
-- Create the table in the specified schema
CREATE TABLE dbo.mission_application
(
	mission_application_id bigint PRIMARY KEY IDENTITY(1,1) CHECK (mission_application_id > 0),
	mission_id bigint not null,
	[user_id] bigint NOT null,
	applied_at datetime not NULL,
	approval_status_id TINYINT,
	created_at timestamp not null,
	updated_at datetime,
	deleted_at datetime,
);
GO

-- Create a new table called 'mission_document' in schema 'dbo'
-- Drop the table if it already exists
IF OBJECT_ID('dbo.mission_document', 'U') IS NOT NULL
DROP TABLE dbo.mission_document
GO
-- Create the table in the specified schema
CREATE TABLE dbo.mission_document
(
	mission_document_id bigint PRIMARY KEY IDENTITY(1,1) CHECK (mission_document_id > 0),
	mission_id bigint not null,
	document_name VARCHAR(255) NOT NULL,
	doucment_type VARCHAR(255),
	document_path VARCHAR(255),
	created_at timestamp not null,
	updated_at datetime,
	deleted_at datetime,
);
GO

-- Create a new table called 'mission_invite' in schema 'dbo'
-- Drop the table if it already exists
IF OBJECT_ID('dbo.mission_invite', 'U') IS NOT NULL
DROP TABLE dbo.mission_invite
GO
-- Create the table in the specified schema
CREATE TABLE dbo.mission_invite
(
	mission_invite_id bigint PRIMARY KEY IDENTITY(1,1) CHECK (mission_invite_id > 0),
	mission_id bigint not null,
	from_user_id bigint not NULL,
	to_user_id bigint not NULL,
	created_at timestamp not null,
	updated_at datetime,
	deleted_at datetime,
);
GO

-- Create a new table called 'mission_media' in schema 'dbo'
-- Drop the table if it already exists
IF OBJECT_ID('dbo.mission_media', 'U') IS NOT NULL
DROP TABLE dbo.mission_media
GO
-- Create the table in the specified schema
CREATE TABLE dbo.mission_media
(
	mission_media_id bigint PRIMARY KEY IDENTITY(1,1) CHECK (mission_media_id > 0),
	mission_id bigint not null,
	media_name VARCHAR(64),
	media_type VARCHAR(4),
	media_path VARCHAR(255),
	[default] bit,
	created_at timestamp not null,
	updated_at datetime,
	deleted_at datetime,
);
GO

-- Create a new table called 'mission_rating' in schema 'dbo'
-- Drop the table if it already exists
IF OBJECT_ID('dbo.mission_rating', 'U') IS NOT NULL
DROP TABLE dbo.mission_rating
GO
-- Create the table in the specified schema
CREATE TABLE dbo.mission_rating
(
	mission_rating_id bigint PRIMARY KEY IDENTITY(1,1) CHECK (mission_rating_id > 0),
	mission_id bigint not null,
	[user_id] bigint not null,
	media_name VARCHAR(64),
	rating TINYINT CHECK (rating<=5 and rating>=1),
	created_at timestamp not null,
	updated_at datetime,
	deleted_at datetime,
);
GO

-- Create a new table called 'mission_skill' in schema 'dbo'
-- Drop the table if it already exists
IF OBJECT_ID('dbo.mission_skill', 'U') IS NOT NULL
DROP TABLE dbo.mission_skill
GO
-- Create the table in the specified schema
CREATE TABLE dbo.mission_skill
(
	mission_skill_id bigint PRIMARY KEY IDENTITY(1,1) CHECK (mission_skill_id > 0),
	mission_id bigint,
	skill_id bigint not null,
	media_name VARCHAR(64),
	rating TINYINT CHECK (rating<=5 and rating>=1),
	created_at timestamp not null,
	updated_at datetime,
	deleted_at datetime,
);
GO

-- Create a new table called 'story' in schema 'dbo'
-- Drop the table if it already exists
IF OBJECT_ID('dbo.story', 'U') IS NOT NULL
DROP TABLE dbo.story
GO
-- Create the table in the specified schema
CREATE TABLE dbo.story
(
	story_id bigint PRIMARY KEY IDENTITY(1,1) CHECK (story_id > 0),
	mission_id bigint not null,
	[user_id] bigint not null,
	title VARCHAR(255),
	[description] text,
	story_status_id TINYINT not null DEFAULT 1,
	--set default to draft
	published_at DATETIME ,
	created_at timestamp not null,
	updated_at datetime,
	deleted_at datetime,
);
GO

-- Create a new table called 'story_invite' in schema 'dbo'
-- Drop the table if it already exists
IF OBJECT_ID('dbo.story_invite', 'U') IS NOT NULL
DROP TABLE dbo.story_invite
GO
-- Create the table in the specified schema
CREATE TABLE dbo.story_invite
(
	story_invite_id bigint PRIMARY KEY IDENTITY(1,1) CHECK (story_invite_id > 0),
	story_id bigint not null,
	from_user_id bigint not NULL,
	to_user_id bigint not NULL,
	created_at timestamp not null,
	updated_at datetime,
	deleted_at datetime,
);
GO


-- Create a new table called 'story_media' in schema 'dbo'
-- Drop the table if it already exists
IF OBJECT_ID('dbo.story_media', 'U') IS NOT NULL
DROP TABLE dbo.story_media
GO
-- Create the table in the specified schema
CREATE TABLE dbo.story_media
(
	story_media_id bigint PRIMARY KEY IDENTITY(1,1) CHECK (story_media_id > 0),
	story_id bigint not null,
	[type] VARCHAR(8) not NULL,
	[path] text,
	created_at timestamp not null,
	updated_at datetime,
	deleted_at datetime,

);
GO

-- Create a new table called 'timesheet' in schema 'dbo'
-- Drop the table if it already exists
IF OBJECT_ID('dbo.timesheet', 'U') IS NOT NULL
DROP TABLE dbo.timesheet
GO
-- Create the table in the specified schema
CREATE TABLE dbo.timesheet
(
	timesheet_id bigint PRIMARY KEY IDENTITY(1,1) CHECK (timesheet_id > 0),
	[user_id] bigint not NULL,
	mission_id bigint not NULL,
	[time] TIME,
	[action] int,
	date_volunteered DATETIME not null,
	notes text,
	approval_status_id TINYINT not null DEFAULT 1,
	--timesheet_status_id -> default pending
	created_at timestamp not null,
	updated_at datetime,
	deleted_at datetime,
);
GO


-- Create a new table called 'comment' in schema 'dbo'
-- Drop the table if it already exists
IF OBJECT_ID('dbo.comment', 'U') IS NOT NULL
DROP TABLE dbo.comment
GO
-- Create the table in the specified schema
CREATE TABLE dbo.comment
(
	comment_id bigint PRIMARY KEY IDENTITY(1,1) CHECK (comment_id > 0),
	[user_id] bigint not NULL,
	mission_id bigint not NULL,
	approval_status_id TINYINT not NULL DEFAULT 1,
	created_at timestamp,
	updated_at datetime,
	deleted_at datetime
);
GO


-- Create a new table called 'favourite_mission' in schema 'dbo'
-- Drop the table if it already exists
IF OBJECT_ID('dbo.favourite_mission', 'U') IS NOT NULL
DROP TABLE dbo.favourite_mission
GO
-- Create the table in the specified schema
CREATE TABLE dbo.favourite_mission
(
	favourite_mission_id bigint PRIMARY KEY IDENTITY(1,1) CHECK (favourite_mission_id > 0),
	[user_id] bigint not NULL,
	mission_id bigint not NULL,
	created_at timestamp not null,
	updated_at datetime,
	deleted_at datetime
);
GO


-- Create a new table called 'goal_mission' in schema 'dbo'
-- Drop the table if it already exists
IF OBJECT_ID('dbo.goal_mission', 'U') IS NOT NULL
DROP TABLE dbo.goal_mission
GO
-- Create the table in the specified schema
CREATE TABLE dbo.goal_mission
(
	goal_mission_id bigint PRIMARY KEY IDENTITY(1,1) CHECK (goal_mission_id > 0),
	mission_id bigint not NULL,
	goal_objective_text VARCHAR(255),
	goal_value int not null,
	created_at timestamp not null,
	updated_at datetime,
	deleted_at datetime
);
GO


-- ##############
-- Foregin Key
-- ##############


ALTER TABLE city
ADD CONSTRAINT fk_country_countryId__city__countryId 
FOREIGN KEY (country_id) REFERENCES country(country_id)
GO


ALTER TABLE comment
ADD CONSTRAINT fk_user_userId__comment__userId 
FOREIGN KEY (user_id) REFERENCES [user](user_id)
GO


ALTER TABLE comment
ADD CONSTRAINT fk_mission_missionId__comment__missionId 
FOREIGN KEY (mission_id) REFERENCES [mission](mission_id)
GO

ALTER TABLE comment
ADD CONSTRAINT fk_approvalStatus_approvalStatusId__comment__approvalStatusId 
FOREIGN KEY (approval_status_id) REFERENCES [approval_status](approval_status_id)
GO

ALTER TABLE favourite_mission
ADD CONSTRAINT fk_user_userId__favouriteMission__userId 
FOREIGN KEY (user_id) REFERENCES [user](user_id)
GO

ALTER TABLE favourite_mission
ADD CONSTRAINT fk_mission_missionId__favouriteMission__missionId 
FOREIGN KEY (mission_id) REFERENCES [mission](mission_id)
GO


ALTER TABLE goal_mission
ADD CONSTRAINT fk_mission_missionId__goalMission__missionId 
FOREIGN KEY (mission_id) REFERENCES [mission](mission_id)
GO

ALTER TABLE mission
ADD CONSTRAINT fk_missionTheme_missionThemeId__mission__missionThemeId 
FOREIGN KEY (mission_theme_id) REFERENCES [mission_theme](mission_theme_id)
GO

ALTER TABLE mission
ADD CONSTRAINT fk_city_cityId__mission__cityId 
FOREIGN KEY (city_id) REFERENCES [city](city_id)
GO

ALTER TABLE mission
ADD CONSTRAINT fk_country_countryId__mission__countryId 
FOREIGN KEY (country_id) REFERENCES [country](country_id)
GO

ALTER TABLE mission
ADD CONSTRAINT fk_missionType_missionTypeId__mission__missionTypeId 
FOREIGN KEY (mission_type_id) REFERENCES [mission_type](mission_type_id)
GO

ALTER TABLE mission
ADD CONSTRAINT fk_availability_availabilittyId__mission__availabilittyId 
FOREIGN KEY (availability_id) REFERENCES [availability](availability_id)
GO

-- Changes in fk

ALTER TABLE mission_application
ADD CONSTRAINT fk_user_userId__missionApplication__userId 
FOREIGN KEY (user_id) REFERENCES [user](user_id)
GO

ALTER TABLE mission_application
ADD CONSTRAINT fk_mission_missionId__missionApplication_missionId 
FOREIGN KEY (mission_id) REFERENCES [mission](mission_id)
GO


ALTER TABLE mission_application
ADD CONSTRAINT fk_approvalStatus_approvalStatusId__missionApplication__approvalStatusId 
FOREIGN KEY (approval_status_id) REFERENCES [approval_status](approval_status_id)
GO


ALTER TABLE mission_document
ADD CONSTRAINT fk_mission_missionId__missionDocument__missionId 
FOREIGN KEY (mission_id) REFERENCES [mission](mission_id)
GO


ALTER TABLE mission_invite
ADD CONSTRAINT fk_mission_missionId__missionInvite__missionId 
FOREIGN KEY (mission_id) REFERENCES [mission](mission_id)
GO

ALTER TABLE mission_invite
ADD CONSTRAINT fk_user_userId__missionInvite__fromUserId 
FOREIGN KEY (from_user_id) REFERENCES [user](user_id)
GO

ALTER TABLE mission_invite
ADD CONSTRAINT fk_user_userId__missionInvite__toUserId 
FOREIGN KEY (to_user_id) REFERENCES [user](user_id)
GO


ALTER TABLE mission_media
ADD CONSTRAINT fk_mission_missionId__missionMedia__missionId 
FOREIGN KEY (mission_id) REFERENCES [mission](mission_id)
GO

ALTER TABLE mission_rating
ADD CONSTRAINT fk_mission_missionId__missionRating__missionId 
FOREIGN KEY (mission_id) REFERENCES [mission](mission_id)
GO

ALTER TABLE mission_rating
ADD CONSTRAINT fk_user_userId__missionRating__userId 
FOREIGN KEY (user_id) REFERENCES [user](user_id)
GO

ALTER TABLE mission_skill
ADD CONSTRAINT fk_mission_missionId__missionSkill__missionId 
FOREIGN KEY (mission_id) REFERENCES [mission](mission_id)
GO


ALTER TABLE mission_skill
ADD CONSTRAINT fk_skill_skillId__missionSkill__skillId 
FOREIGN KEY (skill_id) REFERENCES [skill](skill_id)
GO


ALTER TABLE story
ADD CONSTRAINT fk_mission_missionId__story__missionId 
FOREIGN KEY (mission_id) REFERENCES [mission](mission_id)
GO

ALTER TABLE story
ADD CONSTRAINT fk_user_userId__story__userId 
FOREIGN KEY (user_id) REFERENCES [user](user_id)
GO

ALTER TABLE story
ADD CONSTRAINT fk_storyStatus_storyStatusId__story__storyStatusId 
FOREIGN KEY (story_status_id) REFERENCES [story_status](story_status_id)
GO

ALTER TABLE story_invite
ADD CONSTRAINT fk_story_storyId__storyInvite__storyId 
FOREIGN KEY (story_id) REFERENCES [story](story_id)
GO

ALTER TABLE story_invite
ADD CONSTRAINT fk_user_userId__storyInvite__fromUserId 
FOREIGN KEY (from_user_id) REFERENCES [user](user_id)
GO

ALTER TABLE story_invite
ADD CONSTRAINT fk_user_userId__storyInvite__toUserId 
FOREIGN KEY (to_user_id) REFERENCES [user](user_id)
GO

ALTER TABLE story_media
ADD CONSTRAINT fk_story_storyId__storyMedia__storyId 
FOREIGN KEY (story_id) REFERENCES [story](story_id)
GO

ALTER TABLE timesheet
ADD CONSTRAINT fk_mission_missionId__timesheet__missionId 
FOREIGN KEY (mission_id) REFERENCES [mission](mission_id)
GO

ALTER TABLE timesheet
ADD CONSTRAINT fk_user_userId__timesheet__userId 
FOREIGN KEY (user_id) REFERENCES [user](user_id)
GO


ALTER TABLE timesheet
ADD CONSTRAINT fk_approvalStatus_approvalStatusId__timesheet__approvalStatusId 
FOREIGN KEY (approval_status_id) REFERENCES [approval_status](approval_status_id)
GO

ALTER TABLE [user]
ADD CONSTRAINT fk_city_cityId__user__cityId 
FOREIGN KEY (city_id) REFERENCES [city](city_id)
GO

ALTER TABLE [user]
ADD CONSTRAINT fk_country_countryId__user__countryId 
FOREIGN KEY (country_id) REFERENCES [country](country_id)
GO

ALTER TABLE user_skill
ADD CONSTRAINT fk_user_userId__userSkill__userId 
FOREIGN KEY (user_id) REFERENCES [user](user_id)
GO

ALTER TABLE user_skill
ADD CONSTRAINT fk_skill_skillId__userSkill__skillId 
FOREIGN KEY (skill_id) REFERENCES [skill](skill_id)
GO