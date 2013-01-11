using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QATool.Models
{
    public class Step
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public virtual int StepId { get; set; }

        public virtual int TestCaseId { get; set; }

        [Required]
        public virtual string Action { get; set; }
      
        [Required]
        public virtual string Result { get; set; }
    }
}