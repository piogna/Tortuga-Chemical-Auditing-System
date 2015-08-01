using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TMNT.Utils;
using System.Data.Entity;

namespace TMNT.Models.Repository {
    public class DeviceRepository : IRepository<Device> {
        private ApplicationDbContext db = DbContextSingleton.Instance;

        public DeviceRepository() { }

        public DeviceRepository(ApplicationDbContext db) {
            this.db = db;
        }

        public IEnumerable<Device> Get() {
            return db.Devices.ToList();
        }

        public Device Get(int? i) {
            return db.Devices.Find(i);
        }

        public void Create(Device t) {
            db.Devices.Add(t);
            db.SaveChanges();
        }

        public void Update(Device t) {
            db.Entry(t).State = EntityState.Modified;
        }

        public void Delete(int? i) {
            db.Devices.Remove(db.Devices.Find(i));
            db.SaveChanges();
        }

        public void Dispose() {
            db.Dispose();
        }
    }
}