
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

exec spGetAllMissions

exec spGetAllMissions @PageIndex=1, @Skills='1', @SortColumn='lowest_available_seats', @SortOrder='DESC'
go

select mission.mission_id from mission
inner join mission_skill on mission.mission_id = mission_skill.mission_id
where skill_id = 1

exec spGetAllMissions @Cities = '11,4,5', @Themes='2', @SearchKeyword=' proj'
go

sp_help time_mission


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

