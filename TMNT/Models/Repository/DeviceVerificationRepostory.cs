using System;
using System.Collections.Generic;
using System.Linq;
using TMNT.Utils;

namespace TMNT.Models.Repository {
    public class DeviceVerificationRepostory : IRepository<DeviceVerification> {
        ApplicationDbContext db = DbContextSingleton.Instance;

        public DeviceVerificationRepostory() { }

        public DeviceVerificationRepostory(ApplicationDbContext db) {
            this.db = db;
        }

        public IEnumerable<DeviceVerification> Get() {
            return db.DeviceVerifications.ToList();
        }

        public DeviceVerification Get(int? i) {
            return db.DeviceVerifications.Find(i);
        }

        public void Create(DeviceVerification t) {
            db.DeviceVerifications.Add(t);
            db.SaveChanges();
        }

        public void Update(DeviceVerification t) {
            throw new NotImplementedException();
        }

        public void Delete(int? i) {
            throw new NotImplementedException();
        }

        public void Dispose() {
            db.Dispose();
        }
    }
}