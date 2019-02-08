using ENoticeBoard.Models;
using ENoticeBoard.ViewModels;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace ENoticeBoard.Controllers
{
    public class ObjectsController : Controller
    {
        private readonly MyDatabaseEntities _db = new MyDatabaseEntities();
        private readonly BaseDataEntities _baseData = new BaseDataEntities();
        // GET: Objects
        [HttpPost]
        public PartialViewResult Summary(string period, string year)
        {
            if(period==null && year==null)
            {
                period= DateConversion.CurrentPeriod();
                year = DateConversion.CurrentYear();
            }

            var publishedDate = DateConversion.PublishedDate();
            ViewBag.budget = _db.Targets.Single(t => t.Subject == "Budget").TargetNum;
            var model = new ObjectSummaryViewModel()
            {
                ObjectWFPs = _db.Vw_ObjectsWithinFinancialPeriod.Where(x=>x.FinancialPeriod==period && x.FinancialYear==year && x.isDeleted==false).ToList(),
                GroupByModels = _db.Vw_ObjectsWithinFinancialPeriod.Where(x=>x.FinancialPeriod==period && x.FinancialYear==year && x.isDeleted==false)
                    .GroupBy(x => new {x.FinancialPeriod, x.FinancialYear}).Select(y => new GroupByModel()
                    {
                        Period = y.Key.FinancialPeriod, Year = y.Key.FinancialYear,
                        Sum = y.Sum(z => z.Cost)
                    }).ToList(),
                Periodddl = _baseData.FinancialCalendars.Where(x=>x.Date >= publishedDate).Select(x=>new DropDownBoxList()
                {
                    text=x.FinancialPeriod,
                    value=x.FinancialPeriod
                }).Distinct().OrderBy(x=>x.value).ToList(),
                Yearddl = _baseData.FinancialCalendars.Select(x=>new DropDownBoxList()
                {
                    text=x.FinancialYear,
                    value=x.FinancialYear
                }).Distinct().OrderBy(x=>x.value).ToList(),
                SelectedPeriod = period,
                SelectedYear = year
                
            };
            return PartialView(model);
        }
        
        // GET: Objects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Object oobject = _db.Objects.Find(id);
            if (oobject == null)
            {
                return HttpNotFound();
            }

            var model = new ObjectFormViewModel()
            {
                Id = oobject.ObjectId,
                Subject = oobject.ObjectName,
                Description = oobject.Description,
                Date = oobject.Date,
                Cost = oobject.Cost
            };
            return PartialView(model);
        }

        // GET: Objects/Create
        [HttpGet]
        public ActionResult Create()
        {
            var model = new ObjectFormViewModel
            {
                Date = DateTime.Today
            };
            return PartialView(model);
        }

        // POST: Objects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public JsonResult Create(ObjectFormViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
            }
            var @object = new Object()
            {
                ObjectId = viewModel.Id,
                ObjectName = viewModel.Subject,
                Date = viewModel.Date,
                Cost = viewModel.Cost,
                Description = viewModel.Description,
                isDeleted = false
            };
            _db.Objects.Add(@object);
            _db.SaveChanges();
            return Json("Success", JsonRequestBehavior.AllowGet);
        }

        // GET: Objects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Object @object = _db.Objects.Find(id);
            if (@object == null)
            {
                return HttpNotFound();
            }

            var model = new ObjectFormViewModel()
            {
                Id = @object.ObjectId,
                Subject = @object.ObjectName,
                Date = @object.Date,
                Cost = @object.Cost,
                Description = @object.Description
            };
            return PartialView(model);
        }

        // POST: Objects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit(ObjectFormViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Object @object = _db.Objects.Find(viewModel.Id);
                if (@object == null)
                {
                    return HttpNotFound();
                }

                @object.ObjectName = viewModel.Subject;
                @object.Date = viewModel.Date;
                @object.Cost = viewModel.Cost;
                @object.Description = viewModel.Description;

                _db.SaveChanges();

            }

            return Json("success", JsonRequestBehavior.AllowGet);
        }

        // GET: Objects/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Object @object = _db.Objects.Find(id);
        //    if (@object == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(@object);
        //}

        // POST: Objects/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Object @object = _db.Objects.Find(id);
        //    _db.Objects.Remove(@object);
        //    _db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Object @object = _db.Objects.Find(id);
            if (@object == null)
            {
                return HttpNotFound();
            }
            var model = new ObjectFormViewModel()
            {
                Id = @object.ObjectId,
                Subject = @object.ObjectName,
                Description = @object.Description,
                Date = @object.Date,
                Cost = @object.Cost
            };
            return PartialView(model);
        }

        [HttpPost]
        public JsonResult DeleteConfirmed(int? id)
        {
            Object @object = _db.Objects.Find(id);
            if (@object != null)
            {
                @object.isDeleted = true;
            }

            _db.SaveChanges();
            return Json("success", JsonRequestBehavior.AllowGet);
            
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
