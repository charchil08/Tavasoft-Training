alter proc spGetAllMissions
(
	@SearchKeyword varchar(100) = NULL,
	@Cities varchar(100) = NULL,
	@Themes varchar(100) = NULL,
	@Skills varchar(100) = NULL,
	@SortColumn varchar(100) = 'Title',
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
		Deadline datetime
	)
	
	insert into @mission_card 
	select m.mission_id, m.title, m.short_description, m.[start_date], m.end_date, m.city_id, c.[name], md.document_name, md.document_path, mty.mission_type_id, mty.[name], mt.mission_theme_id, mt.[title] , tm.total_seat ,tm.enrolled_user , tm.deadline
	from mission m
	inner join city c on c.city_id = m.city_id
	inner join mission_document md on md.mission_id = m.mission_id
	inner join mission_theme mt on mt.mission_theme_id = m.mission_theme_id
	inner join mission_type mty on mty.mission_type_id = m.mission_type_id
	inner join dbo.[time_mission] tm on tm.mission_id = m.mission_id 
		
	--if(@Cities is NULL AND @Themes IS NULL AND @Skills IS NULL AND @SearchKeyword IS NULL OR (@SearchKeyword = '' AND @Themes = '' AND @Skills = '' )
	--BEGIN
	--end

	if (@SearchKeyword is not null and TRIM(@SearchKeyword) <> '')
	BEGIN
		delete from @mission_card
		where MissionId not in (
		select MissionId from @mission_card
		where Title LIKE '%' + @SearchKeyword+ '%')
	end
	if (@Cities is not null AND  TRIM(@Cities) <> '')
	BEGIN
		delete from @mission_card
		where MissionId not in (
		select MissionId from @mission_card
		where CityId in ( select CAST(VALUE AS bigint) FROM string_split(@Cities,','))
		)
	end
	if(@Themes is not null AND TRIM(@Themes) <> '')
	BEGIN
		DELETE FROM @mission_card
		WHERE MissionId not in (
			select MissionId from @mission_card
		where ThemeId in ( select CAST(VALUE AS bigint) FROM string_split(@Themes,',')) 
		)
	END
	if(@Skills is not null AND TRIM(@Skills) <> '') 
	BEGIN
		DELETE FROM @mission_card
		where MissionId not in (
			select mission_id from dbo.mission_skill
			where skill_id in (select CAST(VALUE AS bigint) FROM string_split(@Skills,','))
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
                END
            END ASC,
            CASE WHEN @SortOrder = 'DESC'
                THEN CASE @SortColumn
                   WHEN 'start_date' THEN CONVERT(varchar(20), m.StartDate,120)
                    WHEN 'deadline' THEN CONVERT(varchar(20), m.Deadline,120)
					WHEN 'lowest_available_seats' THEN '00000000' + CAST((CAST(TotalSeat AS INT) - CAST(EnrolledUser AS INT)) AS VARCHAR(10))
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


	--alter proc spDemo
	--as
	--begin
	
	--	select title from mission where MissionId=2;
	--end
	--go