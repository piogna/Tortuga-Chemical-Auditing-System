using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TMNT.Models.Repository;

namespace TMNT.Models.FakeRepository {
    public class FakeSupplierRepository : IRepository<Supplier> {
        private List<Supplier> _suppliers = new List<Supplier>() {
                new Supplier() {
                    Name = "Supplier One",
                    Address = "123 Fake Street",
                    City = "Oakville",
                    Province = "ON",
                    PostalCode = "L6C 1V2",
                    PhoneNumber = "(905)-888-8888",
                    FaxNumber = "(888)-888-8889",
                    Website = "http://www.google.ca"
                },
                new Supplier() {
                    Name = "Supplier Two",
                    Address = "45 Morden Rd",
                    City = "Niagara Falls",
                    Province = "ON",
                    PostalCode = "L6C 1V3",
                    PhoneNumber = "(905)-777-7778",
                    FaxNumber = "(888)-777-7779",
                    Website = "http://www.google.ca"
                },
                new Supplier() {
                    Name = "Supplier Three",
                    Address = "221b Baker Street",
                    City = "OrangeVille",
                    Province = "ON",
                    PostalCode = "L6C 1V4",
                    PhoneNumber = "(905)-555-5556",
                    FaxNumber = "(888)-555-5557",
                    Website = "http://www.google.ca"
                }
            };

        public IEnumerable<Supplier> Get() {
            return _suppliers;
        }

        public Supplier Get(int? i) {
            throw new NotImplementedException();
        }

        public void Create(Supplier t) {
            throw new NotImplementedException();
        }

        public void Update(Supplier t) {
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