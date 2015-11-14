using System;
using System.ComponentModel.DataAnnotations;

namespace TMNT.Models
{
    public class CertificateOfAnalysis
    {
        public int CertificateOfAnalysisId { get; set; }
        [StringLength(255)]
        public string FileName { get; set; }
        [StringLength(100)]
        public string ContentType { get; set; }
        [Display(Name = "Upload CofA")]
        public byte[] Content { get; set; }
        public DateTime DateAdded { get; set; }

        //Foreign Keys
        public InventoryItem InventoryItem { get; set; }
    }
}
