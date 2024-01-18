using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EcommerceWebsite.Models;
using EcommerceWebsite.Models.ProductModel;
namespace EcommerceWebsite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductAPIController : ControllerBase
    {
        QlbanVaLiContext db = new QlbanVaLiContext();
        [HttpGet]
        public IEnumerable<Product> GetAllProducts()
        {
            var products = from p in db.TDanhMucSps
                           select
                           new Product
                           {
                               MaSp = p.MaSp,
                               MaLoai = p.MaLoai,
                               TenSp = p.TenSp,
                               AnhDaiDien = p.AnhDaiDien,
                               GiaNhoNhat = p.GiaNhoNhat

                           };
            return products.ToList();
        }
        [HttpGet("{maloai}")]
        public IEnumerable<Product> GetProductsByCatagory(string maloai)
        {
            var products = from p in db.TDanhMucSps
                           where(p.MaLoai == maloai)
                           select
                           new Product
                           {
                               MaSp = p.MaSp,
                               MaLoai = p.MaLoai,
                               TenSp = p.TenSp,
                               AnhDaiDien = p.AnhDaiDien,
                               GiaNhoNhat = p.GiaNhoNhat

                           };
            return products.ToList();
        }
    }
}
