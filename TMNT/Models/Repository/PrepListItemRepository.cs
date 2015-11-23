using System;
using System.Collections.Generic;
using System.Data;
using TMNT.Models.Enums;
using TMNT.Utils;

namespace TMNT.Models.Repository {
    public class PrepListItemRepository : IRepository<PrepListItem> {

        private ApplicationDbContext _db;

        public PrepListItemRepository() { }

        public PrepListItemRepository(ApplicationDbContext db) {
            this._db = db;
        }

        public void Create(PrepListItem t) {
            _db.PrepListItems.Add(t);
        }

        public void Delete(int? i) {
            throw new NotImplementedException();
        }

        public void Dispose() {
            _db.Dispose();
        }

        public IEnumerable<PrepListItem> Get() {
            return _db.PrepListItems;
        }

        public PrepListItem Get(int? i) {
            return _db.PrepListItems.Find(i);
        }

        public void Update(PrepListItem t) {
            throw new NotImplementedException();
        }
    }
}