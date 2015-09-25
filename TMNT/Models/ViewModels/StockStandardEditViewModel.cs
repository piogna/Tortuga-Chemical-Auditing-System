using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TMNT.Models.ViewModels {
    public class StockStandardEditViewModel {
        public int StockStandardId { get; set; }

        //stock standard properties
        [Required(ErrorMessage = "{0} is Required"), Display(Name = "ID Code")]
        public string IdCode { get; set; }
        [Required(ErrorMessage = "{0} is Required"), Display(Name = "Standard Name")]
        public string StockStandardName { get; set; }
        [Display(Name = "Date Opened")]
        public DateTime? DateOpened { get; set; }
        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }
        [DataType(DataType.Date), Display(Name = "Date Last Modified")]
        public DateTime? DateModified { get; set; }
        [Display(Name = "Last Modified By")]
        public string LastModifiedBy { get; set; }
        [Required(ErrorMessage = "{0} is Required"), Display(Name = "Supplier")]
        public string SupplierName { get; set; }
        [Required(ErrorMessage = "Lot Number is Required"), Display(Name = "Lot No.")]
        public string LotNumber { get; set; }

        //properties to help with views and have nothing to do with the db
        public bool IsExpired { get; set; }
        public bool IsOpened { get; set; }

        //inventory properties
        public List<Device> DevicesUsed { get; set; }
        [Display(Name = "SDS")]
        public MSDS MSDS { get; set; }
        [DataType(DataType.Date), Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; }
        [Display(Name = "Certificate of Analysis")]
        public CertificateOfAnalysis CertificateOfAnalysis { get; set; }
        public Unit Unit { get; set; }
        public Department Department { get; set; }

        public List<CertificateOfAnalysis> AllCertificatesOfAnalysis { get; set; }
        public List<MSDS> AllMSDS { get; set; }

        //View Model data fields
        public List<String> Storage { get; set; }
        public IEnumerable<Device> BalanceDevices { get; set; }
        public IEnumerable<Device> VolumeDevices { get; set; }
    }
}