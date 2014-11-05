//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BTracker.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class BTUser
    {
        public BTUser()
        {
            this.ProjectUsers = new HashSet<ProjectUser>();
            this.TicketAttachments = new HashSet<TicketAttachment>();
            this.TicketComments = new HashSet<TicketComment>();
            this.TicketHistories = new HashSet<TicketHistory>();
            this.TicketNotifications = new HashSet<TicketNotification>();
            this.Tickets = new HashSet<Ticket>();
            this.Tickets1 = new HashSet<Ticket>();
        }
    
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }
        public string AspNetUserId { get; set; }
        public string Email { get; set; }
    
        public virtual ICollection<ProjectUser> ProjectUsers { get; set; }
        public virtual ICollection<TicketAttachment> TicketAttachments { get; set; }
        public virtual ICollection<TicketComment> TicketComments { get; set; }
        public virtual ICollection<TicketHistory> TicketHistories { get; set; }
        public virtual ICollection<TicketNotification> TicketNotifications { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
        public virtual ICollection<Ticket> Tickets1 { get; set; }

        public bool IsOnProject(int id)
        {
            BTrackerEntities db = new BTrackerEntities();

            foreach (var pu in db.ProjectUsers)
            {
                if (pu.ProjectId == id && pu.UserName == this.UserName)
                {
                    return true;
                }
            }
            return false;
        }

        public void AddUserToProjects(int id)
        {
            BTrackerEntities db = new BTrackerEntities();

            var name = this.UserName;

            if (!this.IsOnProject(id) && (name == this.UserName))
            {
                db.ProjectUsers.Add(new ProjectUser { ProjectId = id, UserName = this.UserName });
                db.SaveChanges();
            }
        }
        public void RemoveUserFromProject(int id)
        {
            BTrackerEntities db = new BTrackerEntities();

            if (this.IsOnProject(id))
            {
                foreach (var pu in db.ProjectUsers)
                {
                    if (pu.ProjectId == id && pu.UserName == this.UserName)
                    {
                        db.ProjectUsers.Remove(pu);
                        break;
                    }
                }
                db.SaveChanges();
            }

        }
    }
}
