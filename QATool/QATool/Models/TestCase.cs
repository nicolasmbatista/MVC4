using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QATool.Models
{
    public class TestCase
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public virtual int TestCaseId { get; set; }

        public virtual int ProjectId { get; set; }
        
        [StringLength(120)]
        [Required]
        public virtual string Scenario { get; set; }

        [StringLength(50)]
        [Required]
        public virtual string Feature { get; set; }
        
        [Required]
        public virtual string Result { get; set; }
        //DropDown for list for result
        public virtual IEnumerable<SelectListItem> Results { get; set; }

        //Declaring this int with a ? makes it so it can be null, cause not every test case has a bug!
        public virtual int? BugId { get; set; }
        public virtual string Comments { get; set; }
        public virtual string Environment { get; set; }
        public virtual List<Step> Steps { get; set; }

        public TestCase DeepClone()
        {
            TestCase newTestCase = new TestCase();
            newTestCase.Result = this.Result;
            newTestCase.Scenario = this.Scenario;
            newTestCase.BugId = this.BugId;
            newTestCase.Comments = this.Comments;
            newTestCase.Feature = this.Feature;
            newTestCase.Environment = this.Environment;
            newTestCase.Steps = new List<Step>();
            foreach (var step in this.Steps)
            {
                Step aux = step.DeepClone();
                newTestCase.Steps.Add(aux);
            }  

            return newTestCase;
        }
    }
}