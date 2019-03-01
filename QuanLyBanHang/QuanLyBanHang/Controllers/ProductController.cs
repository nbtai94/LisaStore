using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyBanHang.Models;

namespace QuanLyBanHang.Controllers
{
    public class ProductController : Controller
    {
        QuanLyBanHangEntities _db = new QuanLyBanHangEntities();
        // GET: Product ByCat
        public ActionResult ByCat(int? id ,int page=1)
        {
            if (id.HasValue == false)
            {
                return RedirectToAction("Index", "Home");
            }
            //Pagging

            int n = _db.Products.Where(p => p.CatID == id).Count();
            int recordsPerPage = 6;
            int nPages = n / recordsPerPage;
            int m = n % recordsPerPage;
            if (m > 0)
            {
                nPages++;
            }
            ViewBag.Pages = nPages;
            ViewBag.CurPage = page;
            //var lstProduct = _db.Products.Where(p => p.CatID == id).ToList();
            var lstProduct = _db.Products.Where(p => p.CatID == id)
                .OrderBy(p => p.ProID)
                .Skip((page - 1) * recordsPerPage)
                .Take(recordsPerPage)
                .ToList();
            return View(lstProduct);
        }
        public ActionResult Detail(int? id)
        {
            if (id.HasValue==false)
            {
                return RedirectToAction("ByCat", "Product");
            }
            Product model = _db.Products.Where(p => p.ProID == id).FirstOrDefault();
            return View(model);
        }

    }

}