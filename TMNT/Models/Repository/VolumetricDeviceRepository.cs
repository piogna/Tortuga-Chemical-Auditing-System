using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using TMNT.Models.Enums;
using TMNT.Utils;

namespace TMNT.Models.Repository {
    public class VolumetricDeviceRepository : IRepository<Device> {

        private ApplicationDbContext _db;

        public VolumetricDeviceRepository() { }

        public VolumetricDeviceRepository(ApplicationDbContext db) {
            this._db = db;
        }

        public IEnumerable<Device> Get() {
            return _db.Devices.Where(item => item.DeviceType.Equals("Volumetric")).ToList();
        }

        public Device Get(int? i) {
            return _db.Devices.Find(i);
        }
        public void Create(Device t) {
            _db.Devices.Add(t);
        }

        public void Update(Device t) {
            _db.Entry(t).State = EntityState.Modified;
        }

        public void Delete(int? i) {
            var device = _db.Devices.Find(i);
            _db.Entry(device).State = EntityState.Modified;
        }

        public void Dispose() {
            throw new NotImplementedException();
        }
    }
}