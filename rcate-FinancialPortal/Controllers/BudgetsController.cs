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

namespace rcate_FinancialPortal.Controllers
{
    [Authorize]
    public class BudgetsController : Universal
    {

        // GET: Budgets
        public ActionResult Index()
        {
            var user = db.Users.Find(User.Identity.GetUserId());

            var budgets = db.Budget.Where(b => b.HouseHoldId == user.HouseHoldId);
            return View(budgets.ToList());
        }

        // GET: Budgets/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Budget budget = db.Budget.Find(id);
        //    if (budget == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(budget);
        //}

        // GET: Budgets/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.BudgetCategory, "Id", "Name");
            return View();
        }

        // POST: Budgets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,CategoryId,OwnerId,Amount,HouseHoldId")] Budget budget)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.Find(User.Identity.GetUserId());

                budget.HouseHoldId = user.HouseHold.Id;
                budget.OwnerId = user.Id;

                db.Budget.Add(budget);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.BudgetCategory, "Id", "Name", budget.CategoryId);
            return View(budget);
        }

        // GET: Budgets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Budget budget = db.Budget.Find(id);
            if (budget == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.BudgetCategory, "Id", "Name", budget.CategoryId);
            ViewBag.OwnerId = new SelectList(db.Users, "Id", "FirstName", budget.OwnerId);
            return View(budget);
        }

        // POST: Budgets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,CategoryId,OwnerId,Amount,HouseHoldId")] Budget budget)
        {
            if (ModelState.IsValid)
            {
                db.Entry(budget).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.BudgetCategory, "Id", "Name", budget.CategoryId);
            ViewBag.OwnerId = new SelectList(db.Users, "Id", "FirstName", budget.OwnerId);
            return View(budget);
        }

        // GET: Budgets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Budget budget = db.Budget.Find(id);
            if (budget == null)
            {
                return HttpNotFound();
            }
            return View(budget);
        }

        // POST: Budgets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Budget budget = db.Budget.Find(id);
            db.Budget.Remove(budget);
            db.SaveChanges();
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
