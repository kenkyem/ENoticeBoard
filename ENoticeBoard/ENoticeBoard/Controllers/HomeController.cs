using ENoticeBoard.Models;
using ENoticeBoard.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using static ENoticeBoard.Models.AdInfo;

namespace ENoticeBoard.Controllers
{
    public class HomeController : Controller
    {
        private readonly MyDatabaseEntities _db = new MyDatabaseEntities();
        private readonly BaseDataEntities _basedata = new BaseDataEntities();

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";


            SqlLite ticketslist = new SqlLite();
            DataTable ticketTable = ticketslist.ConnectSqLite();


            return View(ticketTable);
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

            

            //Get Target Value for Comparision
            var downtimePlannedTarget = _db.Targets.Single(t => t.Subject == "Downtime_Planned").TargetNum;
            var downtimeUnplannedTarget = _db.Targets.Single(t => t.Subject == "Downtime_Unplanned").TargetNum;
            var breakageTarget = _db.Targets.Single(t => t.Subject == "Breakage").TargetNum;
            var budgetTarget = _db.Targets.Single(t => t.Subject == "Budget").TargetNum;
            var openTicketTarget = _db.Targets.Single(t => t.Subject == "OpenTicket").TargetNum;

            //Get Actual Value 
            var downtimeSum = downtime.Any() ? downtime.Sum(x => x.Duration) : 0;
            var budgetSum = spend.Any() ? spend.Sum(x => x.Cost) : 0M;
            var breakageSum = brreakage.Any() ? brreakage.Sum(x => x.Cost) : 0M;
            
            //Downtime has 2 statement for bgcolor
            var downtimePlannedMin = downtime.Where(dt => dt.Status == "Planned").Sum(dt => dt.Duration);
            var downtimeUnplannedMin = downtime.Where(dt => dt.Status == "Unplanned").Sum(dt => dt.Duration);
            var bgDtPlanned = SetBgColor(downtimePlannedTarget, downtimePlannedMin);
            var bgDtUnplanned = SetBgColor(downtimeUnplannedTarget, downtimeUnplannedMin);
            //Get no of tickets from sqlite
            SqlLite helpDesk = new SqlLite();
            var openTicketSum = helpDesk.ConnectSqLite().Rows.Count; 


            var model = new HomeViewModel()
            {
                Rocks = _db.Rocks.OrderByDescending(s => s.Priority)
                    .Where(s => s.Done == false)
                    .ToList(),
                DowntimeSum = downtimeSum,
                BudgetSum = budgetSum,
                BreakageSum = breakageSum,
                OpenticketSum= openTicketSum,
                BgColorBreakage = SetBgColor(breakageTarget, breakageSum),
                BgColorBudget = SetBgColor(budgetTarget, budgetSum),
                BgColorDowntime = (bgDtPlanned.Contains("bg-red") || bgDtUnplanned.Contains("bg-red")
                    ? "bg-red"
                    : "bg-green"),
                BgColorOpenTicket = SetBgColor(openTicketTarget, openTicketSum),
                Users = _db.Users.ToList()
            };


            return View(model);
        }

        public ActionResult Manage()
        {
            if (!UserIsAdmin())
            {
                return View();
            }

            var model = new ManageFormViewModel()
            {
                Users = _db.Users.ToList(),
                Targets = _db.Targets.ToList()
            };
            return View(model);
        }

        public bool UserIsIT()

        {
            string user = System.Web.HttpContext.Current.User.Identity.Name;
            user = user.ToLower().Replace("oneharvest\\", "");
            List<string> groupOnfo = GetDepartmentFromAd(user);
            bool v = groupOnfo.Contains("IT-WACOL") ? true : false;
            return v;
        }

        public bool UserIsAdmin()
        {
            string user = System.Web.HttpContext.Current.User.Identity.Name;
            user = user.ToLower().Replace("oneharvest\\", "");
            string userEmail = GetEmailFromAd(user).ToLower();
            foreach (var p in _db.Users)
            {
                if (userEmail.Equals(p.Email.ToLower()) && p.Role == "Admin")
                    return true;
            }

            return false;
        }

        //Set Bg Color for Panel
        public string SetBgColor(decimal target, decimal actual)
        {
            if (target >= actual)
                return "bg-green";
            else
                return "bg-red";
        }


        //public decimal GetSumBudget()
        //{
        //    var spend = _db.Vw_ObjectsWithinFinancialPeriod.Where(
        //            x=>x.FinancialPeriod==_currentPeriod && x.FinancialYear==_currentYear && x.isDeleted==false)
        //        .ToList();

        //    decimal objectSpend = spend.Any() ? spend.Sum(x => x.Cost) : 0M;
        //    return objectSpend;
        //}


        //Calendar
        public JsonResult GetEvents()
        {
            using (MyDatabaseEntities dc = new MyDatabaseEntities())
            {
                var today = DateTime.Now.Date;
                //var events = dc.Events.Where(x=>x.End>=today).ToList();
                //var users = dc.Users.ToList();
                var eventtpreturn = from t1 in dc.Events
                    join t2 in dc.Users on t1.Email equals t2.Email
                    select new
                    {
                        EventId = t1.EventID, Subject = t1.Subject, Description = t1.Description, Start = t1.Start,
                        End = t1.End, ThemeColor = t2.Color, Email = t1.Email
                    };

                var events = eventtpreturn.ToList().Select(x => new Event
                {
                    EventID = x.EventId, Subject = x.Subject, Description = x.Description, Start = x.Start, End = x.End,
                    ThemeColor = x.ThemeColor, Email = x.Email
                }).ToList();


                return new JsonResult {Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet};
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
                        v.Email = e.Email;
                    }
                }
                else
                {
                    dc.Events.Add(e);
                }

                dc.SaveChanges();
                status = true;
            }

            return new JsonResult {Data = new {status = status}};
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

            return new JsonResult {Data = new {status = status}};
        }
    }
}