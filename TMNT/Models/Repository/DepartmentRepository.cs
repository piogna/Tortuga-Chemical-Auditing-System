using System;
using System.Collections.Generic;
using System.Data;
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
            try {
                db.Departments.Add(t);
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

        public CheckModelState Update(Department t) {
            throw new NotImplementedException();
        }

        public CheckModelState Delete(int? i) {
            throw new NotImplementedException();
        }

        public void Dispose() {
            db.Dispose();
        }
    }
}