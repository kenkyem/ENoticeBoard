using ENoticeBoard.Models;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace ENoticeBoard.Controllers
{
    public class TicketsController : Controller
    {   
        private readonly MyDatabaseEntities _db =new MyDatabaseEntities();
        // GET: Tickets
        public ActionResult Summary(string period,string year)
        {
            if (period == null && year == null)
            {
                period = DateConversion.CurrentPeriod();
                year = DateConversion.CurrentYear();
            }

            ViewBag.TicketTarget = _db.Targets.Single(x => x.Subject == "OpenTicket").TargetNum;
            SqlLite ticketslist = new SqlLite();
            DataTable ticketTable = ticketslist.ConnectSqLite();
            
            return PartialView(ticketTable);
        }
    }
}