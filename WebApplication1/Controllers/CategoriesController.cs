using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class CategoriesController : Controller
    {
        private static IList<Category> categoryList =
            new List<Category>()
            {
                new Category() { CategoryId = 1, Name = "Keyboard"},
                new Category() { CategoryId = 2, Name = "Monitor"},
                new Category() { CategoryId = 3, Name = "Laptop"},
                new Category() { CategoryId = 4, Name = "Mouse"},
                new Category() { CategoryId = 5, Name = "Printers"}
            };
        
        // GET: Categories
        public ActionResult Index()
        {
            return View(categoryList.OrderBy(c => c.Name) );
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            categoryList.Add(category);
            category.CategoryId = categoryList.Max(c => c.CategoryId) + 1;
            return RedirectToAction("Create");
        }

        public ActionResult Detail(long id)
        {
            var category = categoryList
                .Where(c => c.CategoryId == id)
                .First();

            /* A função acima poderia ser escrita conforme abaixo
             * category = categoryList
                .First(c => c.CategoryId == id);
                */
            return View(category);
        }

        public ActionResult Edit(long id)
        {
            var category = categoryList
                .Where(c => c.CategoryId == id)
                .First();
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category modified)
        {
            var category = categoryList
                .Where(c => c.CategoryId == modified.CategoryId)
                .First();

            category.Name = modified.Name;

            return RedirectToAction("Index");
        }

        public ActionResult Delete(long id)
        {
            var category = categoryList
                .Where(c => c.CategoryId == id)
                .First();
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Category toDelete)
        {
            var category = categoryList
                .Where(c => c.CategoryId == toDelete.CategoryId)
                .First();
            categoryList.Remove(category);

            return RedirectToAction("Index");
        }

    }
}