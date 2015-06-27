using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebForms.Net45.Sample.Models
{
    public class ProductContext : DbContext
    {
        public ProductContext() : base("GachoMvpVmWebFormsSample")
        {
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }
    }
}