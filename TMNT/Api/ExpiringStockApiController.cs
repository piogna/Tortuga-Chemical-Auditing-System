using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using TMNT.Models.FakeRepository;

namespace TMNT.Api {
    /// <summary>
    /// Expiring Stock Standards
    /// </summary>
    public class ExpiringStockApiController : ApiController {
        FakeExpiringStandardsRepository repo = new FakeExpiringStandardsRepository();

        // GET api/FakeDeviceTest
        /// <summary>
        /// Getting all expiring stock standards
        /// </summary>
        /// <returns></returns>
        [ResponseType(typeof(FakeExpiringStandards))]
        public IHttpActionResult Get() {
            return Ok(repo.Get()
                .Select(item => item)
                .ToList());
        }
    }
}
