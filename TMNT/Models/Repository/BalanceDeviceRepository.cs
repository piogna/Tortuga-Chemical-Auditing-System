using System.Collections.Generic;
using System.Linq;
using TMNT.Utils;
using System.Data.Entity;
using System;
using TMNT.Models.Enums;
using System.Data;
using System.Diagnostics;
using System.Data.Entity.Validation;

namespace TMNT.Models.Repository {
    public class BalanceDeviceRepository : IRepository<Device> {
        private ApplicationDbContext _db;

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
            _db.SaveChanges();
        }

        public void Delete(int? i) {
            var device = _db.Devices.Find(i);
            device.Status = "Archived";
            device.IsArchived = true;
            _db.Entry(device).State = EntityState.Modified;
            try {
                _db.SaveChanges();
            } catch (DbEntityValidationException ex) {
                foreach (var validationErrors in ex.EntityValidationErrors) {
                    foreach (var validationError in validationErrors.ValidationErrors) {
                        Trace.TraceInformation("Property: {0} Error: {1}",
                                                validationError.PropertyName,
                                                validationError.ErrorMessage);
                    }
                }
            }
        }

        public void Dispose() {
        }
    }
}