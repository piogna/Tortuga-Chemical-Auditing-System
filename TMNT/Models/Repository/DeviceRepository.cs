using System;
using System.Collections.Generic;
using System.Linq;
using TMNT.Models.Enums;
using TMNT.Utils;

namespace TMNT.Models.Repository {
    public class DeviceRepository : IRepository<Device> {
        private ApplicationDbContext _db;

        public DeviceRepository() { }

        public DeviceRepository(ApplicationDbContext db) {
            _db = db;
        }

        public IEnumerable<Device> Get() {
            return _db.Devices.ToList();
        }

        public Device Get(int? i) {
            return _db.Devices.Find(i);
        }

        public void Create(Device t) {
            throw new NotImplementedException();
        }

        public void Update(Device t) {
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