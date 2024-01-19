using EcommerceWebsite.Infrastructure;
using EcommerceWebsite.Models;

using EcommerceWebsite.Models.Cart;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceWebsite.Controllers
{
    [Authentication("User")]
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
        public IActionResult Checkout()
        {
            Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
            if (ModelState.IsValid)
            {
                var userId = HttpContext.Session.GetString("UserName");
                var hdId = Guid.NewGuid().ToString("N").Substring(0, 25);
                var tHoaDonBan = new THoaDonBan
                {
                    MaHoaDon = hdId,
                    NgayHoaDon = DateTime.Now,
                    MaKhachHang = userId,
                    MaNhanVien = "NV1",
                    TongTienHd = Cart.ComputeTotalValue()
                };

               
                foreach (var cart in Cart.Lines)
                {   //Neu maChiTietSp khong ton tai trong TChiTietSanPhams thi tao moi theo guid
                    var maChiTietSp = _context.TChiTietSanPhams
                        .Where(x => x.MaSp == cart.Product.MaSp)
                        .Select(x => x.MaChiTietSp)
                        .FirstOrDefault();
                    if (maChiTietSp == null)
                    {
                        // Create a new MaChiTietSp using GUID
                        maChiTietSp = Guid.NewGuid().ToString("N").Substring(0, 25);

                        
                        var newChiTietSp = new TChiTietSanPham
                        {
                            MaSp = cart.Product.MaSp,
                            MaChiTietSp = maChiTietSp
                        };

                        // Add the newChiTietSp to the context and save changes
                        _context.TChiTietSanPhams.Add(newChiTietSp);
                        _context.SaveChanges();
                    }
                    //Neu dongiaban chua ton tai trong chi tiet san pham thi lay gialonnhat hoac gianhonhat
                    var donGiaBan = (decimal?)_context.TChiTietSanPhams
                        .Where(x => x.MaSp == cart.Product.MaSp)
                        .Select(x => x.DonGiaBan)
                        .FirstOrDefault();

                    if (donGiaBan == null)
                    {
                        donGiaBan = cart.Product.GiaLonNhat;
                    }
                    else
                    {
                        donGiaBan = cart.Product.GiaNhoNhat;
                    }
                    var tChitiethdb = new TChiTietHdb
                    {
                        MaHoaDon = hdId,
                        MaChiTietSp = maChiTietSp,
                        DonGiaBan = donGiaBan,
                        SoLuongBan = cart.Quantity
                    };
                    tHoaDonBan.TChiTietHdbs.Add(tChitiethdb);
                }
                _context.THoaDonBans.Add(tHoaDonBan);
                _context.SaveChanges();

                HttpContext.Session.Remove("cart"); ;
                return RedirectToAction("Success", "Checkout");
            }
            return View();
        }
        public IActionResult Success() { return View("CheckoutSuccess"); }
    }
   
}
