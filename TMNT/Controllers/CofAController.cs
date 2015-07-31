using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TMNT.Models;
using TMNT.Models.Repository;

namespace TMNT.Controllers {
    public class CofAController : Controller {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: CofA
        [Route("CofA")]
        public ActionResult Index() {
            return View();
        }

        [Route("CofA/{id?}")]
        public ActionResult Get(int? id) {
            CertificateOfAnalysis cofa = db.CertificatesOfAnalysis.Find(id);
            MemoryStream ms = new MemoryStream(cofa.Content, 0, 0, true, true);
            Response.ContentType = cofa.ContentType;
            Response.AddHeader("content-disposition", "attachment;filename=" + cofa.FileName);
            Response.Buffer = true;
            Response.Clear();
            Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length);
            Response.OutputStream.Flush();
            Response.End();
            return new FileStreamResult(Response.OutputStream, cofa.ContentType);
        }
    }
}