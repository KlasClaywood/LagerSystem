using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using LagerSystem.Models;

namespace LagerSystem.DataAccessLayer
{
    public class StoreContext : DbContext
    {
        public DbSet<StockItem> Items { get; set; }
        public StoreContext() : base ("DefaultConnection")
        {
            
        }
    }
}