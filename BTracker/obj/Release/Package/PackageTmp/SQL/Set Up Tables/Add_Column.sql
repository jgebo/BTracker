
ALTER Table [dbo].[ProjectUsers]
ADD UserName nvarchar(50)

ALTER Table [dbo].[TicketAttachments]
ADD UserName nvarchar(50)

ALTER Table [dbo].[TicketComments]
ADD UserName nvarchar(50)

ALTER Table [dbo].[TicketHistories]
ADD UserName nvarchar(50)

ALTER Table [dbo].[TicketNotifications]
ADD UserName nvarchar(50)