using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TMNT.Models;
using TMNT.Models.Repository;
using TMNT.Models.ViewModels;

namespace TMNT.Controllers
{
    public class AuditController : Controller
    {
        private UnitOfWork _uow;

        public AuditController(UnitOfWork uow)
        {
            _uow = uow;
        }
        public AuditController() : this(new UnitOfWork()) { }

        // GET: Audit
        [Route("Audit")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("Audit/PerformAudit")]
        public ActionResult PerformAudit([Bind(Include="IdCode,Type")] PerformAuditViewModel model)
        {
            AuditViewModel auditViewModel = new AuditViewModel();
            auditViewModel.Items = new List<TMNT.Models.ViewModels.AuditViewModel.Item>();
            if (model.Type == "ws")
            {
                WorkingStandard workingStandard = _uow.WorkingStandardRepository.Get().Where(w => w.MaxxamId == model.IdCode).First();
                if (workingStandard == null)
                {
                    return HttpNotFound();
                }

                var vWorkingStandard = new WorkingStandardDetailsViewModel()
                {
                    WorkingStandardId = workingStandard.WorkingStandardId,
                    PrepList = workingStandard.PrepList,
                    PrepListItems = workingStandard.PrepList.PrepListItems.ToList(),
                    IdCode = workingStandard.IdCode,
                    MaxxamId = workingStandard.MaxxamId,
                    LastModifiedBy = workingStandard.LastModifiedBy,
                    Concentration = workingStandard.FinalConcentration,
                    ExpiryDate = workingStandard.ExpiryDate,
                    DateOpened = workingStandard.DateOpened,
                    DateCreated = workingStandard.DateCreated,
                    DateModified = workingStandard.DateModified,
                    CreatedBy = workingStandard.CreatedBy
                };

                foreach (var invItem in workingStandard.InventoryItems)
                {
                    if (invItem.WorkingStandard.WorkingStandardId == workingStandard.WorkingStandardId)
                    {
                        vWorkingStandard.Department = invItem.Department;
                        vWorkingStandard.UsedFor = invItem.UsedFor;
                        vWorkingStandard.IsExpired = invItem.WorkingStandard.ExpiryDate < DateTime.Today;
                        vWorkingStandard.IsExpiring = invItem.WorkingStandard.ExpiryDate < DateTime.Today.AddDays(30) && !(invItem.WorkingStandard.ExpiryDate < DateTime.Today);
                        vWorkingStandard.InitialAmount = invItem.InitialAmount;
                    }
                }
                auditViewModel.Items.Add(new TMNT.Models.ViewModels.AuditViewModel.Item {
                    Id = workingStandard.WorkingStandardId,
                    ChemType = "ws",
                    Level = 0
                });

                foreach (PrepListItem item in workingStandard.PrepList.PrepListItems)
                {
                    if (item.StockReagent != null || item.StockStandard != null)
                    {
                        //do stuff to handle adding base chems
                    }
                     else if(item.IntermediateStandard != null)
                    {
                         //handle int standards
                    }
                    else if (item.WorkingStandard != null)
                    {

                    }
                }
            }
            else if (model.Type == "is")
            {
                IntermediateStandard intermediatestandard = _uow.IntermediateStandardRepository.Get().Where(i => i.MaxxamId == model.IdCode).First();
                var vIntermediateStandard = new IntermediateStandardDetailsViewModel()
                {
                    IntermediateStandardId = intermediatestandard.IntermediateStandardId,
                    Replaces = intermediatestandard.Replaces,
                    ReplacedBy = intermediatestandard.ReplacedBy,
                    PrepList = intermediatestandard.PrepList,
                    PrepListItems = intermediatestandard.PrepList.PrepListItems.ToList(),
                    IdCode = intermediatestandard.IdCode,
                    MaxxamId = intermediatestandard.MaxxamId,
                    LastModifiedBy = intermediatestandard.LastModifiedBy,
                    Concentration = intermediatestandard.FinalConcentration,
                    ExpiryDate = intermediatestandard.ExpiryDate,
                    DateModified = intermediatestandard.DateModified,
                    DateOpened = intermediatestandard.DateOpened,
                    DateCreated = intermediatestandard.DateCreated,
                    CreatedBy = intermediatestandard.CreatedBy
                };

                foreach (var invItem in intermediatestandard.InventoryItems)
                {
                    if (invItem.IntermediateStandard.IntermediateStandardId == intermediatestandard.IntermediateStandardId)
                    {
                        vIntermediateStandard.Department = invItem.Department;
                        vIntermediateStandard.UsedFor = invItem.UsedFor;
                        vIntermediateStandard.IsExpired = invItem.IntermediateStandard.ExpiryDate < DateTime.Today;
                        vIntermediateStandard.IsExpiring = invItem.IntermediateStandard.ExpiryDate < DateTime.Today.AddDays(30) && !(invItem.IntermediateStandard.ExpiryDate < DateTime.Today);
                        vIntermediateStandard.InitialAmount = invItem.InitialAmount;
                    }
                }
                auditViewModel.IntermediateStandards.Add(new Tuple<IntermediateStandardDetailsViewModel, int>(vIntermediateStandard, 0));
            }
            return View();
        }
    }
}