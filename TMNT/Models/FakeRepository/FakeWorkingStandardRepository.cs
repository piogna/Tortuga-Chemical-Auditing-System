using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TMNT.Models.Repository;

namespace TMNT.Models.FakeRepository {
    public class FakeWorkingStandardRepository : IRepository<WorkingStandard> {
        //creating a list of WorkingStandard objects. we only need one to perform a fake audit.
        private List<WorkingStandard> _standards = new List<WorkingStandard>() {
            new WorkingStandard() {
                WorkingStandardId = 1,
                IdCode = "ORG-02-35790AA",
                Grade = 80.5,
                PreparationDate = new DateTime(2015, 04, 15),
                PrepList = new FakePrepListRepository().Get(1),
                Source = 1
            }
        };

        public IEnumerable<WorkingStandard> Get() {
            return _standards;
        }

        public WorkingStandard Get(int? i) {
            return _standards
                .Where(item => item.WorkingStandardId == i)
                .Select(item => item)
                .FirstOrDefault<WorkingStandard>();
        }

        public WorkingStandard Get(string id) {
            return _standards
                .Where(item => item.IdCode == id)
                .Select(item => item)
                .FirstOrDefault<WorkingStandard>();
        }

        public void Create(WorkingStandard t) {
            throw new NotImplementedException();
        }

        public void Update(WorkingStandard t) {
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