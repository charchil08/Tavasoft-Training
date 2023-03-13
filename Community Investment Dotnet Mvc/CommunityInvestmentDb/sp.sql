alter FUNCTION is_part_of_cities_fn(@countriesid NVARCHAR(MAX), @citiesid NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
    SELECT 
        country.country_id,
        CASE WHEN EXISTS (SELECT 1 FROM city WHERE city.country_id = country.country_id AND city.city_id IN (SELECT value FROM STRING_SPLIT(@citiesid, ','))) THEN 1 ELSE 0 END AS is_part_of_cities
    FROM 
        country
    WHERE 
        country.country_id IN (SELECT value FROM STRING_SPLIT(@countriesid, ','))
)
go

alter proc spGetAllMissions
	(
	@SearchKeyword varchar(100) = NULL,
	@Countries varchar(100) = NULL,
	@Cities varchar(100) = NULL,
	@Themes varchar(100) = NULL,
	@Skills varchar(100) = NULL,
	@SortColumn varchar(100) = 'title',
	@SortOrder varchar(4) = 'ASC',
	@PageIndex int = 1,
	@PageSize int = 6
)
as
BEGIN
	declare @mission_card Table (
		MissionId bigint,
		Title varchar(128),
		ShortDesc varchar(128),
		StartDate datetime,
		EndDate datetime,
		OrganizationName varchar(100),
		CityId bigint,
		CityName varchar(255),
		DocumentName varchar(255),
		DocumentPath varchar(255),
		TypeId tinyint,
		TypeName varchar(16),
		ThemeId bigint,
		ThemeName varchar(128),

		--for time mission
		TotalSeat int,
		EnrolledUser int,
		Deadline datetime,

		-- mission rating
		MissionRating decimal(2,1)
	)

	declare @ratings Table(
		MissionId bigint,
		AvgRating decimal(2,1)
	)

	insert into @ratings
	select m.mission_id, AVG(CAST(mr.rating as decimal(2,1))) from mission m
	left join mission_rating mr on mr.mission_id = m.mission_id 
	group by m.mission_id
	
	insert into @mission_card
	select m.mission_id, m.title, m.short_description, m.[start_date], m.end_date, m.organization_name, m.city_id, c.[name], md.document_name, md.document_path, mty.mission_type_id, mty.[name], mt.mission_theme_id, mt.[title] , tm.total_seat , tm.enrolled_user , tm.deadline, r.AvgRating
	from mission m
		inner join city c on c.city_id = m.city_id
		inner join mission_document md on md.mission_id = m.mission_id
		inner join mission_theme mt on mt.mission_theme_id = m.mission_theme_id
		inner join mission_type mty on mty.mission_type_id = m.mission_type_id
		inner join dbo.[time_mission] tm on tm.mission_id = m.mission_id
		inner join @ratings r on r.MissionId = m.mission_id

	--if(@Cities is NULL AND @Themes IS NULL AND @Skills IS NULL AND @SearchKeyword IS NULL OR (@SearchKeyword = '' AND @Themes = '' AND @Skills = '' )
	--BEGIN
	--end

	if (@SearchKeyword is not null and TRIM(@SearchKeyword) <> '')
	BEGIN
		delete from @mission_card
		where MissionId not in (
		select MissionId
		from @mission_card
		where Title LIKE '%' + @SearchKeyword+ '%')
	end

	-- country selected but not any cities selected
	if(@Countries is not null AND TRIM(@Countries) <> '' AND (@Cities is NULL OR TRIM(@Cities) = '') )
	BEGIN
		delete from @mission_card where CityId not in (
				select c.city_id
		from city c inner join country cnt on cnt.country_id = c.country_id
		where c.country_id IN (select CAST(VALUE AS bigint)
		FROM string_split(@Countries,',')))
	end

	if (@Cities is not null AND TRIM(@Cities) <> '')
	BEGIN

		delete from @mission_card
		where MissionId not in (
		select MissionId
		from @mission_card
		where CityId in (select CAST(VALUE AS bigint)
		FROM string_split(@Cities,',')))
	end
	if(@Themes is not null AND TRIM(@Themes) <> '')
	BEGIN
		DELETE FROM @mission_card
		WHERE MissionId not in (
			select MissionId
		from @mission_card
		where ThemeId in ( select CAST(VALUE AS bigint)
		FROM string_split(@Themes,',')) 
		)
	END
	if(@Skills is not null AND TRIM(@Skills) <> '') 
	BEGIN
		DELETE FROM @mission_card
		where MissionId not in (
			select mission_id
		from dbo.mission_skill
		where skill_id in (select CAST(VALUE AS bigint)
		FROM string_split(@Skills,','))
		)
	end

	--TODO: count of toal missions


	-- pagination and sorting
	SELECT *, ROW_NUMBER() OVER(
        ORDER BY
            CASE WHEN @SortOrder = 'ASC'
                THEN CASE @SortColumn
                    WHEN 'start_date' THEN CONVERT(varchar(20), m.StartDate,120)
                    WHEN 'deadline' THEN CONVERT(varchar(20), m.Deadline,120)
					WHEN 'lowest_available_seats' THEN '00000000' + CAST((CAST(TotalSeat AS INT) - CAST(EnrolledUser AS INT)) AS VARCHAR(10))
					WHEN 'title' THEN m.Title
                END
            END ASC,
            CASE WHEN @SortOrder = 'DESC'
                THEN CASE @SortColumn
                   WHEN 'start_date' THEN CONVERT(varchar(20), m.StartDate,120)
                    WHEN 'deadline' THEN CONVERT(varchar(20), m.Deadline,120)
					WHEN 'lowest_available_seats' THEN '00000000' + CAST((CAST(TotalSeat AS INT) - CAST(EnrolledUser AS INT)) AS VARCHAR(10))
					WHEN 'title' THEN m.Title
				END
            END DESC
        ) AS RowNo, COUNT_BIG(1) OVER() AS TotalRows
	FROM @mission_card m
	ORDER BY RowNo
	OFFSET ((@PageIndex - 1) * @PageSize) ROWS 
	FETCH NEXT @PageSize ROWS ONLY;
end
go


alter proc spFetchCityBasedOnCountry
(
	@Countries varchar(100) = NULL
)
as
begin
	select city.city_id, city.[name]
	from city
	where city.country_id in (select CAST(value as bigint) from string_split(@Countries, ','))
end
go


alter proc spGetMissionDetail
(
	@MissionId bigint
)
as 
begin
	declare @mission_detail Table (
		MissionId bigint,
		Title varchar(128),
		ShortDesc varchar(128),
		[Description] text,

		--Time_mission
		StartDate datetime,
		EndDate datetime,
		SeatsLeft int,
		Deadline datetime,

		--Goal mission
		GoalObjectiveText varchar(255),
		GoalValue int,
		AchievedGoalValue int,

		CityId bigint,
		CityName varchar(255),
		ThemeId bigint,
		ThemeName varchar(100),
		OrganizationName varchar(128),

		--Skill
		SkillId bigint,
		SkillName varchar(64),

		--availability
		AvailabilityId tinyint,
		AvailibilityDays varchar(16), 

		--rating & (user ramianing)
		Rating decimal(10,2)
	)

	declare @MissionType tinyint
	set @MissionType = (select mission_type_id from mission where mission_id = @MissionId);

	declare @MissionRating decimal(10,2)
	set @MissionRating = (select AVG(CAST(mr.rating as decimal(2,1))) from mission_rating mr where mission_id = @MissionId)

	if (@MissionType = 1)
	begin
		insert into @mission_detail 
		(MissionId, Title, ShortDesc, Description, StartDate, EndDate, SeatsLeft, Deadline, CityId, CityName, ThemeId, ThemeName, OrganizationName, SkillId, SkillName, AvailabilityId, AvailibilityDays, Rating)	
		select m.mission_id, m.[title], m.short_description, m.[description], m.[start_date], m.[end_date],
		 (tm.total_seat - tm.enrolled_user) as 'SeatsLeft', tm.deadline, ct.city_id, ct.[name], mt.mission_theme_id,
		 mt.[title] as 'ThemeName', m.organization_name, s.skill_id, s.skill_name, av.availability_id, av.[name],
		 @MissionRating  
		from mission m
		inner join time_mission tm on tm.mission_id = m.mission_id
		inner join city ct on ct.city_id = m.city_id
		inner join mission_theme mt on mt.mission_theme_id = m.mission_theme_id
		inner join dbo.[availability] av on av.availability_id = m.availability_id
		inner join mission_skill ms on ms.mission_id = m.mission_id
		inner join skill s on s.skill_id = ms.skill_id
		where m.mission_id = @MissionId
	end

	else
	begin
		insert into @mission_detail 
		(MissionId, Title, ShortDesc, Description, GoalObjectiveText, SeatsLeft, AchievedGoalValue, CityId, CityName, ThemeId, ThemeName, OrganizationName, SkillId, SkillName, AvailabilityId, AvailibilityDays,Rating)
		
		select m.mission_id, m.[title], m.short_description, m.[description], gm.goal_objective_text,
		 (gm.goal_value - gm.achieved_goal_value), gm.achieved_goal_value, ct.city_id, ct.[name], mt.mission_theme_id,
		 mt.[title] as 'ThemeName', m.organization_name, s.skill_id, s.skill_name, av.availability_id, av.[name],
		 @MissionRating
		from mission m
		inner join goal_mission gm on gm.mission_id = m.mission_id
		inner join city ct on ct.city_id = m.city_id
		inner join mission_theme mt on mt.mission_theme_id = m.mission_theme_id
		inner join dbo.[availability] av on av.availability_id = m.availability_id
		inner join mission_skill ms on ms.mission_id = m.mission_id
		inner join skill s on s.skill_id = ms.skill_id
		where m.mission_id = @MissionId
	end

	select * from @mission_detail for JSON PATH, ROOT('SpMissionDetail')

end
go