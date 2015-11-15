using System;
using System.Collections.Generic;
using System.Linq;
using TMNT.Models.Enums;
using TMNT.Utils;

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

        public CheckModelState Create(Device t) {
            throw new NotImplementedException();
        }

        public CheckModelState Update(Device t) {
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