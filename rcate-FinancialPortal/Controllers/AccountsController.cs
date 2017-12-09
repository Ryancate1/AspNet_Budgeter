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

    public class AccountsController : Universal
    {

        // GET: Accounts
        public ActionResult Index()
        {
            var user = db.Users.Find(User.Identity.GetUserId());
            var accounts = db.Accounts.Where(a => a.HouseHoldId == user.HouseHoldId);

            return View(accounts.OrderByDescending(a => a.Owner.FirstName).ToList());
        }

        // GET: Accounts/Details/5
        public ActionResult Details(int? id)
        {
            Accounts accounts = db.Accounts.Find(id);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            if (accounts == null)
            {
                return HttpNotFound();
            }

            ViewBag.Transactions = db.Transaction.Where(t => t.AccountsId == accounts.Id).OrderByDescending(t => t.Id).ToList();

            return View(accounts);
        }

        // GET: Accounts/Create
        public ActionResult Create()
        {
            ViewBag.AccountTypeId = new SelectList(db.AccountType, "Id", "Name");
            return View();
        }

        // POST: Accounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,Opened,Updated,AccountNumber,OwnerId,AccountTypeId,Balance,HouseHoldId")] Accounts accounts)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.Find(User.Identity.GetUserId());
                accounts.Opened = DateTime.Now;
                accounts.OwnerId = User.Identity.GetUserId();
                accounts.HouseHold = user.HouseHold;
                db.Accounts.Add(accounts);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AccountTypeId = new SelectList(db.AccountType, "Id", "Name", accounts.AccountTypeId);
            return View(accounts);
        }

        // GET: Accounts/Edit/5
        public ActionResult Edit(int? id)
        {
            Accounts accounts = db.Accounts.Find(id);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            if (accounts == null)
            {
                return HttpNotFound();
            }

            ViewBag.AccountTypeId = new SelectList(db.AccountType, "Id", "Name", accounts.AccountTypeId);
            return View(accounts);
            //return RedirectToAction("Index");
        }

        // POST: Accounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,Opened,AccountNumber,OwnerId,AccountTypeId,Balance,HouseHoldId")] Accounts accounts)
        {
            if (ModelState.IsValid)
            {
                db.Entry(accounts).State = EntityState.Modified;
                accounts.Updated = DateTime.Now;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AccountTypeId = new SelectList(db.AccountType, "Id", "Name", accounts.AccountTypeId);
            return View(accounts);
        }

        // GET: Accounts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Accounts accounts = db.Accounts.Find(id);
            if (accounts == null)
            {
                return HttpNotFound();
            }
            return View(accounts);
        }

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Accounts accounts = db.Accounts.Find(id);
            db.Accounts.Remove(accounts);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateTransaction([Bind(Include = "Id,Name,Description,Amount,Created,ReconciledAmount,Reconciled,AuthorId,CategoryId")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.Find(User.Identity.GetUserId());

                transaction.AuthorId = user.Id;
                db.Transaction.Add(transaction);
                db.SaveChanges();
                return RedirectToAction("Details", "Accounts", new { id = transaction.AccountsId });
            }

            ViewBag.AuthorId = new SelectList(db.Users, "Id", "FirstName", transaction.AuthorId);
            ViewBag.CategoryId = new SelectList(db.TransactionCategory, "Id", "Name", transaction.CategoryId);
            return View(transaction);
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
