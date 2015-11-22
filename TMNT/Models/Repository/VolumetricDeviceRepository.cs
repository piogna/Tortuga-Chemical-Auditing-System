using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using TMNT.Models.Enums;
using TMNT.Utils;

namespace TMNT.Models.Repository {
    public class VolumetricDeviceRepository : IRepository<Device> {

        private ApplicationDbContext db = DbContextSingleton.Instance;

        public VolumetricDeviceRepository() { }

        public VolumetricDeviceRepository(ApplicationDbContext db) {
            this.db = db;
        }

        public IEnumerable<Device> Get() {
            return db.Devices.Where(item => item.DeviceType.Equals("Volumetric")).ToList();
        }

        public Device Get(int? i) {
            return db.Devices.Find(i);
        }
        public void Create(Device t) {
                db.Devices.Add(t);
        }

        public void Update(Device t) {
                db.Entry(t).State = EntityState.Modified;
        }

        public void Delete(int? i) {
            var device = db.Devices.Find(i);
                db.Entry(device).State = EntityState.Modified;
        }

        public void Dispose() {
            throw new NotImplementedException();
        }
    }
}