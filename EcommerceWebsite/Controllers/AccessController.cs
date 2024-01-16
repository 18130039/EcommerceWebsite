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
            if (HttpContext.Session.GetString("UserName") == null)
            {
                var u = db.TUsers.Where(x => x.Username.Equals(user.Username) && x.Password.Equals(user.Password)).FirstOrDefault();
                if (u != null)
                {
                    HttpContext.Session.SetString("UserName", u.Username.ToString());
                    return RedirectToAction("Index", "Home");

                }
            }
            return View();
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

    /*       [HttpPost]
           [ValidateAntiForgeryToken]
           public async Task<IActionResult> Register(SignUpViewModel model)
           {
               if (!ModelState.IsValid)
               {
                   // Log ModelState errors for debugging
                   foreach (var modelState in ModelState.Values)
                   {
                       foreach (var error in modelState.Errors)
                       {
                           // Log or print error messages
                           Console.WriteLine(error.ErrorMessage);
                       }
                   }

                   return View(model);
               }

               // Check if the username is already taken
               if (await db.TUsers.AnyAsync(u => u.Username == model.Username))
               {
                   ModelState.AddModelError("Username", "Username is already taken.");
                   return View(model);
               }

               // Create a new TUser instance
               var newUser = new TUser
               {
                   Username = model.Username,
                   Password = model.Password,
                   LoaiUser = 1
               };

               // Create a new TKhachHang instance
               var newCustomer = new TKhachHang
               {
                   MaKhachHang = Guid.NewGuid().ToString(), // Generate a unique identifier for MaKhachHang
                   Username = model.Username,
                   // Set other properties as needed
                   TenKhachHang = model.Name,
                   NgaySinh = model.Birthday,
                   SoDienThoai = model.Phone

               };

               // Save entities to the database
               db.TUsers.Add(newUser);
               db.TKhachHangs.Add(newCustomer);

               try
               {
                   var result = await db.SaveChangesAsync();

                   // Log or print the result for debugging
                   Console.WriteLine($"Number of entries written to the database: {result}");

                   await db.SaveChangesAsync();
                   return RedirectToAction("Index", "Home"); // Redirect to home or login page
               }
               catch (Exception ex)
               {

                   // Handle exception
                   return View(model);
               }
           }*/



}
