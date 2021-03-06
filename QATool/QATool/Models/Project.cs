﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QATool.Models
{
    public class Project
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public virtual int ProjectId { get; set; }

        [Required]
        public virtual string Name { get; set; }

        [StringLength(120)]        
        public virtual string Description { get; set; }     
   
        public virtual IList<TestCase> TestCases { get; set; }

        [DataType(DataType.Url)]
        [Required]
        public virtual string JiraURL { get; set; }

        public Project DeepClone()
        {
            Project newProject = new Project();
            newProject.Name = this.Name+" (Copy)";
            newProject.Description = this.Description;
            newProject.JiraURL = this.JiraURL;
            newProject.TestCases = new List<TestCase>();
            foreach(var tcase in this.TestCases)
            {
                TestCase aux = tcase.DeepClone();
                newProject.TestCases.Add(aux);
            }            
            return newProject;
        }
    }
}