using EcommerceWebsite.Infrastructure;
using EcommerceWebsite.Models.Cart;
using EcommerceWebsite.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceWebsite.ViewComponents
{
    public class CartWidget: ViewComponent
    {
        public IViewComponentResult Invoke()
        {
           
            return View(HttpContext.Session.GetJson<Cart>("cart"));
        }
    }
}
