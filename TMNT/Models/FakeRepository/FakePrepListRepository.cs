using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TMNT.Models.Repository;

namespace TMNT.Models.FakeRepository {
    public class FakePrepListRepository : IRepository<PrepList> {
        /*
         * creating a list of PrepList objects. we only need one object for this.
         * it has an ID of 1
         * it finds two PrepListItems - whichever ones have an ID of 1 or/and 2
         * it is not built with an intermediate standard or working standard, so they are null
         */
        private List<PrepList> _lists = new List<PrepList>() {
            new PrepList () {
                PrepListId = 1,
                PrepListItems = new FakePrepListItemRepository()
                                    .Get()
                                    .Where(item => item.PrepListItemId < 4)
                                    .ToList(),
                IntermediateStandards = null,
                WorkingStandards = null
            },
             new PrepList () {
                PrepListId = 2,
                PrepListItems = new FakePrepListItemRepository()
                                    .Get()
                                    .Where(item => item.PrepListItemId > 3)
                                    .ToList(),
                IntermediateStandards = null,
                WorkingStandards = null
            }
        };

        public IEnumerable<PrepList> Get() {
            return _lists;
        }

        public PrepList Get(int? i) {
            return _lists
                .Where(item => item.PrepListId == i)
                .Select(item => item)
                .FirstOrDefault<PrepList>();
        }

        public void Create(PrepList t) {
            throw new NotImplementedException();
        }

        public void Update(PrepList t) {
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