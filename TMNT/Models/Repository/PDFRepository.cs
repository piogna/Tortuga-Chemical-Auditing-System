using System;
using System.Collections.Generic;
using System.Data;
using TMNT.Models.Enums;
using TMNT.Utils;

namespace TMNT.Models.Repository {

    public class CofARepository : IRepository<CertificateOfAnalysis> {

        private ApplicationDbContext _db;

        public CofARepository() { }

        public CofARepository(ApplicationDbContext db) {
            _db = db;
        }

        public void Create(CertificateOfAnalysis t) {
                _db.CertificatesOfAnalysis.Add(t);

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

        private ApplicationDbContext _db;

        public MSDSRepository() { }

        public MSDSRepository(ApplicationDbContext db) {
            _db = db;
        }

        public void Create(MSDS t) {
                _db.MSDS.Add(t);
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