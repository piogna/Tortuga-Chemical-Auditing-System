using System;
using System.Collections.Generic;
using TMNT.Models.Enums;
using TMNT.Utils;

namespace TMNT.Models.Repository {
    public class CertificateOfAnalysisRepository : IRepository<CertificateOfAnalysis> {
        private ApplicationDbContext db = DbContextSingleton.Instance;

        public CertificateOfAnalysisRepository(ApplicationDbContext db) {
            this.db = db;
        }

        public CertificateOfAnalysisRepository() { }

        public IEnumerable<CertificateOfAnalysis> Get() {
            return db.CertificatesOfAnalysis;
        }

        public CertificateOfAnalysis Get(int? i) {
            return db.CertificatesOfAnalysis.Find(i);
        }
        public CheckModelState Create(CertificateOfAnalysis t) {
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