using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using TMNT.Models.Enums;
using TMNT.Utils;

namespace TMNT.Models.Repository {
    public class DepartmentRepository : IRepository<Department> {
        private ApplicationDbContext _db;

        public DepartmentRepository() { }

        public DepartmentRepository(ApplicationDbContext db) {
            this._db = db;
        }

        public IEnumerable<Department> Get() {
            return _db.Departments.ToList();
        }

        public Department Get(int? i) {
            return _db.Departments.Find(i);
        }

        public void Create(Department t) {
            _db.Departments.Add(t);
        }

        public void Update(Department t) {
            throw new NotImplementedException();
        }

        public void Delete(int? i) {
            throw new NotImplementedException();
        }

        public void Dispose() {
        }
    }
}