using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WLN.Test.Project.Web.Models
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} must be at least {2} symbols.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password Confirmation")]
        [Compare("Password", ErrorMessage = "Password and it's confirmation does't equal.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Set as Administrator")]
        public bool SetAdmin { get; set; }
    }
}