﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TMNT.Utils;

namespace TMNT.Models.Repository
{
    public class LowStockRepository : IRepository<InventoryItem>
    {
        private ApplicationDbContext db = DbContextSingleton.Instance;


        public IEnumerable<InventoryItem> Get()
        {
            return db.InventoryItems.Where(i => i.LowAmountThreshHold > i.Amount).ToList();
        }

        public InventoryItem Get(int? i)
        {
            throw new NotImplementedException();
        }

        public void Create(InventoryItem t)
        {
            throw new NotImplementedException();
        }

        public void Update(InventoryItem t)
        {
            throw new NotImplementedException();
        }

        public void Delete(int? i)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}