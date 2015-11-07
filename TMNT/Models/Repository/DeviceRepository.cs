using System.Collections.Generic;
using System.Linq;
using TMNT.Utils;
using System.Data.Entity;
using System;
using TMNT.Models.Enums;

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
            try {
                db.Devices.Add(t);
                if (db.SaveChanges() > 0) {
                    return CheckModelState.Valid;
                }
            } catch (Exception ex) {
                return CheckModelState.Error;
            }
            return CheckModelState.Invalid;
        }

        public void Update(Device t) {
            try {
                db.Entry(t).State = EntityState.Modified;
                db.SaveChanges();
            } catch (Exception ex) {

            }
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