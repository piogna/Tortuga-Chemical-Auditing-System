using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TMNT.Models.Enums;
using TMNT.Utils;

namespace TMNT.Models.Repository {
    public class DeviceVerificationRepostory : IRepository<DeviceVerification> {
        private ApplicationDbContext _db;

        public DeviceVerificationRepostory() { }

        public DeviceVerificationRepostory(ApplicationDbContext db) {
            _db = db;
        }

        public IEnumerable<DeviceVerification> Get() {
            return _db.DeviceVerifications.ToList();
        }

        public DeviceVerification Get(int? i) {
            return _db.DeviceVerifications.Find(i);
        }

        public void Create(DeviceVerification t) {
                _db.DeviceVerifications.Add(t);                
        }

        public void Update(DeviceVerification t) {
            throw new NotImplementedException();
        }

        public void Delete(int? i) {
            throw new NotImplementedException();
        }

        public void Dispose() {
        }
    }
}