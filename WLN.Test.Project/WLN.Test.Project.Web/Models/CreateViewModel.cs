using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WLN.Test.Project.Web.Models
{
    public class CreateViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "{0} must be at least {2} symbols.", MinimumLength = 5)]
        [Display(Name = "Path with name")]
        public string Path { get; set; }
        public bool File { get; set; }
        public bool Directory { get; set; }
    }
}