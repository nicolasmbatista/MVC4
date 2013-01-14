using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QATool.Models;

namespace QATool.Controllers
{
    public class TestCaseController : Controller
    {
        private QAToolDBContext _db = new QAToolDBContext();

        //
        // GET: /TestCase/

        public ActionResult Index(int projectId)
        {
            var project = _db.Projects.Include(x => x.TestCases).Where(x => x.ProjectId == projectId).FirstOrDefault();
            
            if (project.TestCases == null)
                return new HttpNotFoundResult();

            ViewBag.projectId = projectId;
            return View(project.TestCases);
        }

        //
        // GET: /TestCase/Details/5

        public ActionResult Details(int id = 0)
        {
            TestCase testcase = _db.TestCases.Find(id);
            if (testcase == null)
            {
                return HttpNotFound();
            }
            return View(testcase);
        }

        //
        // GET: /TestCase/Create

        public ActionResult Create(int projectId)
        {
            ViewBag.projectId = projectId;
            return View();
        }

        //
        // POST: /TestCase/Create

        [HttpPost]
        public ActionResult Create(TestCase testcase)
        {
            if (ModelState.IsValid)
            {
                _db.TestCases.Add(testcase);
                _db.SaveChanges();
                return RedirectToAction("List", "Project", new { projectId = testcase.ProjectId });
            }

            return View(testcase);
        }

        //
        // GET: /TestCase/Edit/5

        public ActionResult Edit(int id = 0)
        {
            TestCase testcase = _db.TestCases.Find(id);
            if (testcase == null)
            {
                return HttpNotFound();
            }
            //Cambiar esto por el proyecto al q pertenece el testcase
            @ViewBag.ProjectName = "Proyecto 123";
            return View(testcase);
        }

        //
        // POST: /TestCase/Edit/5

        [HttpPost]
        public ActionResult Edit(TestCase testcase)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(testcase).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("List", "Project", new { projectId = testcase.ProjectId });
            }
            return View(testcase);
        }

        //
        // GET: /TestCase/Delete/5

        public ActionResult Delete(int id)
        {
            TestCase testcase = _db.TestCases.Find(id);
            if (testcase == null)
            {
                return HttpNotFound();
            }
            return View(testcase);
        }

        //
        // POST: /TestCase/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            TestCase testcase = _db.TestCases.Find(id);
            _db.TestCases.Remove(testcase);
            _db.SaveChanges();
            return RedirectToAction("List", "Project", new { projectId = testcase.ProjectId });
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }

    }
}