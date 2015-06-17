using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TMNT.Models.Repository;

namespace TMNT.Models.FakeRepository {
    public class FakeIntermediateStandardRepository : IRepository<IntermediateStandard> {
        private List<IntermediateStandard> _standards = new List<IntermediateStandard>() {
            new IntermediateStandard() { 
                IntermediateStandardId = 9,
                IdCode = "ORG-01-35790AZ",
                DateCreated = new DateTime(2015, 04, 02),
                DiscardDate = new DateTime(2015, 07, 05),
                Replaces = 8,//replaces an intermediate standard to use
                ReplacedBy = 9,//a number to signify the intermediate standard that replaces this one
                PrepList = null//new FakePrepListRepository().Get(2)//new PrepList()
            }
        };

        public IEnumerable<IntermediateStandard> Get() {
            return _standards;
        }

        public IntermediateStandard Get(int? i) {
            return _standards
                .Where(item => item.IntermediateStandardId == i)
                .Select(item => item)
                .FirstOrDefault<IntermediateStandard>();
        }

        public void Create(IntermediateStandard t) {
            throw new NotImplementedException();
        }

        public void Update(IntermediateStandard t) {
            throw new NotImplementedException();
        }

        public void Delete(int? i) {
            throw new NotImplementedException();
        }

        public void Dispose() {
            throw new NotImplementedException();
        }
    }
}