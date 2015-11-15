using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using TMNT.Models.Enums;
using TMNT.Utils;

namespace TMNT.Models.Repository {
    public class VolumetricDeviceRepository : IRepository<Device> {

        private ApplicationDbContext db = DbContextSingleton.Instance;

        public VolumetricDeviceRepository() { }

        public IEnumerable<Device> Get() {
            return db.Devices.Where(item => item.DeviceType.Equals("Volumetric")).ToList();
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
            } catch (DataException) {
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
                device.Status = "Archived";
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
            throw new NotImplementedException();
        }
    }
}