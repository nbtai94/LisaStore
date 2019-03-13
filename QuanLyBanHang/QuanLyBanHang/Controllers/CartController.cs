using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyBanHang.Models;
using QuanLyBanHang.Helpers;
using QuanLyBanHang.Filter;

namespace QuanLyBanHang.Controllers
{
    
    public class CartController : Controller
    {
        QuanLyBanHangEntities _db = new QuanLyBanHangEntities();
        [CheckLogin]
        // GET: Cart
        public ActionResult Index()
        {
            var cart = CurrentContext.GetCart();
            return View(cart.Items);
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
            return RedirectToAction("Detail","Product",new {id=proId });
        }
        // POST: Cart/Add2
        [HttpPost]
        public ActionResult Add2(int proId, int quantity ,int curPage)
        {
            var pro = _db.Products.Where(p => p.ProID == proId).FirstOrDefault();

            var item = new CartItem
            {
                Quantity = quantity,
                Product = pro
            };
            CurrentContext.GetCart().AddItem(item);
            return RedirectToAction("ByCat", "Product", new { id = pro.CatID ,page= curPage});
        }

    }
}