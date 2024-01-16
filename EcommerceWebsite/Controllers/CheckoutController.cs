using EcommerceWebsite.Infrastructure;
using EcommerceWebsite.Models;
using EcommerceWebsite.Models.Authentication;
using EcommerceWebsite.Models.Cart;
using EcommerceWebsite.Models.Checkout;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceWebsite.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly QlbanVaLiContext _context;
        public Cart? Cart { get; set; }
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
                var hdId = Guid.NewGuid().ToString("N").Substring(0, 25);
                var tHoaDonBan = new THoaDonBan
                {
                    MaHoaDon = hdId,
                    NgayHoaDon = DateTime.Now,
                    MaKhachHang = userId,
                    MaNhanVien = "NV1"
                };
                Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
                foreach (var cart in Cart.Lines)
                {
                    
                    var tChitiethdb = new TChiTietHdb
                    {
                        MaHoaDon = hdId,
                        MaChiTietSp = _context.TChiTietSanPhams.Where(x=>x.MaSp == cart.Product.MaSp).Select(x=>x.MaChiTietSp).FirstOrDefault(),
                        DonGiaBan = (decimal?)_context.TChiTietSanPhams.Where(x => x.MaSp == cart.Product.MaSp).Select(x => x.DonGiaBan).FirstOrDefault(),
                        SoLuongBan = cart.Quantity
                    };
                    tHoaDonBan.TChiTietHdbs.Add(tChitiethdb);
                }
                _context.THoaDonBans.Add(tHoaDonBan);
                _context.SaveChanges();


                return RedirectToAction("Index", "Home");
                /*                var tChitiethdb = new TChiTietHdb();
                                tChitiethdb.MaHoaDon = hdId;
                                tChitiethdb.MaChiTietSp = "cad20230001br";
                                tChitiethdb.DonGiaBan = 12312313;
                                tChitiethdb.SoLuongBan = 3;
                                _context.TChiTietHdbs.Add(tChitiethdb);
                                _context.SaveChanges();

                                return RedirectToAction("Index", "Home");*/
            }
            return View();
        }
    }
}
