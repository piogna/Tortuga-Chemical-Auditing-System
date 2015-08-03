﻿using System;
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
        public byte[] Content { get; set; }
        //[DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime DateAdded { get; set; }

        //Foreign Keys
        public InventoryItem InventoryItem { get; set; }
    }
}
