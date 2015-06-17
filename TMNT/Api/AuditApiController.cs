using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Web.Http;
using System.Web.Http.Description;
using TMNT.Models;
using TMNT.Models.FakeRepository;
using TMNT.Models.Repository;

namespace TMNT.Api {
    /// <summary>
    /// Audit
    /// </summary>
    public class AuditApiController : ApiController {
        FakeWorkingStandardRepository workingRepo = new FakeWorkingStandardRepository();

        private IRepository<WorkingStandard> repo;

        public AuditApiController() : this(new WorkingStandardRepository()) {

        }

        public AuditApiController(IRepository<WorkingStandard> repo) {
            this.repo = repo;
        }


        // GET api/auditapi
        /// <summary>
        /// Get all working standards
        /// </summary>
        /// <returns></returns>
        public IEnumerable<WorkingStandard> Get() {
            return workingRepo.Get();
        }

        // GET api/auditapi/IDCODE
        /// <summary>
        /// Performs an audit based on a given IdCode
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(WorkingStandard))]
        public WorkingStandard Get(string id) {
            //string s = repo.Get().Where(item => id.Trim() == item.IdCode.Trim()).Select(item => item.IdCode).FirstOrDefault();

            var v = workingRepo.Get()
                .Where(item => item.IdCode == id)
                .FirstOrDefault<WorkingStandard>();
            return v;
        }

        // POST api/test
        /// <summary>
        /// Not implemented
        /// </summary>
        /// <param name="value"></param>
        public void Post([FromBody]string value) {
            throw new NotImplementedException();
        }

        // PUT api/test/5
        /// <summary>
        /// Not implemented
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        public void Put(int id, [FromBody]string value) {
            throw new NotImplementedException();
        }

        // DELETE api/test/5
        /// <summary>
        /// Not implemented
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id) {
            throw new NotImplementedException();
        }
    }
}
