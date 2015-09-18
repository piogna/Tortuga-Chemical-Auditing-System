using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using TMNT.Models;
using TMNT.Utils;


namespace TMNT.Helpers {
    public static class Helpers {
        public static Department GetUserDepartment() {
            ApplicationDbContext db = DbContextSingleton.Instance;
            UserManager<ApplicationUser> manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var user = manager.FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            return user.Department != null ? user.Department : null;
        }
    }
}