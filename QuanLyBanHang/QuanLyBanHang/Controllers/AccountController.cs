using QuanLyBanHang.Models;
using System;
using System.Web.Mvc;
using QuanLyBanHang.Helpers;
using System.Linq;
using QuanLyBanHang.Filter;

namespace QuanLyBanHang.Controllers
{
    public class AccountController : Controller
    {
        private QuanLyBanHangEntities _db = new QuanLyBanHangEntities();

      

        // GET: Account/Register
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        //// Post: Account/Register
        public ActionResult Register(RegisterModel model)
        {
            User u = new User
            {
                f_Username = model.Username,
                f_Email = model.Email,
                f_Name = model.Name,
                f_Password = StringUtils.Md5(model.Password),
                f_Permission = 0,
                f_DOB = DateTime.ParseExact(model.DOB, "dd/mm/yyyy", null)
            };
            _db.Users.Add(u);
            _db.SaveChanges();
            return View();
        }
        // GET: Account/Login
        public ActionResult Login()
        {
            return View();
        }
        // POST: Account/Login
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            string encPwd = StringUtils.Md5(model.Password);
            var user = _db.Users
                .Where(u => u.f_Username == model.Username && u.f_Password == encPwd)
                .FirstOrDefault();
            if(user != null)
            {
                Session["isLogin"] = 1;
                Session["user"] = user;
                if (model.Remember)
                {
                    Response.Cookies["x"].Value = user.f_ID.ToString();
                    Response.Cookies["x"].Expires = DateTime.Now.AddDays(7);
                }

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.ErrorMsg = "Đăng nhập thất bại";
                return View();
            }
            
        }
        //POST: Account/Logout
        [HttpPost]
        public ActionResult Logout()
        {
            CurrentContext.Destroy();
            return RedirectToAction("Index", "Home");
        }


        //GET: Account/Profile
        [CheckLogin]
        public ActionResult Profile()
        {        
     
            return View();
        }

    }
}