using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using TMNT.Models;
using System;
using TMNT.Models.Repository;
using System.Net.Mail;
using System.Net;

namespace TMNT.Controllers {
    [Authorize]
    public class AccountController : Controller {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController() {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager) {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager {
            get {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager {
            get {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set {
                _userManager = value;
            }
        }

        //
        // GET: /Account/Login
        [Route("Account/Login")]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl) {
            ViewBag.ReturnUrl = returnUrl;
            string username = "";

            if (Request.Cookies["Username"] != null) {
                username = Request.Cookies["Username"].Value;
            }

            ViewBag.Username = username;

            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [Route("Account/Login")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl) {
            if (!ModelState.IsValid) {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, shouldLockout: true);
            UserManager.MaxFailedAccessAttemptsBeforeLockout = 3;
            
            var user = UserManager.FindByName(model.UserName);

            if (user == null) {
                ModelState.AddModelError("", "The username entered does not exist");
                return View(model);
            }

            int count = await UserManager.GetAccessFailedCountAsync(user.Id);

            switch (result) {
                case SignInStatus.Success:
                    //if first-time login, force them to change their password
                    if (user.IsFirstTimeLogin) {
                        return RedirectToAction("ChangePasswordFirstTime", "Manage");
                    }

                    //ViewBag.User = DbContextSingleton.Instance.Users.FirstOrDefault(x => x.Id == user).Department;
                    //setting the "Remember Me?" checkbox
                    if (model.RememberMe) {
                        // is authenticated so save cookie
                        Response.Cookies["Username"].Value = model.UserName;
                        Response.Cookies["Username"].Expires = DateTime.Now.AddDays(5);
                    } else {
                        // remove cookie by forcing it to expire immediately
                        Response.Cookies["Username"].Expires = DateTime.Now.AddDays(-1);
                    }
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                    if (count == UserManager.MaxFailedAccessAttemptsBeforeLockout) {
                        await UserManager.SetLockoutEnabledAsync(user.Id, true);
                    }
                    ModelState.AddModelError("", "Your username or password was incorrect");
                    return View(model);
                default:
                    ModelState.AddModelError("", "Your username or password was incorrect");
                    return View(model);
            }
        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe) {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync()) {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model) {
            if (!ModelState.IsValid) {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result) {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

        //
        // GET: /Account/Register
        [Route("Account/Register")]
        [AllowAnonymous]
        public ActionResult Register() {
            return View();
        }

        //
        // POST: /Account/Register
        [Route("Account/Register")]
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model, string submit) {
            if (ModelState.IsValid) {
                var locationRepo = new LocationRepository();
                
                var location = locationRepo.Get()
                    .Where(item => item.LocationName.Equals(model.Location.LocationName))
                    .First();

                model.Password = "!Maxxam123";
                model.IsFirstTimeLogin = true;
                var user = new ApplicationUser { UserName = model.UserName, Email = model.Email, FirstName = model.FirstName, LastName = model.LastName, LastPasswordChange = DateTime.Today, NextRequiredPasswordChange = DateTime.Today.AddYears(1), IsFirstTimeLogin = model.IsFirstTimeLogin };
                var result = await UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded) {
                    UserManager.AddToRole(user.Id, model.Role);
                    using (ApplicationDbContext db = ApplicationDbContext.Create()) {
                        var updateUser = db.Users.Where(item => item.Id.Equals(user.Id)).First();
                        Department department = db.Departments
                            .Where(item => item.DepartmentName.Equals(model.Department.DepartmentName) && item.SubDepartment.Equals(model.Department.SubDepartment))
                            .First();

                        updateUser.Department = department;

                        db.Entry(updateUser).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }

                    //send email to new user with username and default password
                    string username = "team.tortuga.contact";
                    string password = "tortugarules";
                    try {
                        using (MailMessage message = new MailMessage("admin@maxxam.ca", model.Email, "Maxxam New Account - " + model.FirstName, "")) {
                            message.Body = "Welcome to Maxxam, " + model.FirstName + " " + model.LastName +  "!<br/><br/>" +
                            "Click the link below to login to your account with the following credentials. You will be asked to change your password immediately (minimum 8 characters).<br/><br/>" +
                            "Username: " + model.UserName + "<br/>" +
                            "Password: " + model.Password + "<br/><br/>" +
                            "http://142.55.49.127";

                            message.IsBodyHtml = true;

                            using (SmtpClient client = new SmtpClient("smtp.gmail.com", 587)) {
                                client.UseDefaultCredentials = false;
                                client.Credentials = new NetworkCredential(username, password);
                                client.EnableSsl = true;

                                client.Send(message);
                            }
                        }
                    } catch (Exception ex) {

                    }
                    
                    //UserManager.AddToRole(model.UserName, model.Role);//breaking the application
                    //await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");
                    //return View();

                    if (!string.IsNullOrEmpty(submit) && submit.Equals("Create")) {
                        //save pressed
                        return RedirectToAction("Index", "Home");
                    } else {
                        //save & new pressed
                        return RedirectToAction("Register");
                    }
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code) {
            if (userId == null || code == null) {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword() {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model) {
            if (ModelState.IsValid) {
                var user = await UserManager.FindByNameAsync(model.UserName);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id))) {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                // return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation() {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [Route("Account/ResetPassword")]
        [AllowAnonymous]
        public ActionResult ResetPassword(string code) {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [Route("Account/ResetPassword")]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model) {
            if (!ModelState.IsValid) {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Username);
            if (user == null) {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded) {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation() {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl) {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe) {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null) {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model) {
            if (!ModelState.IsValid) {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider)) {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl) {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null) {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result) {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl) {
            if (User.Identity.IsAuthenticated) {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid) {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null) {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded) {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded) {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        [Route("Account/LogOff")]
        public ActionResult LogOff() {
            Session.Abandon();
            AuthenticationManager.SignOut();
            return RedirectToAction("Login", "Account");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure() {
            return View();
        }

        //
        // GET: /Account/ViewProfile
        [AllowAnonymous]
        [Route("Account/ViewProfile/{id}")]
        public ActionResult ViewProfile(string id) {
            //have to do this
            var user = ApplicationDbContext.Create().Users.Where(item => item.UserName.Equals(id)).First();
            ProfileViewModel model = new ProfileViewModel() {
                Department = user.Department,
                Location = user.Department.Location,
                Username = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Name = user.FirstName + " " + user.LastName,
                Role = Helpers.HelperMethods.GetUserRoles().First(),
                LastPasswordChange = user.LastPasswordChange,
                NextRequiredPasswordChange = user.NextRequiredPasswordChange
            };
            if (model.LastPasswordChange != null) {
                model.DaysUntilNextPasswordChange = (model.NextRequiredPasswordChange.Value - DateTime.Today).Days - 1;
            }
            return View(model);
        }

        [Route("AccessDenied")]
        public ActionResult InsufficientPrivileges() {
            return View();
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                if (_userManager != null) {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null) {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager {
            get {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result) {
            foreach (var error in result.Errors) {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl) {
            if (Url.IsLocalUrl(returnUrl)) {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null) {
            }

            public ChallengeResult(string provider, string redirectUri, string userId) {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context) {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null) {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}