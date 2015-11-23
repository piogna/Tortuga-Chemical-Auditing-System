using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using TMNT.Models.Enums;
using TMNT.Utils;

namespace TMNT.Models.Repository {
    public class PrepListRepository : IRepository<PrepList> {
        private ApplicationDbContext _db;

        public PrepListRepository() { }

        public PrepListRepository(ApplicationDbContext db) {
            _db = db;
        }

        public IEnumerable<PrepList> Get() {
            return _db.PrepLists.ToList();
        }

        public PrepList Get(int? i) {
            return _db.PrepLists.Find(i);
        }

        public void Create(PrepList t) {
            _db.PrepLists.Add(t);
        }

        public void Update(PrepList t) {
            throw new NotImplementedException();
        }

        public void Delete(int? i) {
            throw new NotImplementedException();
        }

        public void Dispose() {
            _db.Dispose();
        }
    }
}