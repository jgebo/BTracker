--Drop Table [dbo].[Tickets]

CREATE Table [dbo].[Tickets]
(
	Id int Not NULL IDENTITY,
	Title nvarchar(MAX) Not Null,
	[Description] Nvarchar(Max) Not Null,
	OwnerId nvarchar(128) Not Null,
	AssignedId nvarchar(128) Not Null,
	StatusId int Not Null,
	PriorityId int Not Null,
	TypeId int Not Null,
	ProjectId int Not Null,
	CreatedOn datetimeoffset(2) Not Null,
	UpdatedOn datetimeoffset(2) Null
)

CREATE Table [dbo].[TicketStatuses]
(
	Id int Not NULL IDENTITY,
	Name nvarchar(50) Not Null
)	

CREATE Table [dbo].[TicketPriorities]
(
	Id int Not NULL IDENTITY,
	Name nvarchar(50) Not Null
)	

CREATE Table [dbo].[TicketTypes]
(
	Id int Not NULL IDENTITY,
	Name nvarchar(50) Not Null
)	

CREATE Table [dbo].[TicketAttachments]
(
	Id int Not Null IDENTITY,
	TicketId int Not Null,
	[Description] nvarchar(MAX) Not Null,
	UserName nvarchar(50 ) Not Null,
	FilePath nvarchar(MAX) Not Null,
	FileUrl nvarchar(MAX) Not Null,
	Created datetimeoffset(2) Not Null
)

CREATE Table [dbo].[TicketComments]
(
	Id int Not Null IDENTITY,
	TicketId int Not Null,
	Comment nvarchar(MAX) Not Null,
	UserName nvarchar(50 ) Not Null,
	Created datetimeoffset(2) Not Null
)

CREATE Table [dbo].[TicketHistories]
(
	Id int Not Null IDENTITY,
	TicketId int Not Null,
	Property nvarchar(50) Not Null,
	UserName nvarchar(50) Not Null,
	OldValue nvarchar(MAX) Not Null,
	NewValue nvarchar(MAX) Not Null,
	Created datetimeoffset(2) Not Null
)

--drop table [dbo].[TicketNotifications]
CREATE Table [dbo].[TicketNotifications]
(
	Id int Not NULL IDENTITY,
	TicketId int Not Null,
	UserName nvarchar(50) Not Null,
	SentToId  nvarchar(50),
	SentFromId nvarchar(50),
	SentO datetimeoffset(2),
	HasBeenSent nvarchar(1)
)	

CREATE Table [dbo].[BTUsers]
(
	UserName nvarchar(50) Not Null,
	FirstName nvarchar(50) Not Null,
	LastName nvarchar(50) Not Null,
	DisplayName nvarchar(100) Not Null,
	AspNetUserId nvarchar(128) Not Null
)	

CREATE Table [dbo].[Projects]
(
	Id int Not NULL IDENTITY,
	Name nvarchar(50) Not Null
)	

CREATE Table [dbo].[ProjectUsers]
(
	Id int Not NULL IDENTITY,
	ProjectId int Not Null,
	UserName nvarchar(50) Not Null
)	




