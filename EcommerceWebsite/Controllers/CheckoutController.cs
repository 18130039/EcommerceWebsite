using EcommerceWebsite.Infrastructure;
using EcommerceWebsite.Models;
using EcommerceWebsite.Models.Authentication;
using EcommerceWebsite.Models.Cart;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceWebsite.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly QlbanVaLiContext _context;

        public CheckoutController(QlbanVaLiContext context)
        {
            _context = context;
        }
        /*        public IActionResult Checkout()
                {
                    return View("Checkout");
                }*/
        [Authentication]
        public IActionResult Checkout()
        {
            if (ModelState.IsValid)
            {


                var userId = HttpContext.Session.GetString("UserName");
                string hdId = Guid.NewGuid().ToString("N").Substring(0, 25);
                var tHoaDonBan = new THoaDonBan();
                tHoaDonBan.MaHoaDon = hdId;
                tHoaDonBan.NgayHoaDon = DateTime.Now;
                tHoaDonBan.MaKhachHang = userId;
                tHoaDonBan.MaNhanVien = "NV1";
                _context.THoaDonBans.Add(tHoaDonBan);
                _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}
