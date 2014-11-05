using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BTracker.Models;
using PagedList.Mvc;
using PagedList;

namespace BTracker.Controllers
{
    public class TicketsController : Controller
    {
        private BTrackerEntities db = new BTrackerEntities();

        static bool SortDirection;
        private static string SortProperty;
        private static int ProjectId = 0; 

         private static IEnumerable<Ticket> tickets;

        //// GET: Tickets
        //public ActionResult Index()
        //{
        //    var tickets = db.Tickets.Include(t => t.BTUser).Include(t => t.BTUser1).Include(t => t.Project).Include(t => t.TicketPriority).Include(t => t.TicketStatus).Include(t => t.TicketType);
        //    return View(tickets.ToList());
        //}

         // GET: Tickets
         [Authorize(Roles = "Administrator, Developer, Submitter, Demo")]
         public ActionResult Index(int? projectid, string param, string paramtype, int? page)
         {
             ViewBag.pid = ProjectId;
             if (projectid != null)
             {
                 ProjectId = (int)projectid;
             }

             if (User.IsInRole("Administrator"))
             {
                 tickets = db.Tickets.Include(t => t.BTUser)
                                    .Include(t => t.BTUser1)
                                    .Include(t => t.Project)
                                    .Include(t => t.TicketStatus)
                                    .Include(t => t.TicketType)
                                    .Include(t => t.TicketPriority);
             }
             else if (User.IsInRole("Developer"))
             {
                 tickets = db.Tickets.Include(t => t.BTUser)
                                   .Include(t => t.BTUser1)
                                   .Include(t => t.Project)
                                   .Include(t => t.TicketStatus)
                                   .Include(t => t.TicketType)
                                   .Include(t => t.TicketPriority).Where(t => t.AssignedId == User.Identity.Name);
             }
             else
             {
                 tickets = db.Tickets.Include(t => t.BTUser)
                                     .Include(t => t.BTUser1)
                                     .Include(t => t.Project)
                                     .Include(t => t.TicketStatus)
                                     .Include(t => t.TicketType)
                                     .Include(t => t.TicketPriority).Where(t => t.OwnerId == User.Identity.Name);
             }

             if (projectid > 0)
             {
                 ViewBag.pid = ProjectId;
                 ViewBag.Title = "Tickets:" + db.Projects.Find(ProjectId).Name;
                 tickets = tickets.Where(t => t.ProjectId == ProjectId);
             }
             else
             {
                 ViewBag.Title = "Title";
             }

             switch (paramtype)
             {
                 case "Owner":
                     tickets = tickets.Where(t => t.BTUser1.DisplayName == param);
                     break;
                 //case "Last Updated":
                 //    tickets = tickets.Where(t => t.UpdatedOn == param);
                 //    break;
                 case "Assigned":
                     tickets = tickets.Where(t => t.BTUser.DisplayName == param);
                     break;
                 case "Project":
                     tickets = tickets.Where(t => t.Project.Name == param);
                     break;
                 case "Priority":
                     tickets = tickets.Where(t => t.TicketPriority.Name == param);
                     break;
                 case "Status":
                     tickets = tickets.Where(t => t.TicketStatus.Name == param);
                     break;
                 case "Type":
                     tickets = tickets.Where(t => t.TicketType.Name == param);
                     break;
                 default:
                     break;
             }

             switch (SortProperty)
             {
                 case "Title":
                     if (!SortDirection)
                     {
                         tickets = tickets.OrderBy(t => t.Title).ThenBy(t => t.UpdatedOn);
                     }
                     else
                     {
                         tickets = tickets.OrderByDescending(t => t.Title).ThenByDescending(t => t.UpdatedOn);
                     }
                     break;
                 case "Last Updated":
                     if (!SortDirection)
                     {
                         tickets = tickets.OrderBy(t => t.UpdatedOn);
                     }
                     else
                     {
                         tickets = tickets.OrderByDescending(t => t.UpdatedOn);
                     }
                     break;
                 case "Owner":
                     if (!SortDirection)
                     {
                         tickets = tickets.OrderBy(t => t.BTUser1.DisplayName).ThenBy(t => t.UpdatedOn);
                     }
                     else
                     {
                         tickets = tickets.OrderByDescending(t => t.BTUser1.DisplayName).ThenByDescending(t => t.UpdatedOn);
                     }
                     break;
                 case "Assigned":
                     if (!SortDirection)
                     {

                         tickets = tickets.OrderBy(t => t.BTUser.DisplayName).ThenBy(t => t.UpdatedOn);
                     }
                     else
                     {
                         tickets = tickets.OrderByDescending(t => t.BTUser.DisplayName).ThenByDescending(t => t.UpdatedOn);
                     }
                     break;
                 case "Project":
                     if (!SortDirection)
                     {
                         tickets = tickets.OrderBy(t => t.Project.Name).ThenBy(t => t.UpdatedOn);
                     }
                     else
                     {
                         tickets = tickets.OrderByDescending(t => t.Project.Name).ThenByDescending(t => t.UpdatedOn);
                     }
                     break;
                 case "Priority":
                     if (!SortDirection)
                     {
                         tickets = tickets.OrderBy(t => t.TicketPriority.Name).ThenBy(t => t.UpdatedOn);
                     }
                     else
                     {
                         tickets = tickets.OrderByDescending(t => t.TicketPriority.Name).ThenByDescending(t => t.UpdatedOn);
                     }
                     break;
                 case "Status":
                     if (!SortDirection)
                     {
                         tickets = tickets.OrderBy(t => t.TicketStatus.Name).ThenBy(t => t.UpdatedOn);
                     }
                     else
                     {
                         tickets = tickets.OrderByDescending(t => t.TicketStatus.Name).ThenByDescending(t => t.UpdatedOn);
                     }
                     break;
                 case "Type":
                     if (!SortDirection)
                     {
                         tickets = tickets.OrderBy(t => t.TicketType.Name).ThenBy(t => t.UpdatedOn);
                     }
                     else
                     {
                         tickets = tickets.OrderByDescending(t => t.TicketType.Name).ThenByDescending(t => t.UpdatedOn);
                     }
                     break;
                 default:
                     {
                         tickets = tickets.OrderBy(t => t.Title).ThenBy(t => t.UpdatedOn);
                     }
                     break;
             }     
             int pagesize = 10;
             int pagenumber = page ?? 1;

             return View(tickets.ToPagedList(pagenumber, pagesize));
         }


        // GET: Tickets/Details/5
         [Authorize(Roles = "Administrator, Developer, Demo")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // GET: Tickets/Create
        [Authorize(Roles = "Administrator, Developer, Submitter, Demo")]
        public ActionResult Create()
        {
            ViewBag.AssignedId = new SelectList(db.BTUsers, "UserName", "FirstName");
            ViewBag.OwnerId = new SelectList(db.BTUsers, "UserName", "FirstName");
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name");
            ViewBag.PriorityId = new SelectList(db.TicketPriorities, "Id", "Name");
            ViewBag.StatusId = new SelectList(db.TicketStatuses, "Id", "Name");
            ViewBag.TypeId = new SelectList(db.TicketTypes, "Id", "Name");
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Administrator, Developer, Submitter")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Description,OwnerId,AssignedId,StatusId,PriorityId,TypeId,ProjectId,CreatedOn,UpdatedOn")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                ticket.CreatedOn = DateTimeOffset.Now;
                db.Tickets.Add(ticket);
               
                
                db.TicketHistories.Add(new TicketHistory
                {
                    Property = "New Ticket",
                    Changed = DateTimeOffset.Now,
                    UserName = User.Identity.Name,
                    TicketId = ticket.Id,
                    OldValue = " ",
                    NewValue = ticket.Description
                });

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AssignedId = new SelectList(db.BTUsers, "UserName", "FirstName", ticket.AssignedId);
            ViewBag.OwnerId = new SelectList(db.BTUsers, "UserName", "FirstName", ticket.OwnerId);
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", ticket.ProjectId);
            ViewBag.PriorityId = new SelectList(db.TicketPriorities, "Id", "Name", ticket.PriorityId);
            ViewBag.StatusId = new SelectList(db.TicketStatuses, "Id", "Name", ticket.StatusId);
            ViewBag.TypeId = new SelectList(db.TicketTypes, "Id", "Name", ticket.TypeId);
            return View(ticket);
        }

        // GET: Tickets/Edit/5
         [Authorize(Roles = "Administrator, Developer, Demo")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            ViewBag.AssignedId = new SelectList(db.BTUsers, "UserName", "FirstName", ticket.AssignedId);
            ViewBag.OwnerId = new SelectList(db.BTUsers, "UserName", "FirstName", ticket.OwnerId);
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", ticket.ProjectId);
            ViewBag.PriorityId = new SelectList(db.TicketPriorities, "Id", "Name", ticket.PriorityId);
            ViewBag.StatusId = new SelectList(db.TicketStatuses, "Id", "Name", ticket.StatusId);
            ViewBag.TypeId = new SelectList(db.TicketTypes, "Id", "Name", ticket.TypeId);

            TempData["OldTicket"] = ticket; 
            
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Administrator, Developer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Description,OwnerId,AssignedId,StatusId,PriorityId,TypeId,ProjectId,CreatedOn,UpdatedOn")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                var oldTicket = (Ticket)TempData["OldTicket"];

                if (oldTicket.Description != ticket.Description)
                {
                    db.TicketHistories.Add(new TicketHistory
                    {
                        Property = "Description",
                        Changed = DateTimeOffset.Now,
                        UserName = User.Identity.Name,
                        TicketId = ticket.Id,
                        OldValue = oldTicket.Description,
                        NewValue = ticket.Description
                    });
                }
                if (oldTicket.ProjectId != ticket.ProjectId)
                {
                    db.TicketHistories.Add(new TicketHistory
                    {
                        Property = "Project",
                        Changed = DateTimeOffset.Now,
                        UserName = User.Identity.Name,
                        TicketId = ticket.Id,
                        OldValue = oldTicket.Project.Name,
                        NewValue = db.Projects.Find(ticket.ProjectId).Name
                    });
                }
                if (oldTicket.StatusId != ticket.StatusId)
                {

                    db.TicketHistories.Add(new TicketHistory
                    {
                        Property = "Status",
                        Changed = DateTimeOffset.Now,
                        UserName = User.Identity.Name,
                        TicketId = ticket.Id,
                        OldValue = oldTicket.TicketStatus.Name,
                        NewValue = db.TicketStatuses.Find(ticket.StatusId).Name
                    });
                }
                if (oldTicket.TypeId != ticket.TypeId)
                {
                    db.TicketHistories.Add(new TicketHistory
                    {
                        Property = "Type",
                        Changed = DateTimeOffset.Now,
                        UserName = User.Identity.Name,
                        TicketId = ticket.Id,
                        OldValue = oldTicket.TicketType.Name,
                        NewValue = db.TicketTypes.Find(ticket.TypeId).Name
                    });
                }
                if (oldTicket.PriorityId != ticket.PriorityId)
                {
                    db.TicketHistories.Add(new TicketHistory
                    {
                        Property = "Priority",
                        Changed = DateTimeOffset.Now,
                        UserName = User.Identity.Name,
                        TicketId = ticket.Id,
                        OldValue = oldTicket.TicketPriority.Name,
                        NewValue = db.TicketPriorities.Find(ticket.PriorityId).Name
                    });
                }
                if (oldTicket.AssignedId != ticket.AssignedId)
                {
                    db.TicketHistories.Add(new TicketHistory
                    {
                        Property = "Assigned To",
                        Changed = DateTimeOffset.Now,
                        UserName = User.Identity.Name,
                        TicketId = ticket.Id,
                        OldValue = oldTicket.AssignedId,
                        NewValue = ticket.AssignedId
                    });
                }

                ticket.UpdatedOn = DateTimeOffset.Now;
                db.Entry(ticket).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AssignedId = new SelectList(db.BTUsers, "UserName", "FirstName", ticket.AssignedId);
            ViewBag.OwnerId = new SelectList(db.BTUsers, "UserName", "FirstName", ticket.OwnerId);
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", ticket.ProjectId);
            ViewBag.PriorityId = new SelectList(db.TicketPriorities, "Id", "Name", ticket.PriorityId);
            ViewBag.StatusId = new SelectList(db.TicketStatuses, "Id", "Name", ticket.StatusId);
            ViewBag.TypeId = new SelectList(db.TicketTypes, "Id", "Name", ticket.TypeId);
            return View(ticket);
        }

        
        // Sort: 
        [Authorize(Roles = "Administrator, Developer, Submitter, Demo")]
        public ActionResult Sort(string property, IEnumerable<Ticket> model)
        {
            tickets = model;

            if (SortProperty == property)
            {
                // toggle direction
                SortDirection = !SortDirection;
            }
            else
            {
                // initial direction (ascending)
                SortDirection = false;
            }

            SortProperty = property;
            return RedirectToAction("Index");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
