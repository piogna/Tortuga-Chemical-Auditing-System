using System.Collections.Generic;
using System.Linq;
using TMNT.Utils;
using System.Data.Entity;
using TMNT.Models.Enums;
using System;

namespace TMNT.Models.Repository {
    public class LocationRepository : IRepository<Location> {
        ApplicationDbContext db = DbContextSingleton.Instance;

        public LocationRepository() { }

        public LocationRepository(ApplicationDbContext db) {
            this.db = db;
        }

        public IEnumerable<Location> Get() {
            return db.Locations.ToList();
        }

        public Location Get(int? i) {
            return db.Locations.Find(i);
        }

        public CheckModelState Create(Location t) {
            db.Locations.Add(t);
            db.SaveChanges();
            try {
                db.Locations.Add(t);
                if (db.SaveChanges() > 0) {
                    return CheckModelState.Valid;
                }
            } catch (Exception ex) {

            }
            return CheckModelState.Invalid;
        }

        public void Update(Location t) {
            db.Entry(t).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(int? i) {
            db.Locations.Remove(db.Locations.Find(i));
            db.SaveChanges();
        }

        public void Dispose() {
            db.Dispose();
        }
    }
}