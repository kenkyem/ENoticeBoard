using ENoticeBoard.ViewModels;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;


namespace ENoticeBoard.Controllers
{
    public class DowntimesController : Controller
    {
        private readonly MyDatabaseEntities _db = new MyDatabaseEntities();
        // GET: Downtimes
        public ActionResult Index()
        {
            return View(_db.Downtimes.ToList());
        }
        [HttpPost]
        public PartialViewResult Summary(string period, string year)
        {
            if(period==null && year==null)
            {
                period= BreakagesController.CurrentPeriod();
                year = BreakagesController.CurrentYear();
            }

            ViewBag.DTPlanned = _db.Targets.Single(t => t.Subject == "Downtime_Planned").TargetNum;
            ViewBag.DTUnplanned= _db.Targets.Single(t => t.Subject == "Downtime_Unplanned").TargetNum;
            var model = new DowntimeSummaryViewModel()
            {
                DowntimeWFPs = _db.Vw_DowntimesWithinFinancialPeriod
                    .Where(x=>x.FinancialPeriod==period && x.FinancialYear==year && x.isDeleted==false)
                    .ToList(),
                GroupByModels = _db.Vw_DowntimesWithinFinancialPeriod
                    .Where(x=>x.FinancialPeriod==period && x.FinancialYear==year && x.isDeleted==false)
                    .GroupBy(x => new {x.FinancialPeriod, x.FinancialYear})
                    .Select(y => new GroupByModel()
                    {
                        Period = y.Key.FinancialPeriod, Year = y.Key.FinancialYear,
                        Sum = y.Sum(z => z.Duration)
                    })
                    .ToList(),
                Periodddl = _db.Vw_DowntimesWithinFinancialPeriod.Select(x=>new DropDownBoxList()
                {
                    text=x.FinancialPeriod,
                    value=x.FinancialPeriod
                }).Distinct().ToList(),
                Yearddl = _db.Vw_DowntimesWithinFinancialPeriod.Select(x=>new DropDownBoxList()
                {
                    text=x.FinancialYear,
                    value=x.FinancialYear
                }).Distinct().ToList(),
                SelectedPeriod = period,
                SelectedYear = year
                

            };
            
            return PartialView(model);
        }

        // GET: Downtimes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Downtime downtime = _db.Downtimes.Find(id);
            if (downtime == null)
            {
                return HttpNotFound();
            }

            var model = new DowntimeFormViewModel()
            {
                Id = downtime.DownTimeId,
                Subject = downtime.Subject,
                Date = downtime.Date,
                Description = downtime.Description,
                Duration = downtime.Duration,
                Type = downtime.Type,
                Types = _db.Downtimetypes.ToList(),
                Status = downtime.Status
            };
            return PartialView(model);

        }

        // GET: Downtimes/Create
        public ActionResult Create()
        {
            var model = new DowntimeFormViewModel
            {
                Date = DateTime.Today,
                Types = _db.Downtimetypes.ToList()
            };
            return PartialView(model);
        }

        // POST: Downtimes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public JsonResult Create(DowntimeFormViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                viewModel.Types = _db.Downtimetypes.ToList();
            }

            var downtime = new Downtime
            {
                DownTimeId = viewModel.Id,
                Subject = viewModel.Subject,
                Date = viewModel.Date,
                Description = viewModel.Description,
                Duration = viewModel.Duration,
                Type = viewModel.Type,
                isDeleted = false,
                Status = viewModel.Status
                
            };
            
            _db.Downtimes.Add(downtime);
            _db.SaveChanges();

            return Json("Success", JsonRequestBehavior.AllowGet);
        }

        // GET: Downtimes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Downtime downtime = _db.Downtimes.Find(id);
            if (downtime == null)
            {
                return HttpNotFound();
            }

            var model = new DowntimeFormViewModel()
            {
                Id = downtime.DownTimeId,
                Subject = downtime.Subject,
                Date = downtime.Date,
                Description = downtime.Description,
                Duration = downtime.Duration,
                Type = downtime.Type,
                Types = _db.Downtimetypes.ToList(),
                Status = downtime.Status
            };
            return PartialView(model);
        }

        // POST: Downtimes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit(DowntimeFormViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Downtime downtime = _db.Downtimes.Find(viewModel.Id);
                if (downtime == null)
                {
                    return HttpNotFound();
                }

                downtime.Subject = viewModel.Subject;
                downtime.Date = viewModel.Date;
                downtime.Description = viewModel.Description;
                downtime.Duration = viewModel.Duration;
                downtime.Type = viewModel.Type;
                downtime.Status = viewModel.Status;

                _db.SaveChanges();

            }
            return Json("success", JsonRequestBehavior.AllowGet);
        }

        // GET: Downtimes/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Downtime downtime = db.Downtimes.Find(id);
        //    if (downtime == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(downtime);
        //}

        //// POST: Downtimes/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Downtime downtime = db.Downtimes.Find(id);
        //    db.Downtimes.Remove(downtime);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Downtime downtime = _db.Downtimes.Find(id);
            if (downtime == null)
            {
                return HttpNotFound();
            }
            
            var model = new DowntimeFormViewModel()
            {
                Id = downtime.DownTimeId,
                Subject = downtime.Subject,
                Date = downtime.Date,
                Description = downtime.Description,
                Duration = downtime.Duration,
                Type = downtime.Type,
                Types = _db.Downtimetypes.ToList(),
                Status = downtime.Status
            };
            return PartialView(model);

        }

        [HttpPost]
        public JsonResult DeleteConfirmed(int? id)
        {
            Downtime downtime = _db.Downtimes.Find(id);
            if (downtime != null)
            {
                downtime.isDeleted = true;
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
