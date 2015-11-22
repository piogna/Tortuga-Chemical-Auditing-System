using System;
using System.Collections.Generic;
using TMNT.Models.Enums;
using TMNT.Utils;

namespace TMNT.Models.Repository {
    public class CertificateOfAnalysisRepository : IRepository<CertificateOfAnalysis> {
        private ApplicationDbContext _db;

        public CertificateOfAnalysisRepository(ApplicationDbContext db) {
_db = db;
        }

        public CertificateOfAnalysisRepository() { }

        public IEnumerable<CertificateOfAnalysis> Get() {
            return _db.CertificatesOfAnalysis;
        }

        public CertificateOfAnalysis Get(int? i) {
            return _db.CertificatesOfAnalysis.Find(i);
        }
        public void Create(CertificateOfAnalysis t) {
            throw new NotImplementedException();
        }

        public void Update(CertificateOfAnalysis t) {
            throw new NotImplementedException();
        }

        public void Delete(int? i) {
            throw new NotImplementedException();
        }

        public void Dispose() {
            throw new NotImplementedException();
        }
    }
}