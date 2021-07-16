using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using product_manager.Data;
using product_manager.Models;
namespace product_manager.Controllers
{
    public class ProductCategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ProductCategoryController(ApplicationDbContext context)
        {
            this._db = context;

        }
        // GET: ProductCategoryController
        public ActionResult Index()
        {
            IEnumerable<ProductCategory> productCategories = this._db.ProductCategories;
            
            return View(productCategories);
        }

        // GET: ProductCategoryController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductCategoryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductCategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductCategory obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    this._db.ProductCategories.Add(obj);
                    this._db.SaveChanges();
                    return RedirectToAction("Index");

                }

                return View(obj);
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductCategoryController/Edit/5
        public ActionResult Edit(int id)
        {
          
                var product = this._db.ProductCategories.Find(id);

           
                if (product == null) {
                    return NotFound();
                }
                return View(product);
           
        }

        // POST: ProductCategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ProductCategory obj)
        {

                this._db.ProductCategories.Update(obj);
                this._db.SaveChanges();

                return RedirectToAction("Index");
           
        }

        // GET: ProductCategoryController/Delete/5
        public ActionResult DeleteView(int id)
        {
           
                var product = this._db.ProductCategories.Find(id);
                if (product == null) {
                    return NotFound();
                }
            return View("Delete",product);

            
        }

        // POST: ProductCategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                var product = this._db.ProductCategories.Find(id);

                if (product == null)
                {
                    return NotFound();
                }

                this._db.ProductCategories.Remove(product);
                this._db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View("Error");
            }
        }
    }
}
