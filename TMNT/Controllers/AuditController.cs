using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TMNT.Models;
using TMNT.Models.Repository;
using TMNT.Models.ViewModels;

namespace TMNT.Controllers {
    public class AuditController : Controller {
        private UnitOfWork _uow;

        public AuditController(UnitOfWork uow) {
            _uow = uow;
        }
        public AuditController() : this(new UnitOfWork()) { }

        // GET: Audit
        [Route("Audit")]
        public ActionResult Index() {
            return View();
        }

        [HttpPost]
        [Route("Audit/PerformAudit")]
        public ActionResult PerformAudit([Bind(Include = "IdCode,Type")] PerformAuditViewModel model) {
            AuditViewModel auditViewModel;
            if (model.Type == "ws") {
                WorkingStandard workingStandard = _uow.WorkingStandardRepository.Get().Where(w => w.IdCode == model.IdCode).FirstOrDefault();
                if (workingStandard == null) {
                    return HttpNotFound();
                }

                auditViewModel = new AuditViewModel {
                    ChemType = "WorkingStandard",
                    Id = workingStandard.WorkingStandardId,
                    IdCode = workingStandard.IdCode
                };
                auditViewModel.Parents = GetAllParents(workingStandard.PrepList.PrepListItems);
            } else if (model.Type == "is") {
                IntermediateStandard intermediatestandard = _uow.IntermediateStandardRepository.Get().Where(i => i.IdCode == model.IdCode).FirstOrDefault();
                if (intermediatestandard == null) {
                    return HttpNotFound();
                }
                auditViewModel = new AuditViewModel {
                    ChemType = "IntermediateStandard",
                    Id = intermediatestandard.IntermediateStandardId,
                    IdCode = intermediatestandard.IdCode
                };
                auditViewModel.Parents = GetAllParents(intermediatestandard.PrepList.PrepListItems);
            } else {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(auditViewModel);
        }

        public List<AuditViewModel> GetAllParents(ICollection<PrepListItem> items) {
            List<AuditViewModel> auditList = new List<AuditViewModel>();
            foreach (PrepListItem item in items) {
                if (item.StockReagent != null) {
                    var reagent = item.StockReagent;
                    AuditViewModel reagentAudit = new AuditViewModel {

                        ChemType = "Reagent",
                        Name = reagent.ReagentName,
                        IdCode = reagent.IdCode,
                        Id = reagent.ReagentId
                    };
                    auditList.Add(reagentAudit);
                } else if (item.StockStandard != null) {
                    var standard = item.StockStandard;
                    AuditViewModel standardAudit = new AuditViewModel {
                        ChemType = "Standard",
                        Name = standard.StockStandardName,
                        IdCode = standard.IdCode,
                        Id = standard.StockStandardId
                    };
                    auditList.Add(standardAudit);
                } else if (item.IntermediateStandard != null) {
                    var intStandard = item.IntermediateStandard;
                    AuditViewModel intAudit = new AuditViewModel {
                        ChemType = "IntermediateStandard",
                        Name = intStandard.IntermediateStandardName,
                        IdCode = intStandard.IdCode,
                        Id = intStandard.IntermediateStandardId
                    };
                    if (intStandard.PrepList.PrepListItems.Count > 0) {
                        intAudit.Parents = GetAllParents(intStandard.PrepList.PrepListItems);
                    }
                    auditList.Add(intAudit);

                } else if (item.WorkingStandard != null) {
                    var workStandard = item.WorkingStandard;
                    AuditViewModel workAudit = new AuditViewModel {
                        ChemType = "WorkingStandard",
                        IdCode = workStandard.IdCode,
                        Name = workStandard.WorkingStandardName,
                        Id = workStandard.WorkingStandardId
                    };
                    if (workStandard.PrepList.PrepListItems.Count > 0) {
                        workAudit.Parents = GetAllParents(workStandard.PrepList.PrepListItems);
                    }
                    auditList.Add(workAudit);
                }
            }
            return auditList;
        }
    }
}