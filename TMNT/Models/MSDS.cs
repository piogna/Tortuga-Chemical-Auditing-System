﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        //Foreign Key
        public virtual InventoryItem InventoryItem { get; set; }
    }
}
