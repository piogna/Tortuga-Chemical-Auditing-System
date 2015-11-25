using System.Net;
using System.Web.Mvc;
using TMNT.Filters;
using TMNT.Models;
using TMNT.Models.Repository;

namespace TMNT.Controllers {
    [Authorize]
    [PasswordChange]
    public class PDFViewerController : Controller {

        private UnitOfWork _uow;

        public PDFViewerController()
            : this(new UnitOfWork()) {

        }

        public PDFViewerController(UnitOfWork uow) {
            _uow = uow;
        }

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
                    cofa = _uow.CofARepository.Get(id);
                    fileBuffer = cofa.Content;

                    if (fileBuffer != null) {
                        Response.ContentType = cofa.ContentType;
                        Response.AddHeader("content-length", fileBuffer.Length.ToString());
                        Response.BinaryWrite(fileBuffer);
                    }

                    break;
                case "MSDS":
                    msds = _uow.MSDSRepository.Get(id);
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