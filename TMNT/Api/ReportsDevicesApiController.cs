using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using TMNT.Models;
using TMNT.Models.Repository;
using TMNT.Utils;

namespace TMNT.Api {
    public class ReportsDevicesApiController : ApiController {
        ApplicationDbContext db = ApplicationDbContext.Create();
        private UnitOfWork _uow;

        public ReportsDevicesApiController(UnitOfWork uow) {
            _uow = uow;
        }
        public ReportsDevicesApiController() : this(new UnitOfWork()) {
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;
        }

        //GET: All
        [ResponseType(typeof(Device))]
        public IHttpActionResult GetDevices() {
            var devices = db.Devices;
            var userDepartment = _uow.GetUserDepartment();

            var apidevices = devices
                .Where(item => !item.IsArchived && item.Department.DepartmentId == userDepartment.DepartmentId)
                .Select(item => new BalanceApiModel() {
                    BalanceId = item.DeviceId,
                    Department = item.Department,
                    DeviceCode = item.DeviceCode,
                    IsVerified = item.IsVerified,
                    DeviceVerifications = item.DeviceVerifications.Where(v => v.VerifiedOn == DateTime.Today),
                    LastVerifiedBy = item.DeviceVerifications.Count > 0 ?
                                    item.DeviceVerifications.OrderByDescending(v => v.VerifiedOn).FirstOrDefault().User.UserName :
                                    "New Device"
                }).ToList();

            if (apidevices == null) {
                return NotFound();
            }
            try {
                return Ok(apidevices);
            } catch (Exception ex) {

            }
            return null;
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                db.Dispose();
                _uow.Dispose();
            }
            base.Dispose(disposing);
        }
    }

    public class BalanceApiModel {
        public int BalanceId { get; set; }
        public string DeviceCode { get; set; }
        public Location Location { get; set; }
        public Department Department { get; set; }
        public bool IsVerified { get; set; }
        public string Status { get; set; }
        public int NumberOfDecimals { get; set; }

        public DateTime? LastVerified { get; set; }
        public string LastVerifiedBy { get; set; }//full name

        public IEnumerable<DeviceVerification> DeviceVerifications { get; set; }
    }
}