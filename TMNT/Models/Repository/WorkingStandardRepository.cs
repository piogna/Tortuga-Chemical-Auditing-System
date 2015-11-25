using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using TMNT.Utils;
using TMNT.Models.Enums;
using System;
using System.Data;

namespace TMNT.Models.Repository {
    public class WorkingStandardRepository : IRepository<WorkingStandard> {
        private ApplicationDbContext _db;

        public WorkingStandardRepository() { }

        /// <summary>
        /// Parameter for when multiple contexts are open at the same time.
        /// </summary>
        /// <param name="db"></param>
        public WorkingStandardRepository(ApplicationDbContext db) {
            this._db = db;
        }

        public IEnumerable<WorkingStandard> Get() {
            return _db.WorkingStandards.ToList();
        }

        public WorkingStandard Get(int? i) {
            return _db.WorkingStandards.Find(i);
        }

        public void Create(WorkingStandard t) {
            _db.WorkingStandards.Add(t);
        }

        public void Update(WorkingStandard t) {
            _db.Entry(t).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void Delete(int? i) {
            _db.WorkingStandards.Remove(_db.WorkingStandards.Find(i));//change to archive in the future?
        }

        public void Dispose() {
            _db.Dispose();
        }
    }
}