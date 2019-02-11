using ENoticeBoard.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ENoticeBoard.Controllers
{
    public class TicketsController : Controller
    {
        private readonly MyDatabaseEntities _db = new MyDatabaseEntities();
        private readonly HelpDeskStatsEntities _hd = new HelpDeskStatsEntities();
        

        // GET: Tickets
        public ActionResult Summary(string period, string year)
        {
            if (period == null && year == null)
            {
                DateConversion.CurrentPeriod();
                DateConversion.CurrentYear();
            }

            ViewBag.TicketTarget = _db.Targets.Single(x => x.Subject == "OpenTicket").TargetNum;
            List<ENoticeBoardMstr> openHelpDeskTickets = new List<ENoticeBoardMstr>(_hd.ENoticeBoardMstrs.Where(x=>x.Status == "open" && x.Category=="HelpDesk")).ToList();
            return PartialView(openHelpDeskTickets);
        
        }
    }


}