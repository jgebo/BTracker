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
    public class BTUsersController : Controller
    {
        private BTrackerEntities db = new BTrackerEntities();

        // GET: BTUsers
         [Authorize(Roles = "Administrator, Demo")]
        public ActionResult Index()
        {
             if (User.IsInRole("Demo"))
             {
                 var demo = "demo@demo.com";
                 return View(db.BTUsers.Where(u => u.UserName == demo).ToList());
             }

            return View(db.BTUsers.ToList());
        }

        // GET: BTUsers/Details/5
         [Authorize(Roles = "Administrator, Demo")]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BTUser bTUser = db.BTUsers.Find(id);
            if (bTUser == null)
            {
                return HttpNotFound();
            }
            return View(bTUser);
        }

        // GET: BTUsers/Create
         [Authorize(Roles = "None")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: BTUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "None")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserName,FirstName,LastName,DisplayName,AspNetUserId")] BTUser bTUser)
        {
            if (ModelState.IsValid)
            {
                db.BTUsers.Add(bTUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bTUser);
        }

        // GET: BTUsers/Edit/5
         [Authorize(Roles = "None")]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BTUser bTUser = db.BTUsers.Find(id);
            if (bTUser == null)
            {
                return HttpNotFound();
            }
            return View(bTUser);
        }

        // POST: BTUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "None")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserName,FirstName,LastName,DisplayName,AspNetUserId")] BTUser bTUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bTUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bTUser);
        }

        // GET: BTUsers/Delete/5
         [Authorize(Roles = "None")]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BTUser bTUser = db.BTUsers.Find(id);
            if (bTUser == null)
            {
                return HttpNotFound();
            }
            return View(bTUser);
        }

        // POST: BTUsers/Delete/5
         [Authorize(Roles = "None")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            BTUser bTUser = db.BTUsers.Find(id);
            db.BTUsers.Remove(bTUser);
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
