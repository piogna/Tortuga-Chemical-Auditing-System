using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using TMNT.Utils;
using TMNT.Models.Enums;
using System;

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
            try {
                db.WorkingStandards.Add(t);
                if (db.SaveChanges() > 0) {
                    return CheckModelState.Valid;
                }
            } catch (Exception ex) {

            }
            return CheckModelState.Invalid;
        }

        public void Update(WorkingStandard t) {
            db.Entry(t).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(int? i) {
            db.WorkingStandards.Remove(db.WorkingStandards.Find(i));
            db.SaveChanges();
        }

        public void Dispose() {
            db.Dispose();
        }
    }
}