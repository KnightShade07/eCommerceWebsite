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
        /// <summary>
        /// Displays a view that lists a page of products.
        /// </summary>
        /// 
        public async Task<IActionResult> Index(int? id)
        {
            int pageNum = id ?? 1;
            const int pageSize = 3;
            ViewData["CurrentPage"] = pageNum;
            //gets a single number from the database
            int numProducts = await ProductDB.GetTotalProductsAsync(_context);
            //casting numProducts as a double prevents integer division
            int totalPages = (int)Math.Ceiling((double)numProducts / pageSize);

            ViewData["MaxPage"] = totalPages;


            List<Product> products = await ProductDB.GetProductsAsync(_context, pageSize, pageNum);
            
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
                await ProductDB.AddProductAsync(_context, p);

                TempData["Message"] = $"{p.Title} was added successfully!";


                //redirect back to catalog page.
                return RedirectToAction("Index");
            }

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Product p = await ProductDB.GetProductAsync(_context, id);
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
            Product p = await ProductDB.GetProductAsync(_context, id);

            return View(p);
        }

        [HttpPost] 
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Product p = await ProductDB.GetProductAsync(_context, id);

            _context.Entry(p).State = EntityState.Deleted;

            await _context.SaveChangesAsync();

            TempData["Message"] = $"{p.Title} was deleted";

            return RedirectToAction("Index");
        }
    } 
}
