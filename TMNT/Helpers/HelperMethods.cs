using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using TMNT.Models;
using TMNT.Utils;


namespace TMNT.Helpers {
    public abstract class HelperMethods {
        private static ApplicationDbContext db = DbContextSingleton.Instance;
        private static UserManager<ApplicationUser> usrManager;


        private static UserManager<ApplicationUser> UserManager {
            get {
                return usrManager ?? new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            } set {
                usrManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            }
        }

        public static ApplicationUser GetCurrentUser() {
            return UserManager.FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
        }

        public static Department GetUserDepartment() {
            var user = UserManager.FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            return user.Department != null ? user.Department : null;
        }

        public static List<string> GetUserRoles() {
            var user = UserManager.FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            return UserManager.GetRoles(user.Id) as List<string>;
        }
    }
}