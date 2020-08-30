using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceWebsite.Data;
using eCommerceWebsite.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace eCommerceWebsite.Controllers
{


    public class CartController : Controller
    {
        private readonly ProductContext _context;
        private readonly IHttpContextAccessor _httpContext;
        public CartController(ProductContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContext = httpContext;
        }
        /// <summary>
        /// Adds a Product to the shopping cart
        /// </summary>
        /// <param name="id">The ID of the product</param>
        /// 
        public  async Task<IActionResult> Add(int id)
        {
            Product p = await ProductDB.GetProductAsync(_context, id);

            CookieHelper.AddProductToCart(_httpContext, p);

            return RedirectToAction("Index", "Product");
        }

        public IActionResult Summary()
        {
            return View(CookieHelper.getCartProducts(_httpContext));
        }
    }
}
