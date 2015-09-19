using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using TMNT.Models;
using TMNT.Utils;


namespace TMNT.Helpers {
    public abstract class HelperMethods {
        private static ApplicationDbContext db = DbContextSingleton.Instance;

        public static Department GetUserDepartment() {
            UserManager<ApplicationUser> manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var user = manager.FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            return user.Department != null ? user.Department : null;
        }

        public static ApplicationUser GetCurrentUser() {
            UserManager<ApplicationUser> manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            return manager.FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
        }
    }
}