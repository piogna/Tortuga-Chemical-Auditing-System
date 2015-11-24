using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TMNT.Filters;
using TMNT.Models;
using TMNT.Models.Enums;
using TMNT.Models.Repository;
using TMNT.Models.ViewModels;
using TMNT.Utils;

namespace TMNT.Controllers {
    [Authorize]
    [PasswordChange]
    public class VolumetricController : Controller {

        private UnitOfWork _uow;
        public VolumetricController()
            : this(new UnitOfWork()) {
        }

        public VolumetricController(UnitOfWork uow) {
            this._uow = uow;
        }

        // GET: Volumetric
        [Route("Volumetrics")]
        public ActionResult Index() {
            var department = _uow.GetUserDepartment();

            var volumetrics = _uow.VolumetricDeviceRepository.Get().Where(item => item.Department.Equals(department));
            var viewModels = new List<VolumetricIndexViewModel>();

            foreach (var item in volumetrics) {
                viewModels.Add(new VolumetricIndexViewModel() {
                    VolumetricId = item.DeviceId,
                    DeviceCode = item.DeviceCode,
                    IsVerified = item.IsVerified,
                    Department = item.Department,
                    LastVerifiedBy = item.DeviceVerifications//last verified by
                                .Where(x => x.Device.Equals(item))
                                .Count() == 0 ?
                                    null :
                                    item.DeviceVerifications
                                        .Where(x => x.Device.Equals(item))
                                        .OrderBy(x => x.VerifiedOn)
                                        .Select(x => x.User.UserName)
                                        .First()
                });
            }
            this.Dispose();
            return View(viewModels);
        }

        // GET: Volumetric/Details/5
        public ActionResult Details(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Device device = _uow.VolumetricDeviceRepository.Get(id);
            if (device == null) {
                return HttpNotFound();
            }
            this.Dispose();
            return View(device);
        }

        // GET: Volumetric/Create
        [Route("Volumetric/Create")]
        public ActionResult Create() {
            return View(SetCreateVolumetric(new VolumetricCreateViewModel()));
        }

        // POST: Volumetric/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Volumetric/Create")]
        public ActionResult Create([Bind(Include = "DeviceId,DeviceCode,IsVerified,DeviceType,Status")] Device device) {
            if (ModelState.IsValid) {
                _uow.VolumetricDeviceRepository.Create(device);
                this.Dispose();
                return RedirectToAction("Index");
            }
            this.Dispose();
            return View(device);
        }

        [Route("Volumetric/Verification/{id?}")]
        public ActionResult Verification(int? id) {
            //sending all Locations to the view
            var locations = _uow.LocationRepository.Get().Select(name => name.LocationName).ToList();
            var balance = _uow.VolumetricDeviceRepository.Get(id);

            var device = new VolumetricVerificationViewModel() {
                VolumetricId = balance.DeviceId,
                Location = balance.Department.Location,
                DeviceCode = balance.DeviceCode,
                CurrentLocation = balance.Department.Location.LocationName,
                LocationNames = locations,
                NumberOfTestsToVerify = balance.NumberOfTestsToVerify,
                WeightLimitOne = balance.AmountLimitOne + " g",
                WeightLimitTwo = balance.AmountLimitTwo + " g ",
                WeightLimitThree = balance.AmountLimitThree == null ? null : balance.AmountLimitThree + " g",
                NumberOfDecimals = balance.NumberOfDecimals
            };
            this.Dispose();
            return View(device);
        }

        // GET: Volumetric/Edit/5
        public ActionResult Edit(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Device device = _uow.VolumetricDeviceRepository.Get(id);
            if (device == null) {
                return HttpNotFound();
            }
            this.Dispose();
            return View(device);
        }

        // POST: Volumetric/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DeviceId,DeviceCode,IsVerified,DeviceType,Status")] Device device) {
            var result = CheckModelState.Invalid;
            if (ModelState.IsValid) {
                _uow.VolumetricDeviceRepository.Update(device);
                result = _uow.Commit();

                //TODO cases
                switch (result) {
                    case CheckModelState.UpdateError:
                        break;
                    case CheckModelState.ConcurrencyError:
                        break;
                    case CheckModelState.Invalid:
                        break;
                    case CheckModelState.Disposed:
                        break;
                    case CheckModelState.Valid:
                        return RedirectToAction("Index");
                    case CheckModelState.DataError:
                        break;
                    case CheckModelState.Error:
                        break;
                    default:
                        break;
                }
            }
            this.Dispose();
            return View(device);
        }

        // GET: Volumetric/Delete/5
        public ActionResult Delete(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var device = _uow.VolumetricDeviceRepository.Get(id);
            if (device == null) {
                return HttpNotFound();
            }
            _uow.VolumetricDeviceRepository.Delete(id);

            var result = _uow.Commit();

            this.Dispose();
            return View(device);
        }

        // POST: Volumetric/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            _uow.VolumetricDeviceRepository.Delete(id);
            var result = _uow.Commit();

            return RedirectToAction("Index");
        }

        private VolumetricCreateViewModel SetCreateVolumetric(VolumetricCreateViewModel model) {
            var locations = _uow.LocationRepository.Get();
            var departments = _uow.DepartmentRepository.Get();

            model.LocationNames = locations.Select(item => item.LocationName).ToList();
            model.Departments = departments
                .Where(item => !item.DepartmentName.Equals("Quality Assurance"))
                .GroupBy(item => item.DepartmentName)
                .Select(item => item.First()).ToList();
            model.SubDepartments = departments
                .Where(item => !string.IsNullOrEmpty(item.SubDepartment) || !item.DepartmentName.Equals("Quality Assurance"))
                .ToList();

            this.Dispose();
            return model;
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                _uow.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
