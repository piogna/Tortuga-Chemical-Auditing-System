using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TMNT.Models.Repository;

namespace TMNT.Models.FakeRepository {
    public class FakeLocationRepository : IRepository<Location> {
        private List<Location> _tests = new List<Location>() {
            new Location() {
                LocationId = 1,
                Address = "123 Fake Street",
                City = "Mississauga",
                Province = "ON",
                PostalCode = "L6H2B3",
                LocationName = "Campobello",
                LocationCode = "CMP01",
                Website = "www.maxxam.com",
                PhoneNumber = "(416)-559-1234",
                FaxNumber = "(416)-559-1235"
            },
            new Location() {
                LocationId = 2,
                Address = "221b Baker Street",
                City = "Burnaby",
                Province = "BC",
                PostalCode = "L6H2B3",
                LocationName = "Burnaby",
                LocationCode = "BNY02",
                Website = "www.maxxam.com",
                PhoneNumber = "(416)-559-3456",
                FaxNumber = "(416)-559-3457"
            }
        };

        public IEnumerable<Location> Get() {
            return _tests;
        }

        public Location Get(int? i) {
            return _tests
                .Where(item => item.LocationId == i)
                .Select(item => item)
                .FirstOrDefault<Location>();
        }

        public void Create(Location t) {
            _tests.Add(t);
        }

        public void Update(Location t) {
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