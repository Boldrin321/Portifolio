Create database API
use API

create table Lead
(
	Id				int			identity	not null,
	Email			varchar(30)				not null,
	Name			varchar(50)				not null,
	Telephone		varchar(50)				not null,
	CampaignName	varchar(50)				not null,
	Time			datetime				not null
)

select * from Lead

insert into Lead values('pedro.boldrin@gmail.com', 'Pedro Boldrin', '123456789', 'lead 1', '2020-03-12 20:15:00')