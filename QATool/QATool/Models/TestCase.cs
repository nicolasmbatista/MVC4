using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

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
        
        public virtual int BugId { get; set; }
        public virtual string Comments { get; set; }
        public virtual string Environment { get; set; }
        public virtual List<Step> Steps { get; set; }
    }
}