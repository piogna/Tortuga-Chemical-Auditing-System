using System;
using System.Collections.Generic;
using System.Linq;
using TMNT.Models.Enums;
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

        public CheckModelState Create(DeviceVerification t) {
            try {
                db.DeviceVerifications.Add(t);
                if (db.SaveChanges() > 0) {
                    return CheckModelState.Valid;
                }
            } catch (Exception ex) {

            }
            return CheckModelState.Invalid;
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