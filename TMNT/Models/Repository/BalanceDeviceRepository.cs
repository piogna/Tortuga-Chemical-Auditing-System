using System.Collections.Generic;
using System.Linq;
using TMNT.Utils;
using System.Data.Entity;
using System;
using TMNT.Models.Enums;
using System.Data;

namespace TMNT.Models.Repository {
    public class BalanceDeviceRepository : IRepository<Device> {
        private ApplicationDbContext _db = DbContextSingleton.Instance;

        public BalanceDeviceRepository() { }

        public BalanceDeviceRepository(ApplicationDbContext db) {
            _db = db;
        }

        public IEnumerable<Device> Get() {
            return _db.Devices.Where(item => item.DeviceType.Equals("Balance")).ToList();
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
                device.Status = "Archived";
                _db.Entry(device).State = EntityState.Modified;                
        }

        public void Dispose() {
        }
    }
}