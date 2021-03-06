﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QATool.Models;

namespace QATool.Controllers
{
    public class StepController : Controller
    {
        private QAToolDBContext db = new QAToolDBContext();

        //
        // GET: /Step/

        public ActionResult Index()
        {
            return View(db.Steps.ToList());
        }

        //
        // GET: /Step/Details/5

        public ActionResult Details(int id = 0)
        {
            Step step = db.Steps.Find(id);
            if (step == null)
            {
                return HttpNotFound();
            }
            return View(step);
        }

        //
        // GET: /Step/Create

        public ActionResult Create(int testCaseId)
        {
            ViewBag.testCaseId = testCaseId;
            return View();
        }

        //
        // POST: /Step/Create

        [HttpPost]
        public ActionResult Create(Step step)
        {
            if (ModelState.IsValid)
            {
                db.Steps.Add(step);
                db.SaveChanges();
                return RedirectToAction("Edit", "TestCase", new { id = step.TestCaseId } );
            }

            return View(step);
        }

        //
        // GET: /Step/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Step step = db.Steps.Find(id);
            if (step == null)
            {
                return HttpNotFound();
            }
            return View(step);
        }

        //
        // POST: /Step/Edit/5

        [HttpPost]
        public ActionResult Edit(Step step)
        {
            if (ModelState.IsValid)
            {
                db.Entry(step).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(step);
        }

        //
        // GET: /Step/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Step step = db.Steps.Find(id);
            if (step == null)
            {
                return HttpNotFound();
            }
            return View(step);
        }

        //
        // POST: /Step/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Step step = db.Steps.Find(id);
            db.Steps.Remove(step);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}