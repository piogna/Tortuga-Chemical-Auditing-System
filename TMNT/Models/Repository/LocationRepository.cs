using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TMNT.Utils;
using System.Data.Entity;

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

        public void Create(Location t) {
            db.Locations.Add(t);
            db.SaveChanges();
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