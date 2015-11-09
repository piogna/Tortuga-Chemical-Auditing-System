using System;
using System.Collections.Generic;
using System.Data;
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
            } catch (DataException) {
                return CheckModelState.DataError;
            } catch (Exception) {
                return CheckModelState.Error;
            }
            return CheckModelState.Invalid;
        }

        public CheckModelState Delete(int? i) {
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

        public CheckModelState Update(PrepListItem t) {
            throw new NotImplementedException();
        }
    }
}