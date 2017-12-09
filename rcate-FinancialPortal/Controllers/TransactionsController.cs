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

    public class TransactionsController : Universal
    {

        // GET: Transactions
        public ActionResult Index(int? id)
        {
            var user = db.Users.Find(User.Identity.GetUserId());

            var transaction = user.HouseHold.Accounts.SelectMany(t => t.Transactions);
            return View(transaction.OrderByDescending(t => t.Created).ToList());
        }

        // GET: Transactions/Details/5
        public ActionResult Details(int? id)
        {
            Accounts account = db.Accounts.Find(id);
            Transaction transaction = db.Transaction.Find(id);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (transaction == null)
            {
                return HttpNotFound();
            }

            return View(transaction);
        }

        // GET: Transactions/Create
        public ActionResult Create(int? id)
        {
            var user = db.Users.Find(User.Identity.GetUserId());
            var account = db.Accounts.Find(id);

            ViewBag.CategoryId = new SelectList(db.TransactionCategory, "Id", "Name");
            ViewBag.AccountsId = new SelectList(db.Accounts.Where(a => a.HouseHoldId == user.HouseHoldId), "Id", "Name");
            ViewBag.BudgetId = new SelectList(db.Budget.Where(b => b.HouseHoldId == user.HouseHoldId), "Id", "Name");
            return View();
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,Amount,Created,ReconciledAmount,Reconciled,AuthorId,CategoryId,AccountsId,BudgetId,ExpenseId,HouseHoldId")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.Find(User.Identity.GetUserId());
                Accounts accounts = db.Accounts.Find(transaction.AccountsId);

                transaction.AuthorId = user.Id;
                transaction.Created = DateTime.Now;
                transaction.HouseHoldId = user.HouseHold.Id;
                transaction.Void = false;

                if (transaction.CategoryId == 2 && transaction.Void == false)
                {
                    decimal transAmt = transaction.Amount * -1;
                    accounts.Balance = accounts.Balance + transAmt;
                }
                if (transaction.CategoryId == 1 && transaction.Void == false)
                {
                    accounts.Balance = accounts.Balance + transaction.Amount;
                }

                db.Transaction.Add(transaction);
                db.Entry(accounts).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var members = db.HouseHold.SelectMany(m => m.Members).ToList();

            ViewBag.AuthorId = new SelectList(members, "Id", "FullName", transaction.AuthorId);
            ViewBag.CategoryId = new SelectList(db.TransactionCategory, "Id", "Name", transaction.CategoryId);
            ViewBag.AccountsId = new SelectList(db.Accounts, "Id", "Name", transaction.AccountsId);
            //ViewBag.ExpenseId = new SelectList(db.Expense, "Id", "Name", transaction.ExpenseId);
            ViewBag.BudgetId = new SelectList(db.Budget, "Id", "Name", transaction.BudgetId);
            return View(transaction);
        }

        // GET: Transactions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transaction.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            ViewBag.AuthorId = new SelectList(db.Users, "Id", "FirstName", transaction.AuthorId);
            ViewBag.CategoryId = new SelectList(db.TransactionCategory, "Id", "Name", transaction.CategoryId);
            ViewBag.AccountsId = new SelectList(db.Accounts, "Id", "Name");
            return View(transaction);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,Amount,Created,ReconciledAmount,Reconciled,AuthorId,CategoryId,AccountsId,BudgetId,ExpenseId,HouseHoldId")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                Accounts accounts = db.Accounts.Find(transaction.AccountsId);

                if (transaction.CategoryId == 2 && transaction.Void == false)
                {
                    decimal transAmt = transaction.Amount * -1;
                    accounts.Balance = accounts.Balance + transAmt;
                }
                if (transaction.CategoryId == 1 && transaction.Void == false)
                {
                    accounts.Balance = accounts.Balance + transaction.Amount;
                }

                db.Entry(accounts).State = EntityState.Modified;
                db.Entry(transaction).State = EntityState.Modified;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AuthorId = new SelectList(db.Users, "Id", "FirstName", transaction.AuthorId);
            ViewBag.CategoryId = new SelectList(db.TransactionCategory, "Id", "Name", transaction.CategoryId);
            ViewBag.AccountId = new SelectList(db.Accounts, "Id", "Name");
            return View(transaction);
        }

        // GET: Transactions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transaction.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Transaction transaction = db.Transaction.Find(id);
            db.Transaction.Remove(transaction);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Transaction/Void
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult VoidTransaction(int? id)
        {
            Transaction transaction = db.Transaction.Find(id);
            transaction.Void = true;

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Transaction/Un-Void
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UnVoidTransaction(int? id)
        {
            Transaction transaction = db.Transaction.Find(id);
            transaction.Void = false;

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
