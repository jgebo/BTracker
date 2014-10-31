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
        public ActionResult Index()
        {
            return View(db.BTUsers.ToList());
        }

        // GET: BTUsers/Details/5
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
        public ActionResult Create()
        {
            return View();
        }

        // POST: BTUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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
