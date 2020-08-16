using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceWebsite.Models
{
    public class Product
    {
        [Key] //makes property a primary key in DB.
        public int ProductID { get; set; }
        /// <summary>
        /// Consumer-facing title of product
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// The retail price of the product.
        /// </summary>

        public double Price { get; set; }
        /// <summary>
        /// Category product falls under.
        /// </summary>
        public string Category { get; set; }
    }
}
