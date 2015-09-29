﻿using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using TMNT.Utils;
using TMNT.Models.Enums;
using System;

namespace TMNT.Models.Repository {
    public class IntermediateStandardRepository : IRepository<IntermediateStandard> {
        private ApplicationDbContext _db = DbContextSingleton.Instance;

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

        public CheckModelState Create(IntermediateStandard t) {
            try {
                _db.IntermediateStandards.Add(t);
                if (_db.SaveChanges() > 0) {
                    return CheckModelState.Valid;
                }
            } catch (Exception ex) {

            }
            return CheckModelState.Invalid;
        }

        public void Update(IntermediateStandard t) {
            _db.Entry(t).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void Delete(int? i) {
            _db.IntermediateStandards.Remove(_db.IntermediateStandards.Find(i));
            _db.SaveChanges();
        }

        public void Dispose() {
            _db.Dispose();
        }
    }
}