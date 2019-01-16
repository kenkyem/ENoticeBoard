using ENoticeBoard.ViewModels;
using System;
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

        
        // GET: Rocks
        public ActionResult Index()
        {   
            string currentPeriod=_basedata.FinancialCalendars
                .Where(x=>x.CurrentPeriod==true)
                .Select(x => x.FinancialPeriod)
                .FirstOrDefault();
            string currentYear= _basedata.FinancialCalendars
                .Where(x=>x.CurrentYear==true)
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
                x=>x.FinancialPeriod==currentPeriod && x.FinancialYear==currentYear && x.isDeleted==false)
                .ToList();
            ViewBag.downtime = downtime.Any() ? downtime.Sum(x => x.Duration) : 0;
            ViewBag.objectSpend = spend.Any() ? spend.Sum(x => x.Cost) : 0M;
            ViewBag.breakageCost = brreakage.Any() ? brreakage.Sum(x => x.Cost) : 0M;

            var rockModel = new RockFormViewModel()
            {
                Rocks = _db.Rocks.OrderByDescending(s => s.Priority)
                    .Where(s => s.Done == false)
                    .ToList()
            };
            //var rocksViewModel = _db.Rocks.OrderByDescending(s => s.Priority)
            //                                .Where(s => s.Done == false)
            //                                .ToList();
            //List<RockFormViewModel> model=new List<RockFormViewModel>();
            //foreach (var r in rocksViewModel)
            //{
            //    var modelToSend = new RockFormViewModel
            //    {
            //        RockId = r.RockId, 
            //        Subject = r.Subject,
            //        DateDue = r.DateDue,
            //        Priority = r.Priority,
            //        Done = r.Done
            //    };
            //    model.Add(modelToSend);
            //}
            
            return View(rockModel);
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