using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JZappV3.Models
{
    public class Job
    {
        [Required]
        public int JobId { get; set; }
        [Required]
        [Display(Name = "Job Type")]
        public String JobType { get; set; }
        [Required]
        public String Description { get; set; }
        [Required]

        [Display(Name = "Start Date and Time")]
        public DateTime StartDateTime { get; set; }
        [Required]
        [Display(Name = "Finish Date and Time")]
        public DateTime FinishDateTime { get; set; }
        [Display(Name = "Posted By")]
        public String PostedBy { get; set;}
        [Display(Name = "Commited By")]
        public String CommitedBy { get; set; }
    
    }
    
}