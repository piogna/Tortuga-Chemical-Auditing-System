using System;
using System.Collections.Generic;
using TMNT.Utils;

namespace TMNT.Models.Repository {

    public class CofARepository : IRepository<CertificateOfAnalysis> {

        private ApplicationDbContext _db = DbContextSingleton.Instance;

        public void Create(CertificateOfAnalysis t) {
            _db.CertificatesOfAnalysis.Add(t);
            _db.SaveChanges();
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

        public void Create(MSDS t) {
            _db.MSDS.Add(t);
            _db.SaveChanges();
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
            throw new NotImplementedException();
        }
    }
}