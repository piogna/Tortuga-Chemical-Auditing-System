using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using TMNT.Models.FakeModels;
using TMNT.Models.FakeRepository;

namespace TMNT.Api {
    /// <summary>
    /// Device Tests
    /// </summary>
    public class DeviceTestApiController : ApiController {
        FakeDeviceTestRepository repo = new FakeDeviceTestRepository();

        
        [Route("get/all-upcoming-device-tests")]
        public List<FakeDeviceTest> GetDeviceTests() {
            return repo.Get().ToList();
        }
        // GET api/FakeDeviceTest
        /// <summary>
        /// Getting all upcoming device tests
        /// </summary>
        /// <returns></returns>
        [ResponseType(typeof(FakeDeviceTest))]
        public IHttpActionResult Get() {
            return Ok(GetDeviceTests());
        }
    }
}
