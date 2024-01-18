using EcommerceWebsite.Infrastructure;
using EcommerceWebsite.Models;
using EcommerceWebsite.Models.Cart;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using NuGet.Protocol.Core.Types;

namespace EcommerceWebsite.Controllers
{
    [Authentication("User")]
    public class CartController : Controller
    {
        
        public Cart? Cart { get; set; }
        private readonly QlbanVaLiContext _context;
        public CartController(QlbanVaLiContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View("Cart", HttpContext.Session.GetJson<Cart>("cart") ?? new Cart());
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
            return RedirectToAction("Index", "Home");
           /* return View("Cart", Cart);*/

        }
        public IActionResult Decrease(string masp)
        {
            TDanhMucSp? product = _context.TDanhMucSps
 .FirstOrDefault(p => p.MaSp == masp);
            if (product != null)
            {
                Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
                if (Cart.Lines.Count > 1)
                {
                    Cart.AddItem(product, -1);
                }
                else
                {
                    Cart.Lines.RemoveAll(x => x.Product.MaSp == masp);
                }
                if (Cart.Lines.Count == 0)
                {
                    HttpContext.Session.Remove("cart");
                }
                else
                {
                    HttpContext.Session.SetJson("cart", Cart);
                }
            }
            return View("Cart", Cart);
        }
        public IActionResult RemoveFromCart(string masp)
        {
            TDanhMucSp? product = _context.TDanhMucSps
 .FirstOrDefault(p => p.MaSp == masp);
            if (product != null)
            {
                Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
                Cart.RemoveLine(product);
                HttpContext.Session.SetJson("cart", Cart);
            }
            return View("Cart", Cart);
        }
        public IActionResult Clear()
        {

            Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
            Cart.Clear();
            HttpContext.Session.Remove("cart");

            return View("Cart", Cart);
        }
    }
}
