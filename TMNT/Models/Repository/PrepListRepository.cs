using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TMNT.Utils;

namespace TMNT.Models.Repository {
    public class PrepListRepository : IRepository<PrepList> {
        private ApplicationDbContext _db = DbContextSingleton.Instance;

        public PrepListRepository() { }

        public PrepListRepository(ApplicationDbContext db) {
            this._db = db;
        }

        public IEnumerable<PrepList> Get() {
            return _db.PrepLists.ToList();
        }

        public PrepList Get(int? i) {
            return _db.PrepLists.Find(i);
        }

        public void Create(PrepList t) {
            _db.PrepLists.Add(t);
            _db.SaveChanges();
        }

        public void Update(PrepList t) {
            _db.Entry(t).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void Delete(int? i) {
            throw new NotImplementedException();
        }

        public void Dispose() {
            _db.Dispose();
        }
    }
}