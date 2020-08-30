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
            const string CartCookie = "CartCookie";
            Product p = await ProductDB.GetProductAsync(_context, id);
            //get existing cart items
            string existingItems =_httpContext.HttpContext.Request.Cookies[CartCookie];
            //add current product to existing cart.
            List<Product> cartProducts = new List<Product>();
            if(existingItems != null)
            {
                cartProducts = JsonConvert.DeserializeObject<List<Product>>(existingItems);
            }
            cartProducts.Add(p);

            //Add product to cart cookie
            string data = JsonConvert.SerializeObject(p);
            CookieOptions options = new CookieOptions()
            {
                Expires = DateTime.Now.AddYears(1),
                Secure = true,
                IsEssential = true
            };

            _httpContext.HttpContext.Response.Cookies.Append(CartCookie, data, options);

            return RedirectToAction("Index", "Product");
        }

        public IActionResult Summary()
        {
            string cookieData = _httpContext.HttpContext.Request.Cookies["CartCookie"];

            List<Product> cartProducts = JsonConvert.DeserializeObject<List<Product>>(cookieData);
            return View(cartProducts);
        }
    }
}
