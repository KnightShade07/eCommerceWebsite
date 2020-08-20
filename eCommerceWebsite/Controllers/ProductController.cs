using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceWebsite.Data;
using eCommerceWebsite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eCommerceWebsite.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductContext _context;
        public ProductController(ProductContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            //Get all products from DB.
            List<Product> products = await (from p in _context.Products
                                      select p).ToListAsync();
            
            return View(products);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Product p)
        {
            if (ModelState.IsValid)
            {
                //Add to DB
                _context.Products.Add(p);
                await _context.SaveChangesAsync();

                TempData["Message"] = $"{p.Title} was added successfully!";


                //redirect back to catalog page.
                return RedirectToAction("Index");
            }

            return View();
        }
        [HttpGet]
        public  async Task<IActionResult> Edit(int id)
        {
            //Get Product with corrosponding id.
            Product p =
                await (from prod in _context.Products
                       where prod.ProductID == id
                       select prod).SingleAsync();
            //Pass product to view.
            return View(p);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product p)
        {
            //if all the validation checks pass
            if (ModelState.IsValid)
            {
                _context.Entry(p).State = EntityState.Modified; //note, Modified is not a function
                await _context.SaveChangesAsync();

                ViewData["Message"] = "Product updated successfully!";
               
            }

            return View(p);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            Product p =
                await(from prod in _context.Products
                      where prod.ProductID == id
                      select prod).SingleAsync();
            return View(p);
        }
    } 
}
