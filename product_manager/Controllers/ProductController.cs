using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using product_manager.Data;
using product_manager.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using product_manager.Models.VM;

namespace product_manager.Controllers
{
    public class ProductController : Controller
    {

        private readonly ApplicationDbContext _db;

        public ProductController(ApplicationDbContext context)
        {
            this._db = context;
        }


        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<Product> products = this._db.Products;

            foreach(var item in products.ToList())
            {
                item.ProductCategory = this._db.ProductCategories.FirstOrDefault(c => c.Id == item.ProductCategoryId);
            }
            return View(products);
        }

        [HttpGet]
        public IActionResult Create() {

            /*IEnumerable<SelectListItem> TypeDropdown = this._db.ProductCategories.Select(i => new SelectListItem
              {
                  Text = i.Name,
                  Value = i.Id.ToString()
              }
              );

              ViewBag.TypeDropdown = TypeDropdown;
            */

            ProductVM productVM = new()
            {
                Product = new Product(),
                ProductCategory = this._db.ProductCategories.Select(i => new SelectListItem {
                    Text=i.Name,
                    Value=i.Id.ToString()
                }),
            };



            return View(productVM);
        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductVM obj) {
           
            if (ModelState.IsValid) {

                this._db.Products.Add(obj.Product);
                this._db.SaveChanges();

                return RedirectToAction("Index");
            }
                return View(obj);
        }



        [HttpGet]
        public IActionResult Edit(int id) {




            var product = this._db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            ProductVM productVM = new()
            {
                Product = product,
                ProductCategory = this._db.ProductCategories.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };

            return View(productVM);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Product product)
        {
            
            if (ModelState.IsValid)
            {
                this._db.Products.Update(product);
                this._db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }



        [HttpGet]
        public IActionResult DeleteView(int id)
        {

            var product = this._db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            return View("Delete",product);

        }

        [HttpPost]
        public IActionResult Delete(int id) {

            var product = this._db.Products.Find(id);
            if (product == null) {
                return NotFound();
            }

            this._db.Products.Remove(product);
            this._db.SaveChanges();

            return RedirectToAction("Index");
        
        }


    }
}
