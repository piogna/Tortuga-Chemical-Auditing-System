using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using TMNT.Models.FakeModels;
using TMNT.Models.FakeRepository;

namespace TMNT.Api {
    /// <summary>
    /// Low Stock Standard
    /// </summary>
    public class LowStockStandardApiController : ApiController {
        FakeLowStockRepository repo = new FakeLowStockRepository();

        // GET api/FakeLowStockStandard
        /// <summary>
        /// Getting all low stock standards
        /// </summary>
        /// <returns></returns>
        [Route("get/all-low-stock-standards")]
        public List<FakeLowStockStandards> GetLowStockStandards() {
            return repo.Get().ToList();
        }

        /// <summary>
        /// Returning all low stock standards as a IHttpActionResult
        /// </summary>
        /// <returns></returns>
        [ResponseType(typeof(List<FakeLowStockStandards>))]
        public IHttpActionResult Get() {
            return Ok(GetLowStockStandards());
        }
    }
}
