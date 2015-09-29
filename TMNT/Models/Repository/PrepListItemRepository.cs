using System;
using System.Collections.Generic;
using TMNT.Models.Enums;
using TMNT.Utils;

namespace TMNT.Models.Repository {
    public class PrepListItemRepository : IRepository<PrepListItem> {

        private ApplicationDbContext _db = DbContextSingleton.Instance;

        public PrepListItemRepository() { }

        public PrepListItemRepository(ApplicationDbContext db) {
            this._db = db;
        }

        public CheckModelState Create(PrepListItem t) {
            try {
                _db.PrepListItems.Add(t);
                if (_db.SaveChanges() > 0) {
                    return CheckModelState.Valid;
                }
            } catch (Exception ex) {

            }
            return CheckModelState.Invalid;
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
            _db.Entry(t).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();
        }
    }
}