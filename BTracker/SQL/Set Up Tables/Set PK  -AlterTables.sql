ALTER TABLE [dbo].[Tickets]
ADD CONSTRAINT PK_Tickets
PRIMARY KEY CLUSTERED ([Id])

ALTER Table [dbo].[TicketStatuses]
ADD CONSTRAINT PK_TicketStatuses
PRIMARY KEY CLUSTERED ([Id])

ALTER Table [dbo].[TicketPriorities]
ADD CONSTRAINT PK_TicketPriorities
PRIMARY KEY CLUSTERED ([Id])	

ALTER Table [dbo].[TicketTypes]
ADD CONSTRAINT PK_TicketTypes
PRIMARY KEY CLUSTERED ([Id])


ALTER Table [dbo].[TicketHistories]
ADD CONSTRAINT PK_TicketHistories
PRIMARY KEY CLUSTERED ([Id])

ALTER Table [dbo].[TicketNotifications]
ADD CONSTRAINT PK_TicketNotifications
PRIMARY KEY CLUSTERED ([Id])

ALTER Table [dbo].[TicketAttachments]
ADD CONSTRAINT PK_TicketAttachments
PRIMARY KEY CLUSTERED ([Id])

ALTER Table [dbo].[TicketComments]
ADD CONSTRAINT PK_TicketComments
PRIMARY KEY CLUSTERED ([Id])

ALTER Table [dbo].[BTUsers]
ADD CONSTRAINT PK_BTUsers
PRIMARY KEY CLUSTERED ([UserName])

ALTER Table [dbo].[Projects]
ADD CONSTRAINT PK_Projects
PRIMARY KEY CLUSTERED ([Id])

ALTER Table [dbo].[ProjectUsers]
ADD CONSTRAINT PK_ProjectUsers
PRIMARY KEY CLUSTERED ([Id])
