using ENoticeBoard.Models;
using System;
using System.Data.Entity.Validation;
using System.Net.Mail;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ENoticeBoard
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(WebConfigurationManager.AppSettings["Debug"]) == true)
                return;
            try
            {
                Exception exception = Server.GetLastError();


                var mail = new MailMessage { From = new MailAddress(WebConfigurationManager.AppSettings["ITFromEmail"]) };

                mail.To.Add(new MailAddress(WebConfigurationManager.AppSettings["DebugToEmail"]));
                mail.Subject = "ENoticeBoard Site Error at " + DateTime.Now;
                AdInfo ad = new AdInfo();
                string user = System.Web.HttpContext.Current.User.Identity.Name;
                user = user.ToLower().Replace("oneharvest\\", "");

                mail.Body = " User :" + ad.GetDisplayNameFromAd(user) + "\r\n\r\n";

                var dbEntityValidationException = Server.GetLastError() as DbEntityValidationException;
                if (dbEntityValidationException != null)
                {
                    mail.Body += "DbEntityValidationException" + "\r\n";

                    foreach (var eve in dbEntityValidationException.EntityValidationErrors)
                    {
                        mail.Body += $"Entity of type  {eve.Entry.Entity.GetType().Name}  in state  {eve.Entry.State }  has the following validation errors:";
                        foreach (var ve in eve.ValidationErrors)
                        {
                            mail.Body += $"- Property: {ve.PropertyName}, Error: {ve.ErrorMessage}\r\n";
                        }
                    }
                    mail.Body += "\r\n\r\n";
                }


                mail.Body += "Error Description: " + exception;

                var server = new SmtpClient { Host = WebConfigurationManager.AppSettings["MailServer"] };
                //server.Send(mail);


                Response.Redirect("~/Errors/Error");
            }
            catch (Exception)
            {
            }


        }

    }
}
