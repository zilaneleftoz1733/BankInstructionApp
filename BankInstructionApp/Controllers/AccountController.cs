using BankInstructionApp.Models;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace BankInstructionApp.Controllers
{
    public class AccountController : Controller
    {
        private Context db = new Context();

        
        public ActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignIn(LoginViewModel model)
        {


            if (ModelState.IsValid)
            {
                var user = db.Users.FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(user.Email, false);
                    return RedirectToAction("BankInstructionApp", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Geçersiz e-posta veya şifre.");
                }
            }
            return View(model);
        }

       
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var existingUser = db.Users.FirstOrDefault(u => u.Email == model.Email);
                if (existingUser == null)
                {
                    var newUser = new User
                    {
                        UserName = model.UserName,
                        Email = model.Email,
                        Password = model.Password
                        
                    };
                    db.Users.Add(newUser);
                    db.SaveChanges();

                    FormsAuthentication.SetAuthCookie(newUser.Email, false);
                    return RedirectToAction("BankInstructionApp", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Bu e-posta adresi zaten kullanılıyor.");
                }
            }
            return View(model);
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("SignIn", "Account");
        }
    }
}
