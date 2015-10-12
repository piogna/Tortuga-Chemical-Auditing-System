﻿using Microsoft.AspNet.Identity;
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
            //UserManager<ApplicationUser> manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            return UserManager.FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
        }

        public static Department GetUserDepartment() {
            //UserManager<ApplicationUser> manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var user = UserManager.FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            //UserManager.GetRoles(user.Id).ToString();
            return user.Department != null ? user.Department : null;
        }

        public static List<string> GetUserRoles() {
            var user = UserManager.FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            return UserManager.GetRoles(user.Id) as List<string>;
        }
    }
}