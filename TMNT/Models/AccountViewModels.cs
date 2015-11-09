using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TMNT.Models {
    public class ExternalLoginConfirmationViewModel {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel {
        [Required]
        [MinLength(6, ErrorMessage = "{0} must be at least 6 characters long.")]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "Your {0} must be at least 8 characters long.")]
        [DataType(DataType.Password)]
        [Display(Name = "password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class ProfileViewModel {
        public string Username { get; set; }
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        public Department Department { get; set; }
        public Location Location { get; set; }
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        public string Biography { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public DateTime? LastPasswordChange { get; set; }
        public DateTime? NextRequiredPasswordChange { get; set; }
        public int DaysUntilNextPasswordChange { get; set; }
    }

    public class RegisterViewModel {
        [Required]
        [MinLength(6)]
        [Display(Name = "Username")]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Department")]
        public string DepartmentName { get; set; }
        [Required]
        [Display(Name = "Sub Department")]
        public string SubDepartment { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Role")]
        public string Role { get; set; }
        [Display(Name = "Location")]
        public string LocationName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        //properties to help populate the view
        public bool IsFirstTimeLogin { get; set; }
        public List<string> LocationNames { get; set; }// = new List<string>() { "Burnaby", "Campobello" };
        public List<Department> Departments { get; set; }// = new List<string>() { "Organics", "Inorganics" };
        public List<Department> SubDepartments { get; set; }// = new List<string>() { "Volatiles", "Semi-Volatiles", "Hydropcarbons", "Metals", "Wet Chemistry", "Autoanalyzer" };
        public List<string> RoleNames = new List<string>() { "Administrator", "Department Head", "Manager", "Supervisor", "Analyst", "Quality Assurance" };

        //[DataType(DataType.Password)]
        //[Display(Name = "Confirm password")]
        //[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        //public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel {
        //[Required]
        //[EmailAddress]
        //[Display(Name = "Email")]
        //public string Email { get; set; }

        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel {
        [Required]
        [MinLength(6)]
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}
