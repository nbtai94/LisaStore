using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyBanHang.Models;
using QuanLyBanHang.Helpers;

namespace QuanLyBanHang.Controllers
{
    
    public class CartController : Controller
    {
        QuanLyBanHangEntities _db = new QuanLyBanHangEntities();
        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }
        // POST: Cart/Add
        [HttpPost]
        public ActionResult Add(int proId,int quantity)
        {
            var pro = _db.Products.Where(p => p.ProID == proId).FirstOrDefault();

            var item = new CartItem {
                Quantity = quantity,
                Product = pro
            };
            CurrentContext.GetCart().AddItem(item);
            return View();
        }
    }
}