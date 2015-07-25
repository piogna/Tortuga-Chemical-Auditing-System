using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using TMNT.Utils;

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

        public void Create(WorkingStandard t) {
            db.WorkingStandards.Add(t);
            db.SaveChanges();
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
            throw new NotImplementedException();
        }
    }
}