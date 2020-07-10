Create database Mautic
use Mautic

create table Lead
(
	Id				int			identity	not null,
	ID_Mautic		int						not null,
	SegmentId		int						not null,
	Name			varchar(50)				not null,
	Email			varchar(30)				not null,
	IpAddress		varchar(100)			not null
)

select * from Lead

insert into Lead values(2, 7, 'Pedro Boldrin', 'pedro.boldrin@gmail.com', '192.168.0.100')