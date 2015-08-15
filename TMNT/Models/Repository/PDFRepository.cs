using System;
using System.Collections.Generic;
using TMNT.Utils;

namespace TMNT.Models.Repository {

    public class CofARepository : IRepository<CertificateOfAnalysis> {

        private ApplicationDbContext _db = DbContextSingleton.Instance;

        public void Create(CertificateOfAnalysis t) {
            throw new NotImplementedException();
        }

        public void Delete(int? i) {
            throw new NotImplementedException();
        }

        public void Dispose() {
            throw new NotImplementedException();
        }

        public IEnumerable<CertificateOfAnalysis> Get() {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public void Delete(int? i) {
            throw new NotImplementedException();
        }

        public void Dispose() {
            throw new NotImplementedException();
        }

        public IEnumerable<MSDS> Get() {
            throw new NotImplementedException();
        }

        public MSDS Get(int? i) {
            return _db.MSDS.Find(i);
        }

        public void Update(MSDS t) {
            throw new NotImplementedException();
        }
    }
}