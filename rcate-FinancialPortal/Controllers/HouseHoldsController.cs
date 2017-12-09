using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using rcate_FinancialPortal.Models;
using rcate_FinancialPortal.Models.Code_First;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using rcate_FinancialPortal.Models.Helpers;

namespace rcate_FinancialPortal.Controllers
{
    [Authorize]
    public class HouseHoldsController : Universal
    {

        // GET: HouseHolds
        public ActionResult Index()
        {
            var user = db.Users.Find(User.Identity.GetUserId());
            if (user.HouseHoldId != null)
            {
                var houseHold = db.HouseHold.Find(user.HouseHoldId);
                return View(houseHold);
            }
            return RedirectToAction("CreateJoinHouseHold", "Home");
        }

        // GET: Transactions/Details/5
        public ActionResult Details(int? id)
        {
            var user = db.Users.Find(User.Identity.GetUserId());
            HouseHold houseHold = db.HouseHold.Find(id);
            if (user.HouseHoldId != null)
            {
                return View(houseHold);
            }
            else if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else if (houseHold == null)
            {
                return HttpNotFound();
            }
            else
            {
                return RedirectToAction("CreateJoinHouseHold", "Home");
            }

        }

        // GET: HouseHolds/Create
        public ActionResult Create(int? id)
        {
            var user = db.Users.Find(User.Identity.GetUserId());
            HouseHold houseHold = db.HouseHold.Find(id);

            if (user.HouseHoldId == null)
            {
                return View();
            }
            return RedirectToAction("OneHouseWarning", "HouseHolds");
        }

        // POST: HouseHolds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Created,Name,Description,OwnerId")] HouseHold houseHold)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.Find(User.Identity.GetUserId());

                houseHold.OwnerId = User.Identity.GetUserId();
                user.HouseHoldId = houseHold.Id;
                houseHold.Created = DateTime.Now;

                db.HouseHold.Add(houseHold);
                db.SaveChanges();

                await HttpContext.RefreshAuthentication(db.Users.Find(User.Identity.GetUserId()));
                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: HouseHolds/Edit/5
        public ActionResult Edit(int? id)
        {
            HouseHold houseHold = db.HouseHold.Find(id);
            if (User.Identity.GetUserId() != houseHold.OwnerId)
            {
                return RedirectToAction("Login", "Account");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (houseHold == null)
            {
                return HttpNotFound();
            }
            return View(houseHold);
        }

        // POST: HouseHolds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Created,Name,Description,OwnerId,Memebers")] HouseHold houseHold)
        {
            if (ModelState.IsValid)
            {
                db.Entry(houseHold).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(houseHold);
        }

        // GET: HouseHolds/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    HouseHold houseHold = db.HouseHold.Find(id);
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    if (houseHold == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(houseHold);
        //}

        // POST: HouseHolds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var user = db.Users.Find(User.Identity.GetUserId());
            HouseHold houseHold = db.HouseHold.Find(id);

            if (houseHold.Members.Count() == 0)
            {
                db.HouseHold.Remove(houseHold);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }


        /////////////////////////////////////////////////////////////////////////////////



        // GET: HouseHolds/OneHouseWarning
        public ActionResult OneHouseWarning()
        {
            return View();
        }


        // POST: HouseHolds/SurenderHouseId
        [HttpPost]
        public async Task<ActionResult> SurrenderHouseId(int? id)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.Find(User.Identity.GetUserId());
                HouseHold houseHold = db.HouseHold.Find(id);
                user.HouseHoldId = null;

                db.SaveChanges();

                await HttpContext.RefreshAuthentication(db.Users.Find(User.Identity.GetUserId()));

                return RedirectToAction("CreateJoinHouseHold", "Home");
            }
            return View();
        }

        // POST: HouseHolds/KickUser
        [HttpPost]
        public ActionResult KickUser(string id)
        {
            var user = db.Users.Find(User.Identity.GetUserId());
            var SelectedUser = db.Users.Find(id);

            HouseHold houseHold = user.HouseHold;

            if (user.Id == houseHold.OwnerId)
            {
                SelectedUser.HouseHoldId = null;
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
