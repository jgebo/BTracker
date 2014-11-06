using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BTracker.Models;
using System.IO;

namespace BTracker.Controllers
{
    public class TicketAttachmentsController : Controller
    {
        private BTrackerEntities db = new BTrackerEntities();

        // GET: TicketAttachments
        [Authorize(Roles = "Administrator, Developer, Submitter, Demo")]
        public ActionResult Index()
        {
            var ticketAttachments = db.TicketAttachments.Include(t => t.BTUser).Include(t => t.Ticket);
            return View(ticketAttachments.ToList());
        }

        // GET: TicketAttachments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketAttachment ticketAttachment = db.TicketAttachments.Find(id);
            if (ticketAttachment == null)
            {
                return HttpNotFound();
            }
            return View(ticketAttachment);
        }

        // GET: TicketAttachments/Create
        [Authorize(Roles = "Administrator, Developer, Submitter, Demo")]
        public ActionResult Create(int id)
        {
            //ViewBag.UserName = new SelectList(db.BTUsers, "UserName", "FirstName");
            //ViewBag.TicketId = new SelectList(db.Tickets, "Id", "Title");
            //return View();
            var ticketAttachment = new TicketAttachment();
            ticketAttachment.TicketId = id;
            ticketAttachment.Ticket = db.Tickets.Find(id);
            ticketAttachment.Created = DateTimeOffset.Now;
            ticketAttachment.BTUser = db.BTUsers.Find(User.Identity.Name);
            ticketAttachment.UserName = User.Identity.Name;
            return View(ticketAttachment);

        }

        // POST: TicketAttachments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Administrator, Developer, Submitter")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TicketId,FilePath,Description,Created,UserName,FileUrl")] TicketAttachment ticketAttachment, HttpPostedFileBase fileUpload)
        {
            if (ModelState.IsValid)
            {
                var fileName = Path.GetFileName(fileUpload.FileName);
                var path = Server.MapPath("~/App_Data/Attachments/" + ticketAttachment.TicketId + '/');

                ticketAttachment.FilePath = Path.Combine(path, fileName);

                Directory.CreateDirectory(path);

                fileUpload.SaveAs(ticketAttachment.FilePath);

                ticketAttachment.FileUrl = "~/App_Data/Attachments/" + fileName;

                ticketAttachment.Created = DateTimeOffset.Now;
                db.TicketAttachments.Add(ticketAttachment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserName = new SelectList(db.BTUsers, "UserName", "FirstName", ticketAttachment.UserName);
            ViewBag.TicketId = new SelectList(db.Tickets, "Id", "Title", ticketAttachment.TicketId);
            return View(ticketAttachment);
        }

        // GET: TicketAttachments/Edit/5
        [Authorize(Roles = "Administrator, Developer, Submitter, Demo")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketAttachment ticketAttachment = db.TicketAttachments.Find(id);
            if (ticketAttachment == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserName = new SelectList(db.BTUsers, "UserName", "FirstName", ticketAttachment.UserName);
            ViewBag.TicketId = new SelectList(db.Tickets, "Id", "Title", ticketAttachment.TicketId);
            return View(ticketAttachment);
        }

        // POST: TicketAttachments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Administrator, Developer, Submitter")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TicketId,Description,UserName,FilePath,FileUrl,Created")] TicketAttachment ticketAttachment)
        {
            if (ModelState.IsValid)
            {
                ticketAttachment.Created = DateTimeOffset.Now;
                db.Entry(ticketAttachment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserName = new SelectList(db.BTUsers, "UserName", "FirstName", ticketAttachment.UserName);
            ViewBag.TicketId = new SelectList(db.Tickets, "Id", "Title", ticketAttachment.TicketId);
            return View(ticketAttachment);
        }

        // GET: TicketAttachments/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketAttachment ticketAttachment = db.TicketAttachments.Find(id);
            if (ticketAttachment == null)
            {
                return HttpNotFound();
            }
            return View(ticketAttachment);
        }

        // POST: TicketAttachments/Delete/5
         [Authorize(Roles = "Administrator")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TicketAttachment ticketAttachment = db.TicketAttachments.Find(id);
            db.TicketAttachments.Remove(ticketAttachment);
            db.SaveChanges();
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
