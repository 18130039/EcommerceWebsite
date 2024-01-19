using EcommerceWebsite.Models;
using EcommerceWebsite.Models.ProductModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using X.PagedList;

namespace EcommerceWebsite.Controllers
{
    [Authentication("User")]
    public class HomeController : Controller
    {
        QlbanVaLiContext db = new QlbanVaLiContext();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        /* [Authentication]*/
        public IActionResult Index(int? page)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var listProduct = db.TDanhMucSps.AsNoTracking().OrderBy(x => x.GiaNhoNhat);
            PagedList<TDanhMucSp> listP = new PagedList<TDanhMucSp>(listProduct, pageNumber, pageSize);
            return View(listP);
        }
        public IActionResult SanPhamTheoLoai(String maloai, int? page)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var listProduct = db.TDanhMucSps.AsNoTracking().Where(x => x.MaLoai == maloai).OrderBy(x => x.GiaNhoNhat);
            PagedList<TDanhMucSp> listP = new PagedList<TDanhMucSp>(listProduct, pageNumber, pageSize);
            ViewBag.maloai = maloai;
            return View(listP);
        }
        public IActionResult ChiTietSanPham(string masp)
        {
            var sanPham = db.TDanhMucSps.SingleOrDefault(x => x.MaSp == masp);
            var anhSanPham = db.TAnhSps.Where(x => x.MaSp == masp).ToList();
            ViewBag.anhSanPham = anhSanPham;
            return View(sanPham);
        }
        [HttpPost]
        public async Task<ActionResult> AutoComplete(string term)
        {
            using (HttpClient client = new HttpClient())
            {
                // Make a request to the API endpoint
                HttpResponseMessage response = await client.GetAsync("https://localhost:7135/api/productapi");

                if (response.IsSuccessStatusCode)
                {
                    // Read the response content
                    string json = await response.Content.ReadAsStringAsync();

                    // Deserialize the JSON response
                    var products = JsonConvert.DeserializeObject<List<Product>>(json);

                    // Filter products based on the search term
                    var filteredProducts = products
                        .Where(p => p.TenSp.Contains(term))
                        .Select(p => new
                        {
                            label = p.TenSp,
                            value = p.TenSp,
                            anhDaiDien = p.AnhDaiDien,
                            giaNhoNhat = p.GiaNhoNhat,
                            maSp = p.MaSp
                        })
                        .ToList();

                    return Json(filteredProducts);
                }
                else
                {
                    // Handle the case where the API request fails
                    return BadRequest();
                }
            }
        }

    }
}
