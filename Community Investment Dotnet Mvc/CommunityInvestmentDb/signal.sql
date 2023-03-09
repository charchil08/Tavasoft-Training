
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


alter proc spGetAllMissions
(
	@SearchKeyword varchar(100) = NULL,
	@Cities varchar(100) = NULL,
	@Themes varchar(100) = NULL,
	@Skills varchar(100) = NULL,
	@SortColumn varchar(100) = 'Title',
	@SortOrder varchar(4) = 'ASC',
	@PageIndex int = 1,
	@PageSize int = 5
)
as
begin
	declare @mission_card Table (
		mission_id bigint,
		title varchar(128),
		short_desc varchar(128),
		start_date datetime,
		end_date datetime,
		city_id bigint,
		city_name varchar(255),
		document_name varchar(255),
		document_path varchar(255),
		[type_id] tinyint,
		[type_name] varchar(16),
		[theme_id] bigint,
		[theme_name] varchar(128),

		--for time mission
		total_seat int,
		enrolled_user int,
		deadline datetime
	)
	
	insert into @mission_card 
	select m.mission_id, m.title, m.short_description, m.[start_date], m.end_date, m.city_id, c.[name] 'cityName', md.document_name, md.document_path, mty.mission_type_id, mty.[name] 'type name', mt.mission_theme_id, mt.[title] 'theme name', tm.total_seat 'total seats', tm.enrolled_user 'Enrolled user', tm.deadline 'deadline'
	from mission m
	inner join city c on c.city_id = m.city_id
	inner join mission_document md on md.mission_id = m.mission_id
	inner join mission_theme mt on mt.mission_theme_id = m.mission_theme_id
	inner join mission_type mty on mty.mission_type_id = m.mission_type_id
	inner join dbo.[time_mission] tm on tm.mission_id = m.mission_id 
		
	--if(@Cities is NULL AND @Themes IS NULL AND @Skills IS NULL AND @SearchKeyword IS NULL OR (@SearchKeyword = '' AND @Themes = '' AND @Skills = '' )
	--begin
	--end

	if (@SearchKeyword is not null and TRIM(@SearchKeyword) is not null)
	begin
		delete from @mission_card
		where mission_id not in (
		select mission_id from @mission_card
		where title LIKE '%' + @SearchKeyword+ '%')
	end
	if (@Cities is not null AND  TRIM(@Cities) is not null)
	begin
		delete from @mission_card
		where mission_id not in (
		select mission_id from @mission_card
		where [city_id] in ( select CAST(VALUE AS bigint) FROM string_split(@Cities,','))
		)
	end
	if(@Themes is not null AND TRIM(@Themes) IS NOT NULL)
	BEGIN
		DELETE FROM @mission_card
		WHERE mission_id not in (
			select mission_id from @mission_card
		where [theme_id] in ( select CAST(VALUE AS bigint) FROM string_split(@Themes,',')) 
		)
	END
	if(@Skills is not null AND TRIM(@Skills) is not null) 
	begin
		DELETE FROM @mission_card
		where mission_id not in (
			select mission_id from dbo.mission_skill
			where skill_id in (select CAST(VALUE AS bigint) FROM string_split(@Skills,','))
		)
	end

	-- pagination and sorting
	SELECT *, ROW_NUMBER() OVER(
        ORDER BY
            CASE WHEN @SortOrder = 'ASC'
                THEN CASE @SortColumn
                    WHEN 'start_date' THEN CONVERT(varchar(20), m.[start_date],120)
                    WHEN 'deadline' THEN CONVERT(varchar(20), deadline,120)
					WHEN 'lowest_available_seats' THEN '00000000' + CAST((CAST(total_seat AS INT) - CAST(enrolled_user AS INT)) AS VARCHAR(10))
                END
            END ASC,
            CASE WHEN @SortOrder = 'DESC'
                THEN CASE @SortColumn
                    WHEN 'start_date' THEN CONVERT(varchar(20),m.[start_date],120)
                    WHEN 'deadline' THEN CONVERT(varchar(20), deadline,120)
					WHEN 'lowest_available_seats' THEN '00000000' + CAST((CAST(total_seat AS INT) - CAST(enrolled_user AS INT)) AS VARCHAR(10))
				END
            END DESC
        ) AS RowNo, COUNT(1) OVER() AS TotalRows
	FROM @mission_card m
	ORDER BY RowNo
	OFFSET ((@PageIndex - 1) * @PageSize) ROWS 
	FETCH NEXT @PageSize ROWS ONLY;
end
go

exec spGetAllMissions @PageIndex=1, @Skills='1', @SortColumn='lowest_available_seats', @SortOrder='DESC'
go

select mission.mission_id from mission
inner join mission_skill on mission.mission_id = mission_skill.mission_id
where skill_id = 1

exec spGetAllMissions @Cities = '11,4,5', @Themes='2', @SearchKeyword=' proj'
go

sp_help time_mission



select * from dbo.skill
go
select * from mission
go

select * from time_mission
go
select * from mission_skill
go

insert into mission_skill (mission_id, skill_id)
values (2,1),(2,2), (3,1), (3,2), (3,3), (4,4), (4,5), (5,1), (5,4), (5,3), (6,1), (6,5)
go

insert into dbo.[time_mission] (mission_id, total_seat, enrolled_user, deadline, updated_at) 
values
(2, 20, 10, '2023-04-01 00:00:00','2023-03-09 10:30:00'),
(3, 15, 5, '2023-03-20 00:00:00', '2023-03-09 11:15:00'),
(4, 25, 0, '2023-05-01 00:00:00', '2023-03-09 12:00:00'),
(5, 40, 11, '2023-04-01 00:00:00','2023-03-09 10:30:00'),
(6, 55, 5, '2023-03-20 00:00:00', '2023-03-09 11:15:00'),
(8, 21, 9, '2023-05-01 00:00:00', '2023-03-09 12:00:00'),
(9, 19, 4, '2023-04-01 00:00:00','2023-03-09 10:30:00'),
(10, 50, 27, '2023-03-20 00:00:00', '2023-03-09 11:15:00'),
(11, 60, 33, '2023-05-01 00:00:00', '2023-03-09 12:00:00'),
(12, 18, 0, '2023-05-01 00:00:00', '2023-03-09 12:00:00');
go