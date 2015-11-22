using System.Collections.Generic;
using System.Linq;
using TMNT.Utils;
using TMNT.Models.Enums;
using System;
using System.Data;

namespace TMNT.Models.Repository {
    public class LocationRepository : IRepository<Location> {
        ApplicationDbContext _db;

        public LocationRepository() { }

        public LocationRepository(ApplicationDbContext db) {
            _db = db;
        }

        public IEnumerable<Location> Get() {
            return _db.Locations.ToList();
        }

        public Location Get(int? i) {
            return _db.Locations.Find(i);
        }

        public void Create(Location t) {
                _db.Locations.Add(t);
                
        }

        public void Update(Location t) {
            throw new NotImplementedException();
        }

        public void Delete(int? i) {
            throw new NotImplementedException();
        }

        public void Dispose() {
            _db.Dispose();
        }
    }
}