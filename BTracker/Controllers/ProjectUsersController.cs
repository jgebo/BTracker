using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BTracker.Models;

namespace BTracker.Controllers
{
    public class ProjectUsersController : Controller
    {
        private BTrackerEntities db = new BTrackerEntities();

        // GET: ProjectUsers
        public ActionResult Index()
        {
            var projectUsers = db.ProjectUsers.Include(p => p.BTUser).Include(p => p.Project);
            return View(projectUsers.ToList());
        }

        // GET: ProjectUsers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectUser projectUser = db.ProjectUsers.Find(id);
            if (projectUser == null)
            {
                return HttpNotFound();
            }
            return View(projectUser);
        }

        // GET: ProjectUsers/Create
        public ActionResult Create()
        {
            ViewBag.UserName = new SelectList(db.BTUsers, "UserName", "FirstName");
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name");
            return View();
        }

        // POST: ProjectUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ProjectId,UserName")] ProjectUser projectUser)
        {
            if (ModelState.IsValid)
            {
                db.ProjectUsers.Add(projectUser);

                db.TicketHistories.Add(new TicketHistory
                {
                    Property = "New Project User",
                    Changed = DateTimeOffset.Now,
                    UserName = User.Identity.Name,
                    TicketId = projectUser.Id,
                    OldValue = " ",
                    NewValue = projectUser.UserName
                });

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserName = new SelectList(db.BTUsers, "UserName", "FirstName", projectUser.UserName);
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", projectUser.ProjectId);
            return View(projectUser);
        }

        // GET: ProjectUsers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectUser projectUser = db.ProjectUsers.Find(id);
            if (projectUser == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserName = new SelectList(db.BTUsers, "UserName", "FirstName", projectUser.UserName);
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", projectUser.ProjectId);
            return View(projectUser);
        }

        // POST: ProjectUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProjectId,UserName")] ProjectUser projectUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(projectUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserName = new SelectList(db.BTUsers, "UserName", "FirstName", projectUser.UserName);
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", projectUser.ProjectId);
            return View(projectUser);
        }

        // GET: ProjectUsers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectUser projectUser = db.ProjectUsers.Find(id);
            if (projectUser == null)
            {
                return HttpNotFound();
            }
            return View(projectUser);
        }

        // POST: ProjectUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProjectUser projectUser = db.ProjectUsers.Find(id);
            db.ProjectUsers.Remove(projectUser);
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
