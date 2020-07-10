Create database Hubspot
use Hubspot

create table Deals
(
	Id					int			identity	not null,
	ResponsavelNegocio	varchar(50)				not null,
	ValorNegocio		float					not null,
	DataNegocio			varchar(20)				not null,
	TipoNegocio			varchar(30)				not null
)

insert into Deals values('Pedro bodrin', 1750.50, '20/01/2020', 'Compra')

select * from Deals