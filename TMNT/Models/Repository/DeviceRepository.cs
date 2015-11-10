using System.Collections.Generic;
using System.Linq;
using TMNT.Utils;
using System.Data.Entity;
using System;
using TMNT.Models.Enums;
using System.Data;

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
            } catch (DataException ex) {
                return CheckModelState.DataError;
            } catch (Exception) {
                return CheckModelState.Error;
            }
            return CheckModelState.Invalid;
        }

        public CheckModelState Update(Device t) {
            try {
                db.Entry(t).State = EntityState.Modified;
                if (db.SaveChanges() > 0) {
                    return CheckModelState.Valid;
                }
            } catch (DataException) {
                return CheckModelState.DataError;
            } catch (Exception) {
                return CheckModelState.Error;
            }
            return CheckModelState.Invalid;
        }

        public CheckModelState Delete(int? i) {
            var device = db.Devices.Find(i);

            if (device == null) {
                return CheckModelState.Error;
            }

            try {
                device.IsArchived = true;
                db.Entry(device).State = EntityState.Modified;
                if (db.SaveChanges() > 0) {
                    return CheckModelState.Valid;
                }
            } catch (DataException) {
                return CheckModelState.DataError;
            } catch (Exception) {
                return CheckModelState.Error;
            }
            return CheckModelState.Invalid;
        }

        public void Dispose() {
            db.Dispose();
        }
    }
}