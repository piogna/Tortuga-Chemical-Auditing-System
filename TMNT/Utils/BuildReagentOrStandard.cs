using System;
using System.Linq;
using System.Web;
using TMNT.Models;
using TMNT.Models.Enums;
using TMNT.Models.Repository;
using TMNT.Models.ViewModels;

namespace TMNT.Utils {
    public static class BuildReagentOrStandard {

        /* Standard Code */
        public static StockStandardCreateViewModel BuildStandard(StockStandardCreateViewModel model, string devicesUsed, string[] AmountUnit, string[] ConcentrationUnit, HttpPostedFileBase uploadCofA, HttpPostedFileBase uploadMSDS) {
            var deviceRepo = new BalanceDeviceRepository();
            if (model.NumberOfBottles == 0) { model.NumberOfBottles = 1; }

            if (devicesUsed.Contains(",")) {
                model.DeviceOne = deviceRepo.Get().Where(item => item.DeviceCode.Equals(devicesUsed.Split(',')[0])).FirstOrDefault();
                model.DeviceTwo = deviceRepo.Get().Where(item => item.DeviceCode.Equals(devicesUsed.Split(',')[1])).FirstOrDefault();
            } else {
                model.DeviceOne = deviceRepo.Get().Where(item => item.DeviceCode.Equals(devicesUsed.Split(',')[0])).FirstOrDefault();
            }

            model.InitialAmountUnits = AmountUnit[0];
            model.InitialConcentrationUnits = ConcentrationUnit[0];

            if (AmountUnit.Length > 1) {
                model.InitialAmountUnits += "/" + AmountUnit[1];
            }

            if (ConcentrationUnit.Length > 1) {
                model.InitialConcentrationUnits += "/" + ConcentrationUnit[1];
            }

            if (uploadCofA != null) {
                var cofa = new CertificateOfAnalysis() {
                    FileName = uploadCofA.FileName,
                    ContentType = uploadCofA.ContentType,
                    DateAdded = DateTime.Today
                };

                using (var reader = new System.IO.BinaryReader(uploadCofA.InputStream)) {
                    cofa.Content = reader.ReadBytes(uploadCofA.ContentLength);
                }
                model.CertificateOfAnalysis = cofa;
            }

            if (uploadMSDS != null) {
                var msds = new MSDS() {
                    FileName = uploadMSDS.FileName,
                    ContentType = uploadMSDS.ContentType,
                    DateAdded = DateTime.Today,
                    MSDSNotes = model.MSDSNotes
                };
                using (var reader = new System.IO.BinaryReader(uploadMSDS.InputStream)) {
                    msds.Content = reader.ReadBytes(uploadMSDS.ContentLength);
                }
                model.MSDS = msds;
            }
            return model;
        }

        public static InventoryItem BuildStandardInventoryItem(StockStandardCreateViewModel model, Department department) {
            InventoryItem inventoryItem = new InventoryItem() {
                CatalogueCode = model.CatalogueCode.ToUpper(),
                Department = department,
                UsedFor = model.UsedFor,
                Type = "Standard",
                FirstDeviceUsed = model.DeviceOne,
                SecondDeviceUsed = model.DeviceTwo,
                StorageRequirements = model.StorageRequirements,
                SupplierName = model.SupplierName,
                NumberOfBottles = model.NumberOfBottles,
                InitialAmount = model.InitialAmount.ToString() + " " + model.InitialAmountUnits,
                OtherUnitExplained = model.OtherUnitExplained,
                ConcentrationOtherUnitExplained = model.ConcentrationOtherUnitExplained
            };

            inventoryItem.MSDS.Add(model.MSDS);
            inventoryItem.CertificatesOfAnalysis.Add(model.CertificateOfAnalysis);

            return inventoryItem;
        }

        public static CheckModelState EnterStandardIntoDatabase(StockStandardCreateViewModel model, InventoryItem inventoryItem, int numOfItems, Department department, string user) {
            CheckModelState result = CheckModelState.Invalid;//default to invalid to expect the worst
            StockStandardRepository repo = new StockStandardRepository(DbContextSingleton.Instance);

            StockStandard createStandard = new StockStandard() {
                LotNumber = model.LotNumber,
                StockStandardName = model.StockStandardName,
                Purity = model.Purity,
                SolventUsed = model.SolventUsed,
                Concentration = model.Concentration.ToString() + " " + model.InitialConcentrationUnits,
                DateReceived = model.DateReceived,
                DateCreated = DateTime.Today,
                DateOpened = null,
                DaysUntilExpired = model.DaysUntilExpired,
                ExpiryDate = model.ExpiryDate,
                CreatedBy = user,
                DateModified = null,
                CatalogueCode = model.CatalogueCode
            };

            if (model.NumberOfBottles > 1) {
                for (int i = 1; i <= model.NumberOfBottles; i++) {
                    createStandard.IdCode = department.Location.LocationCode + "-" + (numOfItems + 1) + "-" + model.LotNumber + "/" + i;//append number of bottles

                    createStandard.InventoryItems.Add(inventoryItem);
                    result = repo.Create(createStandard);

                    //creation wasn't successful - break from loop and let switch statement handle the problem
                    if (result != CheckModelState.Valid) { break; }
                }
            } else {
                createStandard.IdCode = department.Location.LocationCode + "-" + (numOfItems + 1) + "-" + model.LotNumber + "/" + model.NumberOfBottles;//only 1 bottle, no need to concatenate

                createStandard.InventoryItems.Add(inventoryItem);
                result = repo.Create(createStandard);
            }
            return result;
        }
        /* End Standard Code */
    }
}