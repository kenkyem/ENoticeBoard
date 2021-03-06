﻿using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace ENoticeBoard.Controllers
{
    public class TargetsController : Controller
    {
        private readonly MyDatabaseEntities _db = new MyDatabaseEntities();

        // GET: Targets
        public ActionResult Index()
        {
            return View(_db.Targets.AsNoTracking().ToList());
        }

        // GET: Targets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Target target = _db.Targets.Find(id);
            if (target == null)
            {
                return HttpNotFound();
            }
            return View(target);
        }

        // GET: Targets/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Targets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TargetId,Subject,TargetNum")] Target target)
        {
            if (ModelState.IsValid)
            {
                _db.Targets.Add(target);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(target);
        }

        // GET: Targets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Target target = _db.Targets.Find(id);
            if (target == null)
            {
                return HttpNotFound();
            }
            return View(target);
        }

        // POST: Targets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TargetId,Subject,TargetNum")] Target target)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(target).State = EntityState.Modified;
                _db.SaveChanges();
                return PartialView("_Success");
            }
            return PartialView("Edit",target);
        }

        // GET: Targets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Target target = _db.Targets.Find(id);
            if (target == null)
            {
                return HttpNotFound();
            }
            return View(target);
        }

        // POST: Targets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Target target = _db.Targets.Find(id);
            if (target != null) _db.Targets.Remove(target);
            _db.SaveChanges();
            return RedirectToAction("Index");
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
