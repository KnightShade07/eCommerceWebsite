using eCommerceWebsite.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceWebsite.Data
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> dbContextOptions)
            : base(dbContextOptions) {}

        public DbSet<Product> Products { get; set; }

        public DbSet<UserAccount> UserAccounts { get; set; }
        
    }
}
