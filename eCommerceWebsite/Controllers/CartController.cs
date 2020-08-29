using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceWebsite.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Add(int id)
        {
            return View();
        }

        public IActionResult Summary()
        {
            return View();
        }
    }
}
