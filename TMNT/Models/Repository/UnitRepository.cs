using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMNT.Models.Repository {
    /* Not using the database since units will remain static. List for better performance */
    public class UnitRepository : IRepository<Unit> {
        private ApplicationDbContext _db = ApplicationDbContext.Create();

        private List<Unit> _units = new List<Unit>() {
            new Unit() {
                UnitId = 1,
                UnitName = "ml"
            },
            new Unit() {
                UnitId = 2,
                UnitName = "ug"
            }
        };

        public IEnumerable<Unit> Get() {
            return _db.Units.ToList();//_units;
        }

        public Unit Get(int? i) {
            return _db.Units
                .Where(item => item.UnitId == i)
                .FirstOrDefault<Unit>();
        }

        public void Create(Unit t) {
            throw new NotImplementedException();
        }

        public void Update(Unit t) {
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