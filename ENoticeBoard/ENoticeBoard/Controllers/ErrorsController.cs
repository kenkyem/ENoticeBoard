using System.Web.Mvc;

namespace ENoticeBoard.Controllers
{
    public class ErrorsController : Controller
    {
        // GET: Errors
        public ActionResult Error()
        {
            return View();
        }
    }
}