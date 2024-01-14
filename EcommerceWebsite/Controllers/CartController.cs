using EcommerceWebsite.Infrastructure;
using EcommerceWebsite.Models;
using EcommerceWebsite.Models.Cart;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using NuGet.Protocol.Core.Types;

namespace EcommerceWebsite.Controllers
{
    public class CartController : Controller
    {
        private readonly QlbanVaLiContext _context;
        public CartController(QlbanVaLiContext context)
        {
            _context = context;
        }

        public Cart? Cart { get; set; }
        public IActionResult Index()
        {
            return View("Cart", Cart = HttpContext.Session.GetJson<Cart>("cart"));
        }
        public IActionResult AddToCart(string masp)
        {
            TDanhMucSp? product = _context.TDanhMucSps
.FirstOrDefault(p => p.MaSp == masp);
            if (product != null)
            {
                Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
                Cart.AddItem(product, 1);
                HttpContext.Session.SetJson("cart", Cart);
            }
            return View("Cart", Cart);
        }
        public IActionResult UpdateCart(string masp)
        {
            TDanhMucSp? product = _context.TDanhMucSps
.FirstOrDefault(p => p.MaSp == masp);
            if (product != null)
            {
                Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
                Cart.AddItem(product, -1);
                HttpContext.Session.SetJson("cart", Cart);
            }
            return View("Cart", Cart);
        }
        public IActionResult RemoveFromCart(string masp)
        {
            TDanhMucSp? product = _context.TDanhMucSps
.FirstOrDefault(p => p.MaSp == masp);
            if (product != null)
            {
                Cart = HttpContext.Session.GetJson<Cart>("cart");
                Cart.RemoveLine(product);
                HttpContext.Session.SetJson("cart", Cart);
            }
            return View("Cart", Cart);
        }

    }
}
