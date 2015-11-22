using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using TMNT.Utils;
using TMNT.Models.Enums;
using System;
using System.Data.Entity.Validation;
using System.Data;

namespace TMNT.Models.Repository {
    public class IntermediateStandardRepository : IRepository<IntermediateStandard> {
        private ApplicationDbContext _db;

        public IntermediateStandardRepository() { }

        public IntermediateStandardRepository(ApplicationDbContext db) {
            this._db = db;
        }

        public IEnumerable<IntermediateStandard> Get() {
            return _db.IntermediateStandards.ToList();
        }

        public IntermediateStandard Get(int? i) {
            return _db.IntermediateStandards.Find(i);
        }

        public void Create(IntermediateStandard t) {
                _db.IntermediateStandards.Add(t);
        }

        public void Update(IntermediateStandard t) {
                _db.Entry(t).State = EntityState.Modified;
                
        }

        public void Delete(int? i) {
            
                _db.IntermediateStandards.Remove(_db.IntermediateStandards.Find(i));//change to archive in the future?
                
        }

        public void Dispose() {
            _db.Dispose();
        }
    }
}