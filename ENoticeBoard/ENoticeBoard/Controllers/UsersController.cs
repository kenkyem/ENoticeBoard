﻿using System.Data.Entity;
using System.Net;
using System.Web.Mvc;

namespace ENoticeBoard.Controllers
{
    public class UsersController : Controller
    {
        private readonly MyDatabaseEntities _db = new MyDatabaseEntities();

        // GET: Users
        //public ActionResult Index()
        //{
        //    //return View(db.Users.ToList());
        //}

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = _db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return PartialView(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return PartialView();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "UserId,Email,Name,Color,Role")] User user)
        {
            if (ModelState.IsValid)
            {
                if (user == null)
                {
                    HttpNotFound();
                }

                if (user != null) _db.Users.Add(user);
                _db.SaveChanges();
                return PartialView("_UserCreateSuccess");
            }

            return PartialView("Create", user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = _db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return PartialView(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,Email,Name,Color,Role")] User user)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(user).State = EntityState.Modified;
                _db.SaveChanges();
                return PartialView("_UserCreateSuccess");
            }
            return PartialView("Edit",user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = _db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return PartialView(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = _db.Users.Find(id);
            if (user != null) _db.Users.Remove(user);
            _db.SaveChanges();
            return RedirectToAction("Manage","Home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
