using EcommerceWebsite.Models;
using EcommerceWebsite.Models.Signup;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceWebsite.Controllers
{
    public class AccessController : Controller
    {
        QlbanVaLiContext db = new QlbanVaLiContext();
        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }
        [HttpPost]
        public IActionResult Login(TUser user)
        {
            var username = HttpContext.Session.GetString("UserName");
            if (HttpContext.Session.GetString("UserName") == null)
            {
                var u = db.TUsers.Where(x => x.Username.Equals(user.Username) && x.Password.Equals(user.Password)).FirstOrDefault();
                if (u != null)
                {
                    HttpContext.Session.SetString("UserName", db.TKhachHangs.Where(x=>x.Username == u.Username).Select(x=>x.TenKhachHang).FirstOrDefault());
                    HttpContext.Session.SetString("UserRole", (u.LoaiUser == 0) ? "Admin" : "User");
                    if (u.LoaiUser == 0)
                    {
                        // Redirect to the admin page
                        return RedirectToAction("Index", "Admin");
                    }
                    else if (u.LoaiUser == 1)
                    {
                        // Redirect to the home page
                        return RedirectToAction("Index", "Home");
                    }

                }
            }
            return View();
        }
        public IActionResult Unauthorized()
        {
            return View("Unauthorized");
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("UserName");
            return RedirectToAction("Login", "Access");
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(SignUpViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (db.TUsers.Any(u => u.Username == model.Username))
                {
                    ModelState.AddModelError("Username", "Username is already taken.");
                    return View(model);
                }
                var user = new TUser
                {
                    Username = model.Username,
                    Password = model.Password,
                    LoaiUser = 1
                };
                var khachHang = new TKhachHang
                {
                    MaKhachHang = model.Username,
                    Username = model.Username,
                    TenKhachHang = model.Name,
                    NgaySinh = model.Birthday,
                    SoDienThoai = model.Phone

                    // Set oer properties as needed
                };
                user.TKhachHangs.Add(khachHang);
                db.TUsers.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
    }

  


}
