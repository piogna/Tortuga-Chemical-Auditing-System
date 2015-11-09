using System;
using System.Collections.Generic;
using TMNT.Utils;
using System.Data.Entity;
using TMNT.Models.Enums;
using System.Data;

namespace TMNT.Models.Repository {
    public class PreparedStandardRepository : IRepository<PreparedStandard> {
        private ApplicationDbContext db = DbContextSingleton.Instance;

        public PreparedStandardRepository() { }

        public PreparedStandardRepository(ApplicationDbContext db) {
            this.db = db;
        }

        public IEnumerable<PreparedStandard> Get() {
            return db.PreparedStandard;
        }

        public PreparedStandard Get(int? i) {
            return db.PreparedStandard.Find(i);
        }

        public CheckModelState Create(PreparedStandard t) {
            try {
                db.PreparedStandard.Add(t);
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

        public CheckModelState Update(PreparedStandard t) {
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
                db.StockReagents.Remove(db.StockReagents.Find(i));//change to archive in the future?
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