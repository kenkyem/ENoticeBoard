using ENoticeBoard.Models;
using ENoticeBoard.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ENoticeBoard.Controllers
{
    public class HomeController : Controller
    {
        private readonly MyDatabaseEntities _db = new MyDatabaseEntities();
        private readonly BaseDataEntities _basedata = new BaseDataEntities();

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Authorize]
        // GET: Rocks
        public ActionResult Index()
        {
            if (!UserIsIT())
            {
                return View();
            }

            string currentPeriod = _basedata.FinancialCalendars
                .Where(x => x.CurrentPeriod == true)
                .Select(x => x.FinancialPeriod)
                .FirstOrDefault();
            string currentYear = _basedata.FinancialCalendars
                .Where(x => x.CurrentYear == true)
                .Select(x => x.FinancialYear)
                .FirstOrDefault();
            var brreakage = _db.Vw_BreakagesWithinFinancialPeriod.Where(x =>
                    x.FinancialPeriod == currentPeriod
                    && x.FinancialYear == currentYear
                    && x.isDeleted == false)
                .ToList();
            var downtime = _db.Vw_DowntimesWithinFinancialPeriod.Where(x =>
                    x.FinancialPeriod == currentPeriod
                    && x.FinancialYear == currentYear
                    && x.isDeleted == false)
                .ToList();
            var spend = _db.Vw_ObjectsWithinFinancialPeriod.Where(
                    x => x.FinancialPeriod == currentPeriod && x.FinancialYear == currentYear &&
                         x.isDeleted == false)
                .ToList();
            ViewBag.downtimePlanned = downtime.Where(dt=>dt.Status == "Planned").Sum(dt => dt.Duration);
            ViewBag.downtimeUnplanned = downtime.Where(dt=>dt.Status == "Unplanned").Sum(dt => dt.Duration);
            var model = new HomeViewModel()
            {
                Rocks = _db.Rocks.OrderByDescending(s => s.Priority)
                    .Where(s => s.Done == false)
                    .ToList(),
                DowntimeSum =  downtime.Any() ? downtime.Sum(x => x.Duration) : 0,
                BudgetSum =  spend.Any() ? spend.Sum(x => x.Cost) : 0M,
                BreakageSum =  brreakage.Any() ? brreakage.Sum(x => x.Cost) : 0M,
                DowntimeTargetPlanned = _db.Targets.SingleOrDefault(t => t.Subject == "Downtime_Planned"),
                DowntimeTargetUnplanned = _db.Targets.SingleOrDefault(t => t.Subject == "Downtime_Unplanned"),
                BreakageTarget = _db.Targets.SingleOrDefault(t => t.Subject == "Breakage"),
                BudgetTarget = _db.Targets.SingleOrDefault(t => t.Subject =="Budget")
                
            };
            


            return View(model);
        }

        public bool UserIsIT()
        {
            string user = System.Web.HttpContext.Current.User.Identity.Name;
            user = user.ToLower().Replace("oneharvest\\", "");
            List<string> groupOnfo = AdInfo.GetDepartmentFromAd(user);
            bool v = groupOnfo.Contains("IT-WACOL") ? true : false;
            return v;

        }
        

        //public decimal GetSumBudget()
        //{
        //    var spend = _db.Vw_ObjectsWithinFinancialPeriod.Where(
        //            x=>x.FinancialPeriod==_currentPeriod && x.FinancialYear==_currentYear && x.isDeleted==false)
        //        .ToList();

        //    decimal objectSpend = spend.Any() ? spend.Sum(x => x.Cost) : 0M;
        //    return objectSpend;
        //}
        //public decimal GetSumBreakage()
        //{
        //    var brreakage = _db.Vw_BreakagesWithinFinancialPeriod.Where(x =>
        //            x.FinancialPeriod == _currentPeriod 
        //            && x.FinancialYear == _currentYear 
        //            && x.isDeleted == false)
        //        .ToList();
        //    var breakageCost= brreakage.Any() ? brreakage.Sum(x => x.Cost) : 0M;
        //    return breakageCost;
        //}

        //public int GetSumDowntime()
        //{
        //    var downtime = _db.Vw_DowntimesWithinFinancialPeriod.Where(x =>
        //        x.FinancialPeriod == _currentPeriod
        //        && x.FinancialYear == _currentYear
        //        && x.isDeleted == false)
        //        .ToList();
        //    var downtimeDuration= downtime.Any() ? downtime.Sum(x => x.Duration) : 0;
        //    return downtimeDuration;

        //}













        //Calendar
        public JsonResult GetEvents()
        {
            using (MyDatabaseEntities dc = new MyDatabaseEntities())
            {
                var today = DateTime.Now.Date;
                var events = dc.Events.Where(x=>x.End>=today).ToList();
                return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        [HttpPost]
        public JsonResult SaveEvent(Event e)
        {
            var status = false;
            using (MyDatabaseEntities dc = new MyDatabaseEntities())
            {
                if (e.EventID > 0)
                {
                    //Update the event
                    var v = dc.Events.FirstOrDefault(a => a.EventID == e.EventID);
                    if (v != null)
                    {
                        
                        v.Subject = e.Subject;
                        v.Start = e.Start;
                        v.End = e.End;
                        v.Description = e.Description;
                        v.ThemeColor = e.ThemeColor;
                    }
                }
                else
                {
                    dc.Events.Add(e);
                }

                dc.SaveChanges();
                status = true;

            }
            return new JsonResult { Data = new { status = status } };
        }

        [HttpPost]
        public JsonResult DeleteEvent(int eventID)
        {
            var status = false;
            using (MyDatabaseEntities dc = new MyDatabaseEntities())
            {
                var v = dc.Events.FirstOrDefault(a => a.EventID == eventID);
                if (v != null)
                {
                    dc.Events.Remove(v);
                    dc.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status} };
        }

    }
}