using System.Net;
using System.Web.Mvc;
using TMNT.Models;
using TMNT.Models.Repository;

namespace TMNT.Controllers {
    public class PDFViewerController : Controller {
        // GET: PDFViewer
        public ActionResult Index() {
            return View();
        }

        [Route("ViewPDF/{id?}")]
        public ActionResult ViewPDF(int? id, string type) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            CertificateOfAnalysis cofa = null;
            MSDS msds = null;
            byte[] fileBuffer = null;

            switch (type) {
                case "CofA":
                    cofa = new CofARepository().Get(id);
                    fileBuffer = cofa.Content;

                    if (fileBuffer != null) {
                        Response.ContentType = cofa.ContentType;
                        Response.AddHeader("content-length", fileBuffer.Length.ToString());
                        Response.BinaryWrite(fileBuffer);
                    }

                    break;
                case "MSDS":
                    msds = new MSDSRepository().Get(id);
                    fileBuffer = msds.Content;

                    if (fileBuffer != null) {
                        Response.ContentType = msds.ContentType;
                        Response.AddHeader("content-length", fileBuffer.Length.ToString());
                        Response.BinaryWrite(fileBuffer);
                    }

                    break;
                default:
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
            return View();
        }
    }
}