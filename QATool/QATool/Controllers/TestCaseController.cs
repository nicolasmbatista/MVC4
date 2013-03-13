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
            @ViewBag.ProjectName = _db.Projects.Find(testcase.ProjectId).Name;
            testcase.Results = new []
            {
                new SelectListItem { Value = "PASSED", Text = "PASSED" },
                new SelectListItem { Value = "FAILED", Text = "FAILED" },
                new SelectListItem { Value = "UNTESTED", Text = "UNTESTED" }
            }; 

            return View(testcase);
        }

        //
        // POST: /TestCase/Edit/5

        [HttpPost]
        public ActionResult Edit(TestCase testcase)
        {
            if (ModelState.IsValid)
            {
                var dbTestcase = _db.TestCases.Include(x => x.Steps).Where(x => x.TestCaseId == testcase.TestCaseId).FirstOrDefault();

                if (dbTestcase == null)
                {
                    return new HttpNotFoundResult();
                }
                else
                {
                    //This way, the user can't change the project id or the testcase id as he pleases
                    dbTestcase.BugId = testcase.BugId;
                    dbTestcase.Comments = testcase.Comments;
                    dbTestcase.Environment = testcase.Environment;
                    dbTestcase.Feature = testcase.Feature;
                    dbTestcase.Result = testcase.Result;
                    dbTestcase.Scenario = testcase.Scenario;                    

                    if (testcase.Steps != null)
                    {
                        foreach (var step in testcase.Steps)
                        {
                            step.TestCaseId = testcase.TestCaseId;
                        }

                        var newSteps = testcase.Steps.Where(x => x.StepId == 0);
                        //var modifiedSteps = testcase.Steps.Where(x => x.StepId != 0);
                        List<Step> modifiedSteps = testcase.Steps.Where(x => x.StepId != 0).ToList();
                        dbTestcase.Steps.AddRange(newSteps);
                        HashSet<int> newStepIds = new HashSet<int>(testcase.Steps.Select(x => x.StepId));
                         
                        dbTestcase.Steps.RemoveAll(x => !newStepIds.Contains(x.StepId));

                        //This should be done inside a while, so you don't have to search in the entire list for every item.
                        foreach (var modStep in modifiedSteps)
                        {
                            foreach (var oldStep in dbTestcase.Steps)
                            {
                                //If the step already exists, i should update it
                                if (oldStep.StepId == modStep.StepId)
                                {
                                    oldStep.Action = modStep.Action;
                                    oldStep.Result = modStep.Result;
                                }
                            }
                        }
                    }
                    else
                    {
                        dbTestcase.Steps.RemoveAll(x => x.TestCaseId == testcase.TestCaseId);
                    }
                    _db.SaveChanges();
                }

                return RedirectToAction("List", "Project", new { projectId = testcase.ProjectId });
            }
            
            return new HttpNotFoundResult();
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