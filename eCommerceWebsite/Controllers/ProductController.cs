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
            int numProducts = await (from p in _context.Products
                               select p).CountAsync();
            //casting numProducts as a double prevents integer division
            int totalPages = (int)Math.Ceiling((double)numProducts / pageSize);

            ViewData["MaxPage"] = totalPages;

            //Get all products from DB.
            List<Product> products = await (from p in _context.Products
                                            orderby p.Title ascending
                                      select p)
                                      .Skip(pageSize * (pageNum - 1)) //Skip() must be before Take().
                                      .Take(pageSize).ToListAsync();
            
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

        [HttpPost] 
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Product p =  await (from prod in _context.Products
                         where prod.ProductID == id
                         select prod).SingleAsync();

            _context.Entry(p).State = EntityState.Deleted;

            await _context.SaveChangesAsync();

            TempData["Message"] = $"{p.Title} was deleted";

            return RedirectToAction("Index");
        }
    } 
}
