using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ServiceControl.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "*Hello")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "*Hi_Password")]
        public string Password { get; set; }
    }

    public class RegistrationModel
    {
        [Required(ErrorMessage = "*")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "*")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Required(ErrorMessage = "*")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        [DisplayName("Confirm password")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "*")]
        public string Name { get; set; }

        [Required(ErrorMessage = "*")]
        public string RoleName { get; set; }

        [Required(ErrorMessage = "*")]
        public string UserID { get; set; }
    }
}