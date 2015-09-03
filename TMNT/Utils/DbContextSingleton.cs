using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using TMNT.Models;

namespace TMNT.Utils {
    public class DbContextSingleton {

        private static volatile ApplicationDbContext instance;
        private ApplicationUserManager _userManager;
        private static object syncRoot = new object();

        public DbContextSingleton() {  }

        public static ApplicationDbContext Instance {
            get {
                if (instance == null) {
                    lock (syncRoot) {
                        if (instance == null)
                            instance = new ApplicationDbContext();
                    }
                }
                return instance;
            }
        }
    }
}