using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyBanHang.Models;

namespace QuanLyBanHang.Controllers
{
    public class CategoryController : Controller
    {
        QuanLyBanHangEntities _db = new QuanLyBanHangEntities();
        // GET: Category
        public ActionResult Index()
        {
            var lstCategory = _db.Categories.ToList();
            //ViewBag.List = lstCategory;
            return View(lstCategory);
        }
        public ActionResult Detail(int id)
        {
            var lstCategory = _db.Categories.Where(i => i.CatID == id).FirstOrDefault();
            return View(lstCategory);
        }
        //GET Add Category
        public ActionResult Add()
        {
            return View();
        }
        //POST Add Category
        [HttpPost]
        public ActionResult Add(Category model)
        {
            _db.Categories.Add(model);
            _db.SaveChanges();
            return View();
        }

        //GET: Delete Category
        public ActionResult Delete(int? id)
        {
            if (id.HasValue == false)
            {
                return RedirectToAction("Index","Category");
            }
            ViewBag.Id = id;
          
            return View();
        }
        //POST: Delete Category
        [HttpPost]
        public ActionResult Delete(int idToDelete)
        {
            var model = _db.Categories.Where(i => i.CatID == idToDelete).FirstOrDefault();
            _db.Categories.Remove(model);
            _db.SaveChanges();

            return RedirectToAction("Index", "Category");
        }
        //GET: Update Category
        public ActionResult Edit(int? id)
        {
            if (id.HasValue==false)
            {
                return RedirectToAction("Index", "Category");
            }
            Category model = _db.Categories.Where(c => c.CatID == id).FirstOrDefault();
            return View(model);
        }
        //POST: Update Category
        [HttpPost]
        public ActionResult Update(Category model)
        {
            _db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index", "Category");
        }


        public ActionResult List()
        {
            List<Category> lst = _db.Categories.ToList();
            return PartialView("ListPartial",lst);
        }

    }
}