using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using TMNT.Utils;
using TMNT.Models.Enums;
using System;
using System.Data;

namespace TMNT.Models.Repository {
    public class WorkingStandardRepository : IRepository<WorkingStandard> {
        private ApplicationDbContext db = DbContextSingleton.Instance;

        public WorkingStandardRepository() { }

        /// <summary>
        /// Parameter for when multiple contexts are open at the same time.
        /// </summary>
        /// <param name="db"></param>
        public WorkingStandardRepository(ApplicationDbContext db) {
            this.db = db;
        }

        public IEnumerable<WorkingStandard> Get() {
            return db.WorkingStandards.ToList();
        }

        public WorkingStandard Get(int? i) {
            return db.WorkingStandards.Find(i);
        }

        public CheckModelState Create(WorkingStandard t) {
            //try {
                db.WorkingStandards.Add(t);
                if (db.SaveChanges() > 0) {
                    return CheckModelState.Valid;
                }
            //} catch (DataException) {
                return CheckModelState.Invalid;
            //} catch (Exception) {
            //    return CheckModelState.Error;
            //}
            //return CheckModelState.Invalid;
        }

        public CheckModelState Update(WorkingStandard t) {
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
                db.WorkingStandards.Remove(db.WorkingStandards.Find(i));//change to archive in the future?
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
            db.Dispose();
        }
    }
}