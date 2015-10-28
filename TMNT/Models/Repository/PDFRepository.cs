using System;
using System.Collections.Generic;
using System.Data.Entity;
using TMNT.Models.Enums;
using TMNT.Utils;

namespace TMNT.Models.Repository {

    public class CofARepository : IRepository<CertificateOfAnalysis> {

        private ApplicationDbContext _db = DbContextSingleton.Instance;

        public CheckModelState Create(CertificateOfAnalysis t) {
            try {
                _db.CertificatesOfAnalysis.Add(t);
                if (_db.SaveChanges() > 0) {
                    return CheckModelState.Valid;
                }
            } catch (Exception ex) {

            }
            return CheckModelState.Invalid;
        }

        public void Delete(int? i) {
            throw new NotImplementedException();
        }

        public void Dispose() {
            throw new NotImplementedException();
        }

        public IEnumerable<CertificateOfAnalysis> Get() {
            return _db.CertificatesOfAnalysis;
        }

        public CertificateOfAnalysis Get(int? i) {
            return _db.CertificatesOfAnalysis.Find(i);
        }

        public void Update(CertificateOfAnalysis t) {
            throw new NotImplementedException();
        }
    }

    public class MSDSRepository : IRepository<MSDS> {

        private ApplicationDbContext _db = DbContextSingleton.Instance;

        public CheckModelState Create(MSDS t) {
            try {
                _db.MSDS.Add(t);
                if (_db.SaveChanges() > 0) {
                    return CheckModelState.Valid;
                }
            } catch (Exception ex) {

            }
            return CheckModelState.Invalid;
        }

        public void Delete(int? i) {
            throw new NotImplementedException();
        }

        public void Dispose() {
            throw new NotImplementedException();
        }

        public IEnumerable<MSDS> Get() {
            return _db.MSDS;
        }

        public MSDS Get(int? i) {
            return _db.MSDS.Find(i);
        }

        public void Update(MSDS t) {
            try {
                _db.Entry(t).State = EntityState.Modified;
                _db.SaveChanges();
            } catch (Exception ex) {

            }
        }
    }
}