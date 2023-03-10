
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

exec spGetAllMissions @Countries='5,1'

exec spGetAllMissions @PageIndex=1, @Skills='1', @SortColumn='lowest_available_seats', @SortOrder='DESC'
go

select mission.mission_id from mission
inner join mission_skill on mission.mission_id = mission_skill.mission_id
where skill_id = 1

exec spGetAllMissions @Cities = '11,4,5', @Themes='2', @SearchKeyword=' proj'
go

exec spGetAllMissions @SortColumn='lowest_available_seats', @SortOrder='DESC'

sp_help time_mission 


insert into mission_skill (mission_id, skill_id)
values (2,1),(2,2), (3,1), (3,2), (3,3), (4,4), (4,5), (5,1), (5,4), (5,3), (6,1), (6,5)
go

