using System.Collections.Generic;
using System.Linq;
using TMNT.Utils;
using System.Data.Entity;
using TMNT.Models.Enums;
using System;
using System.Data;

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
            try {
                db.Locations.Add(t);
                if (db.SaveChanges() > 0) {
                    return CheckModelState.Valid;
                }
            } catch (DataException) {
                return CheckModelState.DataError;
            } catch (Exception) {
                return CheckModelState.Error;
            }
            return CheckModelState.Invalid;
        }

        public CheckModelState Update(Location t) {
            throw new NotImplementedException();
        }

        public CheckModelState Delete(int? i) {
            throw new NotImplementedException();
        }

        public void Dispose() {
            db.Dispose();
        }
    }
}