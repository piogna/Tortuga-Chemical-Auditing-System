using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using TMNT.Utils;
using System;
using TMNT.Models.Enums;
using System.Data;

namespace TMNT.Models.Repository {
    public class StockReagentRepository : IRepository<StockReagent> {
        private ApplicationDbContext db = DbContextSingleton.Instance;

        private static volatile StockReagentRepository instance;
        private static object syncRoot = new object();

        public StockReagentRepository() { }

        public static StockReagentRepository StockReagentRepositoryInstance {
            get {
                if (instance == null) {
                    lock (syncRoot) {
                        if (instance == null)
                            instance = new StockReagentRepository();
                    }
                }
                return instance;
            }
        }

        public StockReagentRepository(ApplicationDbContext db) {
            this.db = db;
        }

        public IEnumerable<StockReagent> Get() {
            return db.StockReagents.ToList();
        }

        public StockReagent Get(int? i) {
            return db.StockReagents.Find(i);
        }

        public void Create(StockReagent t) {
                db.StockReagents.Add(t);
        }

        public void Update(StockReagent t) {
                db.Entry(t).State = EntityState.Modified;
        }

        public void Delete(int? i) {
                db.StockReagents.Remove(db.StockReagents.Find(i));//change to archive in the future?
        }

        public void Dispose() {
            db.Dispose();
        }
    }
}