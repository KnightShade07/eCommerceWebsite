using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceWebsite.Data;
using eCommerceWebsite.Models;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceWebsite.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductContext _context;
        public ProductController(ProductContext context)
        {
            _context = context;
        }
        //Test
        public IActionResult Index()
        {
            //Get all products from DB.
            List<Product> products = _context.Products.ToList();
            
            return View(products);
        }
    }
}
