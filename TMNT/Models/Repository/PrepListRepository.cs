using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using TMNT.Models.Enums;
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

        public CheckModelState Create(PrepList t) {
            try {
                _db.PrepLists.Add(t);
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

        public CheckModelState Update(PrepList t) {
            throw new NotImplementedException();
        }

        public CheckModelState Delete(int? i) {
            throw new NotImplementedException();
        }

        public void Dispose() {
            _db.Dispose();
        }
    }
}