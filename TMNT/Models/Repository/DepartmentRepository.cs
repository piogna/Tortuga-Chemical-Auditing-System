using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TMNT.Models.Enums;
using TMNT.Utils;

namespace TMNT.Models.Repository {
    public class DepartmentRepository : IRepository<Department> {
        ApplicationDbContext db = DbContextSingleton.Instance;

        public DepartmentRepository() { }

        public DepartmentRepository(ApplicationDbContext db) {
            this.db = db;
        }

        public IEnumerable<Department> Get() {
            return db.Departments.ToList();
        }

        public Department Get(int? i) {
            return db.Departments.Find(i);
        }

        public CheckModelState Create(Department t) {
            db.Departments.Add(t);
            db.SaveChanges();
            try {
                db.Departments.Add(t);
                if (db.SaveChanges() > 0) {
                    return CheckModelState.Valid;
                }
            } catch (Exception ex) {

            }
            return CheckModelState.Invalid;
        }

        public void Update(Department t) {
            db.Entry(t).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(int? i) {
            db.Departments.Remove(db.Departments.Find(i));
            db.SaveChanges();
        }

        public void Dispose() {
            db.Dispose();
        }
    }
}