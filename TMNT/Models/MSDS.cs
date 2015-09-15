using System;
using System.ComponentModel.DataAnnotations;

namespace TMNT.Models
{
    public class MSDS
    {
        public int MSDSId { get; set; }
        [StringLength(255)]
        public string FileName { get; set; }
        [StringLength(100)]
        public string ContentType { get; set; }
        public byte[] Content { get; set; }
        public DateTime DateAdded { get; set; }
        [Display(Name = "SDS Notes"), DataType(DataType.MultilineText)]
        public string MSDSNotes { get; set; }

        //Foreign Key
        public virtual InventoryItem InventoryItem { get; set; }
    }
}
