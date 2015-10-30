using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using TMNT.Models;
using TMNT.Models.Repository;
using TMNT.Utils;

namespace TMNT.Api {
    public class ReportsDevicesApiController : ApiController {
        ApplicationDbContext db = DbContextSingleton.Instance;//ApplicationDbContext.Create();
        //private IRepository<Device> _repo;
        public ReportsDevicesApiController() {
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;
        }

        //public ReportsDevicesApiController() : this(new DeviceRepository(DbContextSingleton.Instance)) { }

        //public ReportsDevicesApiController(IRepository<Device> repo) {
        //    _repo = repo;
        //}

        //GET: All
        [ResponseType(typeof(Device))]
        public IHttpActionResult GetDevices() {
            List<Device> devices = db.Devices.ToList();
            List<Department> departments = db.Departments.ToList();
            foreach (var device in devices) {
                device.DeviceVerifications = db.DeviceVerifications.Where(item => item.Device.DeviceId == device.DeviceId).ToList();
                foreach (var department in departments) {
                    device.Department = db.Departments.Where(item => item.DepartmentId == department.DepartmentId).First();
                }
            }

            if (devices == null) {
                return NotFound();
            }
            try {
                return Ok(devices);
            } catch (Exception ex) {

            }
            return null;
        }

        // GET: api/ReportsDevicesApi/5
        //[ResponseType(typeof(Device))]
        //public IHttpActionResult GetDevice(int? id) {
        //    Device device = db.Devices.Find(id);
        //    if (device == null) {
        //        return NotFound();
        //    }

        //    return Ok(device);
        //}

        //// PUT: api/ReportsDevicesApi/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutDevice(int id, Device device) {
        //    if (!ModelState.IsValid) {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != device.DeviceId) {
        //        return BadRequest();
        //    }


        //    try {
        //        db.Devices.
        //    } catch (DbUpdateConcurrencyException) {
        //        if (!DeviceExists(id)) {
        //            return NotFound();
        //        } else {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        //// POST: api/ReportsDevicesApi
        //[ResponseType(typeof(Device))]
        //public IHttpActionResult PostDevice(Device device) {
        //    if (!ModelState.IsValid) {
        //        return BadRequest(ModelState);
        //    }

        //    _repo.Create(device);

        //    return CreatedAtRoute("DefaultApi", new { id = device.DeviceId }, device);
        //}

        //// DELETE: api/ReportsDevicesApi/5
        //[ResponseType(typeof(Device))]
        //public IHttpActionResult DeleteDevice(int? id) {
        //    Device device = _repo.Get(id);
        //    if (device == null) {
        //        return NotFound();
        //    }

        //    _repo.Delete(id);

        //    return Ok(device);
        //}

        ////protected override void Dispose(bool disposing) {
        ////    if (disposing) {
        ////        db.Dispose();
        ////    }
        ////    base.Dispose(disposing);
        ////}

        //private bool DeviceExists(int? id) {
        //    return _repo.Get().Count(e => e.DeviceId == id) > 0;
        //}
    }
}