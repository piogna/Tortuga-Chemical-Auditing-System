using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TMNT.Models.FakeModels;
using TMNT.Models.Repository;

namespace TMNT.Models.FakeRepository {
    public class FakeDeviceTestRepository : IRepository<FakeDeviceTest> {
        private List<FakeDeviceTest> _tests = new List<FakeDeviceTest>() {
                new FakeDeviceTest() { DeviceID = 0001, DeviceType = "Syringe", TestDueDate = "2015-04-18" },
                new FakeDeviceTest() { DeviceID = 0002, DeviceType = "Pipette", TestDueDate = "2015-04-20" },
                new FakeDeviceTest() { DeviceID = 0003, DeviceType = "Scale", TestDueDate = "2015-04-20" }
        };

        public IEnumerable<FakeDeviceTest> Get() {
            return _tests;
        }

        public FakeDeviceTest Get(int? i) {
            throw new NotImplementedException();
                //_tests
                //.Where(item => item.DeviceID == i)
                
        }

        public void Create(FakeDeviceTest t) {
            throw new NotImplementedException();
        }

        public void Update(FakeDeviceTest t) {
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