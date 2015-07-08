using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TMNT.Models;
using TMNT.Models.Repository;

namespace TMNT.Api
{
    public class StandardApiController : ApiController
    {
        private IApiRepository<StockStandard> repoStandard;
        public StandardApiController(IApiRepository<StockStandard> repo) 
        {
            repoStandard = repo;
        }

        public StandardApiController()
            : this(new StockStandardApiRepository()) {
        }

        public IHttpActionResult Get(string? idCode)
        {
            StockStandard standard = repoStandard.Get(idCode.ToString());
            if (standard == null)
            {
                return NotFound();
            }
            return Ok(standard);
        }
    }
}
