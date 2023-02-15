using BulkyBook.Data;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBook.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> categoryList = _db.Categories;

            return View(categoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category Created!";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        // GET Category By Id
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var obj = _db.Categories.FirstOrDefault(c => c.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        //PUT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Category Updated!";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        // Get
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var obj = _db.Categories.FirstOrDefault(c => c.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        //Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Category obj)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Remove(obj);
                _db.SaveChanges();
                TempData["success"] = "Category Deleted!";
                return RedirectToAction("Index");
            }
            return View(obj);
        }
    }
}