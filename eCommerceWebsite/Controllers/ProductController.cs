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

        public IActionResult Index()
        {
            //Get all products from DB.
            List<Product> products = _context.Products.ToList();
            
            return View(products);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Product p)
        {
            if (ModelState.IsValid)
            {
                //Add to DB
                _context.Products.Add(p);
                _context.SaveChanges();

                TempData["Message"] = $"{p.ProductID}:{p.Title} was added successfully!";


                //redirect back to catalog page.
                return RedirectToAction("Index");
            }

            return View();
        }
    } 
}
