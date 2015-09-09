using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using TMNT.Models;
using TMNT.Models.Repository;
using TMNT.Models.ViewModels;

namespace TMNT.Api {
    public class ReportsDevicesApiController : ApiController {
        ApplicationDbContext db = ApplicationDbContext.Create();
        //public ReportsDevicesApiController() { }

        //private IApiRepository<Device> _repo;
        //public ReportsDevicesApiController(IApiRepository<Device> repo) {
        //    _repo = repo;
        //}

        //GET: All
        [ResponseType(typeof(Device))]
        public IHttpActionResult GetDevices() {
            List<Device> devices = db.Devices.ToList();
            //List<Device> deviceDataToSend = new List<Device>();

            //foreach (var item in devices) {
            //    deviceDataToSend.Add(new Device() {
            //        //Department = item.Department,
            //        DeviceCode = item.DeviceCode,
            //        DeviceType = item.DeviceType,
            //        Status = item.Status,
            //        IsVerified = item.IsVerified,
            //        DeviceVerifications = item.DeviceVerifications.ToList()
            //    });
            //}

            if (devices == null) {
                return NotFound();
            }

            return Ok(devices);
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