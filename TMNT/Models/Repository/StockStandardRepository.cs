﻿using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using TMNT.Utils;
using TMNT.Models.Enums;
using System;
using System.Data;

namespace TMNT.Models.Repository {
    public class StockStandardRepository : IRepository<StockStandard> {
        private ApplicationDbContext db = DbContextSingleton.Instance;

        public StockStandardRepository() { }

        public StockStandardRepository(ApplicationDbContext db) {
            this.db = db;
        }

        public IEnumerable<StockStandard> Get() {
            return db.StockStandards
                .Include("InventoryItems.CertificatesOfAnalysis")
                .ToList();
        }

        public StockStandard Get(int? i) {
            return db.StockStandards.Find(i);
        }

        public CheckModelState Create(StockStandard t) {
            try {
                db.StockStandards.Add(t);
                if (db.SaveChanges() > 0) {
                    return CheckModelState.Valid;
                }
            } catch (DataException ex) {
                return CheckModelState.DataError;
            } catch (Exception ex) {
                return CheckModelState.Error;
            }
            return CheckModelState.Invalid;
        }

        public void Update(StockStandard t) {
            db.Entry(t).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(int? i) {
            db.StockStandards.Remove(db.StockStandards.Find(i));
            db.SaveChanges();
        }

        public void Dispose() {
            
        }
    }
}