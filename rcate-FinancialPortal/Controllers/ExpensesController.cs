﻿using System;
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

    //public class ExpensesController : Universal
    //{

    //    // GET: Expenses
    //    public ActionResult Index()
    //    {
    //        var expenses = db.Expense.Include(e => e.Category).Include(e => e.Owner);
    //        return View(expenses.ToList());
    //    }

    //    // GET: Expenses/Details/5
    //    public ActionResult Details(int? id)
    //    {
    //        if (id == null)
    //        {
    //            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    //        }
    //        Expense expense = db.Expense.Find(id);
    //        if (expense == null)
    //        {
    //            return HttpNotFound();
    //        }
    //        return View(expense);
    //    }

    //    // GET: Expenses/Create
    //    public ActionResult Create()
    //    {
    //        ViewBag.CategoryId = new SelectList(db.ExpenseCategory, "Id", "Name");
    //        return View();
    //    }

    //    // POST: Expenses/Create
    //    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    //    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public ActionResult Create([Bind(Include = "Id,Name,OwnerId,Description,CategoryId")] Expense expense)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            var user = db.Users.Find(User.Identity.GetUserId());

    //            expense.HouseHoldId = user.HouseHold.Id;
    //            expense.OwnerId = user.Id;

    //            db.Expense.Add(expense);
    //            db.SaveChanges();
    //            return RedirectToAction("Index");
    //        }

    //        ViewBag.CategoryId = new SelectList(db.ExpenseCategory, "Id", "Name", expense.CategoryId);
    //        return View(expense);
    //    }

    //    // GET: Expenses/Edit/5
    //    public ActionResult Edit(int? id)
    //    {
    //        if (id == null)
    //        {
    //            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    //        }
    //        Expense expense = db.Expense.Find(id);
    //        if (expense == null)
    //        {
    //            return HttpNotFound();
    //        }
    //        ViewBag.CategoryId = new SelectList(db.ExpenseCategory, "Id", "Name", expense.CategoryId);
    //        ViewBag.OwnerId = new SelectList(db.Users, "Id", "FirstName", expense.OwnerId);
    //        return View(expense);
    //    }

    //    // POST: Expenses/Edit/5
    //    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    //    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public ActionResult Edit([Bind(Include = "Id,Name,OwnerId,Description,CategoryId")] Expense expense)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            db.Entry(expense).State = EntityState.Modified;
    //            db.SaveChanges();
    //            return RedirectToAction("Index");
    //        }
    //        ViewBag.CategoryId = new SelectList(db.ExpenseCategory, "Id", "Name", expense.CategoryId);
    //        ViewBag.OwnerId = new SelectList(db.Users, "Id", "FirstName", expense.OwnerId);
    //        return View(expense);
    //    }

    //    // GET: Expenses/Delete/5
    //    public ActionResult Delete(int? id)
    //    {
    //        if (id == null)
    //        {
    //            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    //        }
    //        Expense expense = db.Expense.Find(id);
    //        if (expense == null)
    //        {
    //            return HttpNotFound();
    //        }
    //        return View(expense);
    //    }

    //    // POST: Expenses/Delete/5
    //    [HttpPost, ActionName("Delete")]
    //    [ValidateAntiForgeryToken]
    //    public ActionResult DeleteConfirmed(int id)
    //    {
    //        Expense expense = db.Expense.Find(id);
    //        db.Expense.Remove(expense);
    //        db.SaveChanges();
    //        return RedirectToAction("Index");
    //    }

    //    protected override void Dispose(bool disposing)
    //    {
    //        if (disposing)
    //        {
    //            db.Dispose();
    //        }
    //        base.Dispose(disposing);
    //    }
    //}
}
