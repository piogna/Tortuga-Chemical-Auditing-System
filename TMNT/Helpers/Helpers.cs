using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using TMNT.Models;


namespace TMNT.Helpers
{
    public class Helpers
    {
        public static string GetDepartmentCode()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            UserManager<ApplicationUser> manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var user = manager.FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            return (user.Department != null) ? user.Department.DepartmentCode : "No Department";
        }
    }
}