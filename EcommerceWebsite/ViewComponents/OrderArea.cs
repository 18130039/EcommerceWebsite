using EcommerceWebsite.Infrastructure;
using EcommerceWebsite.Models;
using EcommerceWebsite.Models.Cart;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceWebsite.ViewComponents
{
    public class OrderArea : ViewComponent
    {
        public IViewComponentResult Invoke()
        {

            return View(HttpContext.Session.GetJson<Cart>("cart"));
        }
    }
}
