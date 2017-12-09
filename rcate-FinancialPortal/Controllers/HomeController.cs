using Microsoft.AspNet.Identity;
using rcate_FinancialPortal.Models;
using rcate_FinancialPortal.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace rcate_FinancialPortal.Controllers
{
    public class HomeController : Universal
    {

        [AuthorizeHouseHoldRequired]
        public ActionResult Index()
        {
            var user = db.Users.Find(User.Identity.GetUserId());

            var house = db.HouseHold.Find(user.HouseHoldId);

            return View(house);
        }

        public ActionResult UnauthorizedIndex()
        {
                return View(); 
        }

        public ActionResult CreateJoinHouseHold()
        {
            return View();
        }

        public ActionResult Mission()
        {
            return View();
        }

        public ActionResult Story()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Contact(EmailModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var body = "<p>Email From: <bold>{0}</bold>{1}</p><p> Message:</p><p>{2}</p>";
                    var from = "MyPortal<MyPortal@gmail.com>";

                    var email = new MailMessage(from, ConfigurationManager.AppSettings["emailto"])
                    {
                        Subject = "Portal Contact Email",
                        Body = string.Format(body, model.FromName, model.FromEmail, model.Body),
                        IsBodyHtml = true
                    };
                    var svc = new PersonalEmail();
                    await svc.SendAsync(email);
                    return View();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    await Task.FromResult(0);
                }
            }
            return RedirectToAction("Index");
        }
    }
}