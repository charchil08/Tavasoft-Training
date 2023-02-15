create Database CommunityInvestment
go

create table admin(
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



