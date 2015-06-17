using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TMNT.Api;
using TMNT.Models.ViewModels;

namespace TMNT.Controllers {
    public class AuditController : Controller {
        // GET: Audit
        [Route("start/new-audit")]
        public ActionResult Index() {
            return View();
        }

        // POST: PerformAudit
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("audit-results")]
        public ActionResult PerformAudit(string id) {
            if (id != null) {
                var model = new AuditApiController().Get(id.ToUpper());

                if (model == null) {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                //properties to be added in the beginning
                AuditViewModel audit = new AuditViewModel() {
                    /* working standard model properties */
                    WorkingStandardId = model.WorkingStandardId,
                    PreparationDate = model.PreparationDate,
                    Source = model.Source,
                    Grade = model.Grade,
                    IdCode = model.IdCode,
                    /* prep list model properties */
                    PrepListId = model.PrepList.PrepListId,
                    PrepListItems = model.PrepList.PrepListItems
                };

                //properties to be added through loops
                foreach (var item in model.PrepList.PrepListItems) {
                    audit.Amount = item.Amount;
                }

                return View(audit);
            }
            return View();
        }
    }
}