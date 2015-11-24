using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TMNT.Models {
    public class Device {
        [Key]
        public int DeviceId { get; set; }
        public string DeviceCode { get; set; }
        public bool IsVerified { get; set; }
        public string DeviceType { get; set; }
        public string Status { get; set; }//in good standing, getting repaired etc
        public int NumberOfDecimals { get; set; }
        public int NumberOfTestsToVerify { get; set; }
        public string AmountLimitOne { get; set; }
        public string AmountLimitTwo { get; set; }
        public string AmountLimitThree { get; set; }
        public bool IsArchived { get; set; }

        //volumetric properties
        public string VolumetricDeviceType { get; set; }
        public string Categorization { get; set; }
        public string Frequency { get; set; }
        public string Volume { get; set; }
        public string AcceptanceCriteria { get; set; }

        public virtual Department Department { get; set; }

        public virtual ICollection<DeviceVerification> DeviceVerifications { get; set; }
    }
}