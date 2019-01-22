using ENoticeBoard.ViewModels;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace ENoticeBoard.Controllers
{
    public class BreakagesController : Controller
    {
        private readonly MyDatabaseEntities _db = new MyDatabaseEntities();
        private static readonly BaseDataEntities _basedata = new BaseDataEntities();
        // GET: Breakages
        public ActionResult Index()
        {
            return View(_db.Breakages.ToList());
        }
        [HttpPost]
        public PartialViewResult Summary(string period,string year)
        {
            if(period==null && year==null)
            {
                period= CurrentPeriod();
                year = CurrentYear();
            }
            var b= ViewBag.breakage = _db.Targets.Single(t => t.Subject == "Breakage").TargetNum;
            var model = new BreakageSummaryViewFormModel()
            {
                BreakageWFPs = _db.Vw_BreakagesWithinFinancialPeriod.Where(x=>x.FinancialPeriod==period && x.FinancialYear==year && x.isDeleted==false).ToList(),
                GroupByModels = _db.Vw_BreakagesWithinFinancialPeriod.Where(x=>x.FinancialPeriod==period && x.FinancialYear==year && x.isDeleted==false)
                    .GroupBy(x => new {x.FinancialPeriod, x.FinancialYear}).Select(y => new GroupByModel()
                    {
                        Period = y.Key.FinancialPeriod, Year = y.Key.FinancialYear,
                        Sum = y.Sum(z => z.Cost)
                    }).ToList(),
                Periodddl = _db.Vw_BreakagesWithinFinancialPeriod.Select(x=>new DropDownBoxList()
                {
                    text=x.FinancialPeriod,
                    value=x.FinancialPeriod
                }).Distinct().ToList(),
                Yearddl = _db.Vw_BreakagesWithinFinancialPeriod.Select(x=>new DropDownBoxList()
                {
                    text=x.FinancialYear,
                    value=x.FinancialYear
                }).Distinct().ToList(),
                selectedPeriod = period,
                selectedYear = year
                
            };

            
           
         
            return PartialView(model);
        }

        public static string CurrentPeriod()
        {
            var currentPeriod=_basedata.FinancialCalendars
                .Where(x=>x.CurrentPeriod==true)
                .Select(x => x.FinancialPeriod)
                .FirstOrDefault();
            return currentPeriod;
        }
        public static string CurrentYear()
        {
            var currentYear= _basedata.FinancialCalendars
                .Where(x=>x.CurrentYear==true)
                .Select(x => x.FinancialYear)
                .FirstOrDefault();
            return currentYear;
        }

        // GET: Breakages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Breakage breakage = _db.Breakages.Find(id);
            if (breakage == null)
            {
                return HttpNotFound();
            }
            
            var model = new BreakageFormViewModel()
            {
                Id= breakage.BreakageId,
                Subject = breakage.Subject,
                Date = breakage.Date,
                SiteId = breakage.Site,
                Cost = breakage.Cost,
                TypeId = breakage.Type,
                Sites = _db.Sites.ToList(),
                Types = _db.BreakageTypes.ToList()
                
                
            };
            

            return PartialView(model);
        }

        // GET: Breakages/Create
        public ActionResult Create()
        {
            var model = new BreakageFormViewModel
            {
                Date = DateTime.Today,
                Sites = _db.Sites.ToList(),
                Types = _db.BreakageTypes.ToList()
                    
            };
            return PartialView(model);
        }

        // POST: Breakages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public JsonResult Create(BreakageFormViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                viewModel.Sites = _db.Sites.ToList();
                viewModel.Types = _db.BreakageTypes.ToList();
                
            }

            var breakage = new Breakage
            {
                BreakageId= viewModel.Id,
                Subject = viewModel.Subject,
                Date = viewModel.Date,
                Site = viewModel.SiteId,
                Cost = viewModel.Cost,
                Type = viewModel.TypeId,
                isDeleted = false
            };

            _db.Breakages.Add(breakage);
            _db.SaveChanges();


            return Json("Success", JsonRequestBehavior.AllowGet);
        }

        // GET: Breakages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Breakage breakage = _db.Breakages.Find(id);
            if (breakage == null)
            {
                return HttpNotFound();
            }
            
            var model = new BreakageFormViewModel()
            {
                Id= breakage.BreakageId,
                Subject = breakage.Subject,
                Date = breakage.Date,
                SiteId = breakage.Site,
                Cost = breakage.Cost,
                TypeId = breakage.Type,
                Sites = _db.Sites.ToList(),
                Types = _db.BreakageTypes.ToList()
                
                
            };
            

            return PartialView(model);
        }

        // POST: Breakages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit(BreakageFormViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Breakage breakage = _db.Breakages.Find(viewModel.Id);
                if (breakage == null)
                {
                    return HttpNotFound();
                }

                breakage.Subject = viewModel.Subject;
                breakage.Date = viewModel.Date;
                breakage.Site = viewModel.SiteId;
                breakage.Cost = viewModel.Cost;
                breakage.Type = viewModel.TypeId;
                
                _db.SaveChanges();
                
            }
            return Json("success", JsonRequestBehavior.AllowGet);
        }

        //// GET: Breakages/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Breakage breakage = db.Breakages.Find(id);
        //    if (breakage == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(breakage);
        //}

        //// POST: Breakages/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Breakage breakage = db.Breakages.Find(id);
        //    db.Breakages.Remove(breakage);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Breakage breakage = _db.Breakages.Find(id);
            if (breakage == null)
            {
                return HttpNotFound();
            }
            
            var model = new BreakageFormViewModel()
            {
                Id= breakage.BreakageId,
                Subject = breakage.Subject,
                Date = breakage.Date,
                SiteId = breakage.Site,
                Cost = breakage.Cost,
                TypeId = breakage.Type,
                Sites = _db.Sites.ToList(),
                Types = _db.BreakageTypes.ToList()
                
                
            };
            

            return PartialView(model);
        }

        [HttpPost]
        public JsonResult DeleteConfirmed(int? id)
        {
            Breakage breakage = _db.Breakages.Find(id);
            if (breakage != null)
            {
                breakage.isDeleted = true;
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
