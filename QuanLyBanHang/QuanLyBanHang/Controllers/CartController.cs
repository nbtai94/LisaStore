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
        // POST: Cart/Remove
        [HttpPost]
        public ActionResult Remove(int proId)
        {
            CurrentContext.GetCart().RemoveItem(proId);
            return RedirectToAction("Index","Cart");
        }
        // POST: Cart/Update
        [HttpPost]
        public ActionResult Update(int proId ,int quantity)
        {
            CurrentContext.GetCart().UpdateItem(proId,quantity);
            return RedirectToAction("Index", "Cart");
        }
        // POST: Cart/Checkout
        [HttpPost]
        public ActionResult Checkout()
        {
            //Khởi tạo đơn hàng
            var ord = new Order
            {
                OrderDate = DateTime.Now,
                UserID = CurrentContext.GetCurUser().f_ID,
                Total = 0,
            };
            var c = CurrentContext.GetCart();
            foreach(var item in c.Items){
                var detail = new OrderDetail
                {
                    ProID = item.Product.ProID,
                    Quantity = item.Quantity,
                    Price = item.Product.Price,
                    Amount = item.Product.Price * item.Quantity,
                };
                ord.OrderDetails.Add(detail);
                ord.Total += detail.Amount;

            }
            _db.Orders.Add(ord);
            _db.SaveChanges();

            CurrentContext.GetCart().Items.Clear();
            return RedirectToAction("Index", "Cart");
        }
    }
}