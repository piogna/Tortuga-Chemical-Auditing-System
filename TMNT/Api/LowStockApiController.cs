using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using TMNT.Models;
using TMNT.Models.FakeModels;
using TMNT.Models.FakeRepository;
using TMNT.Models.Repository;

namespace TMNT.Api {
    /// <summary>
    /// Low Stock Standard
    /// </summary>
    public class LowStockApiController : ApiController {
        private IRepository<InventoryItem> repo;
        public LowStockApiController() : this(new LowStockRepository()) { }

        public LowStockApiController(IRepository<InventoryItem> repo)
        {
            this.repo = repo;
        }

        // GET api/FakeLowStockStandard
        /// <summary>
        /// Getting all low stock standards
        /// </summary>
        /// <returns></returns>
        [Route("api/lowstock")]
        [ResponseType(typeof(List<InventoryItem>))]
        public IHttpActionResult Get()
        {
            return Ok(repo.Get());
        }
    }
}
