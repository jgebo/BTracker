--ALTER TABLE [dbo].[Tickets]
--WITH CHECK ADD CONSTRAINT FK_Tickets_AspPNetUsers
--FOREIGN KEY (OwnerId) REFERENCES [dbo].[AspNetUsers] ([Id])

--ALTER TABLE [dbo].[Tickets]
--WITH CHECK ADD CONSTRAINT FK_Tickets_AssignedId_AspPNetUsers
--FOREIGN KEY (AssignedId) REFERENCES [dbo].[AspNetUsers] ([Id])

ALTER Table [dbo].[Tickets]
WITH CHECK ADD CONSTRAINT FK_Tickets_Statuses
FOREIGN KEY (StatusId) REFERENCES [dbo].[TicketStatuses] ([Id])

ALTER Table [dbo].[Tickets]
WITH CHECK ADD CONSTRAINT FK_Tickets_Priorities
FOREIGN KEY (PriorityId) REFERENCES [dbo].[TicketPriorities] ([Id])

ALTER Table [dbo].[Tickets]
WITH CHECK ADD CONSTRAINT FK_Tickets_Types
FOREIGN KEY (TypeId) REFERENCES [dbo].[TicketTypes] ([Id])

ALTER Table [dbo].[Tickets]
WITH CHECK ADD CONSTRAINT FK_Tickets_Projects
FOREIGN KEY (ProjectId) REFERENCES [dbo].[Projects] ([Id])

ALTER Table [dbo].[ProjectUsers]
WITH CHECK ADD CONSTRAINT FK_Tickets_ProjectUsers
FOREIGN KEY (ProjectId) REFERENCES [dbo].[Projects] ([Id])


-- set up for Tickets - BTUsers

ALTER Table [dbo].[ProjectUsers]
WITH CHECK ADD CONSTRAINT FK_ProjectUsers_Tickets
FOREIGN KEY (UserName) REFERENCES [dbo].[BTUsers] ([UserName])

ALTER Table [dbo].[TicketAttachments]
WITH CHECK ADD CONSTRAINT FK_TicketAttachments_Tickets
FOREIGN KEY (UserName) REFERENCES [dbo].[BTUsers] ([UserName])


ALTER Table [dbo].[TicketComments]
WITH CHECK ADD CONSTRAINT FK_TicketComments_Tickets
FOREIGN KEY (UserName) REFERENCES [dbo].[BTUsers] ([UserName])


ALTER Table [dbo].[TicketHistories]
WITH CHECK ADD CONSTRAINT FK_TicketHistories_Tickets
FOREIGN KEY (UserName) REFERENCES [dbo].[BTUsers] ([UserName])

ALTER Table [dbo].[TicketNotifications]
WITH CHECK ADD CONSTRAINT FK_TicketNotifications_Users
FOREIGN KEY (UserName) REFERENCES [dbo].[BTUsers] ([UserName])

ALTER Table [dbo].[Tickets]
WITH CHECK ADD CONSTRAINT FK_Tickets_Owner_BTName
FOREIGN KEY (OwnerId)
REFERENCES [dbo].[BTUsers] ([UserName])

ALTER Table [dbo].[Tickets]
WITH CHECK ADD CONSTRAINT FK_Tickets_Assigned_BTName
FOREIGN KEY (AssignedId)
REFERENCES [dbo].[BTUsers] ([UserName])



-- set up for Tickets - ticketId

ALTER Table [dbo].[TicketAttachments]
WITH CHECK ADD CONSTRAINT FK_TicketAttachments_Tickets
FOREIGN KEY (TicketId) 
REFERENCES [dbo].[Tickets] ([Id])

ALTER Table [dbo].[TicketComments]
WITH CHECK ADD CONSTRAINT FK_TicketComments_Tickets
FOREIGN KEY (TicketId) 
REFERENCES [dbo].[Tickets] ([Id])

ALTER Table [dbo].[TicketHistories]
WITH CHECK ADD CONSTRAINT FK_TicketHistories_Tickets
FOREIGN KEY (TicketId) 
REFERENCES [dbo].[Tickets] ([Id])

ALTER Table [dbo].[TicketNotifications]
WITH CHECK ADD CONSTRAINT FK_TicketNotifications_Tickets
FOREIGN KEY (TicketId) 
REFERENCES [dbo].[Tickets] ([Id])




