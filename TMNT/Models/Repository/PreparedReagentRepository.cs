using System;
using System.Collections.Generic;
using System.Linq;
using TMNT.Utils;
using System.Data.Entity;
using TMNT.Models.Enums;
using System.Data;

namespace TMNT.Models.Repository {
    public class PreparedReagentRepository : IRepository<PreparedReagent> {
        private ApplicationDbContext db = DbContextSingleton.Instance;

        public PreparedReagentRepository() { }
        public PreparedReagentRepository(ApplicationDbContext db) {
            this.db = db;
        }

        public IEnumerable<PreparedReagent> Get() {
            return db.PreparedReagent.ToList();
        }

        public PreparedReagent Get(int? i) {
            return db.PreparedReagent.Find(i);
        }

        public CheckModelState Create(PreparedReagent t) {
            try {
                db.PreparedReagent.Add(t);
                if (db.SaveChanges() > 0) {
                    return CheckModelState.Valid;
                }
            } catch (DataException) {
                return CheckModelState.DataError;
            } catch (Exception) {
                return CheckModelState.Error;
            }
            return CheckModelState.Invalid;
        }

        public CheckModelState Update(PreparedReagent t) {
            try {
                db.Entry(t).State = EntityState.Modified;
                if (db.SaveChanges() > 0) {
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
            try {
                db.PreparedReagent.Remove(db.PreparedReagent.Find(i));//change to archive in the future?
                if (db.SaveChanges() > 0) {
                    return CheckModelState.Valid;
                }
            } catch (DataException) {
                return CheckModelState.DataError;
            } catch (Exception) {
                return CheckModelState.Error;
            }
            return CheckModelState.Invalid;
        }

        public void Dispose() {
            throw new NotImplementedException();
        }
    }
}