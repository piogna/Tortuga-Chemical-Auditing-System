using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace TMNT.Models.Repository {
    public class IntermediateStandardRepository : IRepository<IntermediateStandard> {
        private ApplicationDbContext db = ApplicationDbContext.Create();

        public IEnumerable<IntermediateStandard> Get() {
            return db.IntermediateStandards.ToList();
        }

        public IntermediateStandard Get(int? i) {
            return db.IntermediateStandards.Find(i);
        }

        public void Create(IntermediateStandard t) {
            db.IntermediateStandards.Add(t);
            db.SaveChanges();
        }

        public void Update(IntermediateStandard t) {
            db.Entry(t).State = EntityState.Modified;
        }

        public void Delete(int? i) {
            db.IntermediateStandards.Remove(db.IntermediateStandards.Find(i));
            db.SaveChanges();
        }

        public void Dispose() {
            throw new NotImplementedException();
        }
    }
}