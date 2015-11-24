using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using TMNT.Utils;
using TMNT.Models.Enums;
using System;
using System.Data;

namespace TMNT.Models.Repository {
    public class WorkingStandardRepository : IRepository<WorkingStandard> {
        private ApplicationDbContext db;

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

        public void Create(WorkingStandard t) {
            db.WorkingStandards.Add(t);
        }

        public void Update(WorkingStandard t) {
            db.Entry(t).State = EntityState.Modified;
        }

        public void Delete(int? i) {
            db.WorkingStandards.Remove(db.WorkingStandards.Find(i));//change to archive in the future?
        }

        public void Dispose() {
            db.Dispose();
        }
    }
}