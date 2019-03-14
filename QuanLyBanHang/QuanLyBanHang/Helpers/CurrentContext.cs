using System.Web;
using QuanLyBanHang.Models;
using System;
using System.Linq;

namespace QuanLyBanHang.Helpers
{
    public class CurrentContext
    {
        public static object Request { get; private set; }
        public static object Response { get; private set; }

        public static bool IsLogged()
        {
            var flag = HttpContext.Current.Session["isLogin"];
            if (flag == null || (int)flag == 0)
            {
                //Kiểm tra thêm trong cookie
                //nếu có cookie dùng thông tin trong cookie
                //để tái tạo lại session
                if (HttpContext.Current.Request.Cookies["userID"] != null)
                {
                    int userIdCookie = Convert.ToInt32(HttpContext.Current.Request.Cookies["userID"].Value);
                    using (QuanLyBanHangEntities _db = new QuanLyBanHangEntities())
                    {
                        var user = _db.Users
                            .Where(u => u.f_ID == userIdCookie)
                            .FirstOrDefault();
                        HttpContext.Current.Session["isLoggin"] = 1;
                        HttpContext.Current.Session["user"] = user;
                        HttpContext.Current.Response.Cookies["userId"].Expires = DateTime.Now.AddDays(-1);
                    }
                    return true;
                }
                return false;
            }
            return true;
        }
        public static User GetCurUser()
        {
            return (User)HttpContext.Current.Session["user"];
        }
        public static Cart GetCart()
        {
            var ret = (Cart)HttpContext.Current.Session["cart"];
            if (ret == null)
            {
                ret = new Cart();
                HttpContext.Current.Session["cart"] = ret;
            }
            return ret;


        }
        public static void Destroy()
        {
            HttpContext.Current.Session["isLogin"] = 0;
            HttpContext.Current.Session["user"] = null;


            if (HttpContext.Current.Request.Cookies["userID"] != null)
            {

                HttpContext.Current.Request.Cookies["userID"].Expires = DateTime.Now.AddDays(-1);


            }
        }
    }
}
