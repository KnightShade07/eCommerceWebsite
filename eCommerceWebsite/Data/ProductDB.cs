using eCommerceWebsite.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceWebsite.Data
{
    public static class ProductDB
    {
        /// <summary>
        /// Returns the total count of products.
        /// </summary>
        /// <param name="_context">The object that communicates with the database</param>
        public static async Task<int> GetTotalProductsAsync(ProductContext _context)
        {
           return await(from p in _context.Products
                  select p).CountAsync();
        }

        /// <summary>
        /// Get a page worth of products
        /// </summary>
        /// <param name="_context">Database Context</param>
        /// <param name="pageSize">Number of Products per page</param>
        /// <param name="pageNum">The page of products you want from the database</param>
        /// 
        public static async Task<List<Product>> GetProductsAsync(ProductContext _context, int pageSize, int pageNum)
        {
            return
                await (from p in _context.Products
                                            orderby p.Title ascending
                                            select p)
                                      .Skip(pageSize * (pageNum - 1)) //Skip() must be before Take().
                                      .Take(pageSize).ToListAsync();
        }
        /// <summary>
        /// Adds a product to the database
        /// </summary>
        /// <param name="_context">Database context</param>
        /// <param name="p">object containing product data.</param>
        /// <returns></returns>
        public static async Task <Product> AddProductAsync(ProductContext _context, Product p)
        {
            //Add to DB
            _context.Products.Add(p);
            await _context.SaveChangesAsync();
            return p;
        }
        /// <summary>
        /// Gets a single product from the database.
        /// </summary>
        /// <param name="context">The Database Context Object</param>
        /// <param name="prodID">The ID of the product.</param>
        /// <returns></returns>
        public static async Task <Product> GetProductAsync(ProductContext context, int prodID)
        {
            Product p = await (from products in context.Products
                               where products.ProductID == prodID
                               select products).SingleAsync();
            return p;
        }
    }
}
