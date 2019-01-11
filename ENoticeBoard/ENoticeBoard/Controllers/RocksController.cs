using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace ENoticeBoard.Controllers
{
    public class RocksController : Controller
    {
        private readonly MyDatabaseEntities _db = new MyDatabaseEntities();

        // GET: Rocks
        //public ActionResult Index()
        //{
        //    //ViewBag.objectSpend = _db.Objects.Sum(x => x.Cost);
        //    //return View(_db.Rocks.OrderByDescending(s=>s.Priority).ToList());
        //}
      

        // POST: Rocks/Create
        // To protect from over-posting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public JsonResult Create(String subject,int? priority, string dateDue)
        {
            DateTime dueDateInDateTime=DateTime.Parse(dateDue);
        
            if (ModelState.IsValid)
            {
                if (priority != null)
                {
                    var rock = new Rock
                    {
                        Subject = subject,
                        Priority = priority.Value,
                        DateCreated = DateTime.Today,
                        DateDue=  dueDateInDateTime,
                        Done = false
                    };
                    _db.Rocks.Add(rock);
                }

                _db.SaveChanges();
              
            }

            return Json("Sucess", JsonRequestBehavior.AllowGet);
        }

        // GET: Rocks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rock rock = _db.Rocks.Find(id);
            if (rock == null)
            {
                return HttpNotFound();
            }
            return PartialView(rock);
        }

       

        // POST: Rocks/Edit/5
        // To protect from over-posting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public JsonResult Edit(Rock rock)
        {
            
            if (ModelState.IsValid)
            {
                 
                _db.Entry(rock).State = EntityState.Modified;
                _db.SaveChanges();
            }

            return Json("success",JsonRequestBehavior.AllowGet);
        }

        
        // POST: Rocks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var rock = _db.Rocks.FirstOrDefault(x => x.RockId == id);
            if (rock != null) _db.Rocks.Remove(rock);
            _db.SaveChanges();
            return RedirectToAction("Index","Home");
        }


        // GET: Rocks/Done/5
        public ActionResult Done(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rock rock = _db.Rocks.Find(id);
            if (rock == null)
            {
                return HttpNotFound();
            }
            return PartialView(rock);
        }

        // POST: Rocks/Done/5
        [HttpPost, ActionName("Done")]
        [ValidateAntiForgeryToken]
        public ActionResult DoneConfirmed(int id)
        {
            var rock = _db.Rocks.FirstOrDefault(x => x.RockId == id);
            if (rock != null)
            {
                rock.Done = true;
            }

            _db.SaveChanges();
            return RedirectToAction("Index","Home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }


        public JsonResult GetRocks()
        {

            var rocks = (_db.Rocks.OrderByDescending(s => s.Priority).ToList());
            return new JsonResult{Data = rocks, JsonRequestBehavior = JsonRequestBehavior.AllowGet};
            
        }
    }
}

