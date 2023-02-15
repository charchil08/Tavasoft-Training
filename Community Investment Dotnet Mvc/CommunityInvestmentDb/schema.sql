create Database CommunityInvestment
go

create table admin
(
	admin_id bigint primary key identity(1,1) check (admin_id>0),
	first_name varchar(16),
	last_name varchar(16),
	email varchar(128) not null unique,
	password varchar(255) not null,
	created_at timestamp not null,
	updated_at datetime default null,
	deleted_at datetime default null
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
	updated_at datetime default null,
	deleted_at datetime default null
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
	updated_at datetime default null,
	deleted_at datetime default null
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
	updated_at datetime default null,
	deleted_at datetime default null,

	CONSTRAINT fk_city_cityId FOREIGN KEY (city_id) REFERENCES dbo.country(country_id)
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
	updated_at datetime default null,
	deleted_at datetime default null
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
	updated_at datetime default null,
	deleted_at datetime default null
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
	updated_at datetime default null,
	deleted_at datetime default null
);
GO