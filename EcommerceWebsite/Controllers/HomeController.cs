using EcommerceWebsite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using X.PagedList;

namespace EcommerceWebsite.Controllers
{
    public class HomeController : Controller
    {
        QlbanVaLiContext db = new QlbanVaLiContext();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(int? page)
        {
            int pageSize = 8;
            int pageNumber = page==null || page<0 ? 1 : page.Value;
            var listProduct = db.TDanhMucSps.AsNoTracking().OrderBy(x =>x.TenSp);
            PagedList<TDanhMucSp> listP = new PagedList<TDanhMucSp>(listProduct,pageNumber,pageSize);
            return View(listP);
        }
        public IActionResult SanPhamTheoLoai(String maloai, int? page)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var listProduct = db.TDanhMucSps.AsNoTracking().Where(x=>x.MaLoai==maloai).OrderBy(x => x.TenSp);
            PagedList<TDanhMucSp> listP = new PagedList<TDanhMucSp>(listProduct, pageNumber, pageSize);
            ViewBag.maloai = maloai;
            return View(listP);
        }
        public IActionResult ChiTietSanPham(string masp)
        {
            var sanPham = db.TDanhMucSps.SingleOrDefault(x => x.MaSp==masp);
            var anhSanPham = db.TAnhSps.Where(x => x.MaSp == masp).ToList();
            ViewBag.anhSanPham = anhSanPham;
            return View(sanPham);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
