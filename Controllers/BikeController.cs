using BikeShop.AppDbContext;
using BikeShop.Models;
using BikeShop.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BikeShop.Controllers
{
    [Authorize(Roles = "Admin,Executive")]
    public class BikeController : Controller
    {
        // 
        private readonly BikeShopDbContext _db;

        //when post or retrive it automatically bind the property
        //dont have to exclusively pass it an object on the association method

        [BindProperty]
        public BikeViewModel BikeVM { get; set; }
        public BikeController(BikeShopDbContext db)
        {
            _db = db;

            //LINQ


            BikeVM = new BikeViewModel()
            {
                Makes = _db.Makes.ToList(),
                Models = _db.Models.ToList(),
                Bike = new Models.Bike()

            };
        }
        public IActionResult Index()
        {
            //we just need the name of make and model associated that make
            //we dont need complte  list of makes for our index page
            //we dont need veiw model for our index method
            //we can get the reference of makes associted to any model using eagerloading
            //eagerloading is a process of entity framework whereby a query for one type entity
            //also load related entity as part of the query and we dont need to execute a separate qurey for ralated entities
            //as we already have foreign key reference to make in our model entity we can use eagerloading to load associated makes in our model entity 

            var Bikes = _db.Bikes.Include(m => m.Make).Include(m=>m.Model);
            return View(Bikes.ToList());
        }

        //httpget

        public IActionResult Create()
        {
            return View(BikeVM);
        }
        [HttpPost, ActionName("Create")]
       // [ValidateAntiForgeryToken]
        public IActionResult CreatePost()
        {
            if (!ModelState.IsValid)
            {
                return View(BikeVM);
            }
            _db.Bikes.Add(BikeVM.Bike);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));

        }

        //public IActionResult Edit(int id)
        //{
        //    ModelVM.Model = _db.Models.Include(m => m.Make).SingleOrDefault(m => m.Id == id);
        //    if (ModelVM == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(ModelVM);
        //}

        //[HttpPost]
        //public IActionResult Edit()
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(ModelVM);

        //    }
        //    _db.Update(ModelVM.Model);
        //    _db.SaveChanges();
        //    return RedirectToAction(nameof(Index)); ;

        //}
        //[HttpPost]
        //public IActionResult Delete(int id)
        //{
        //    Model model = _db.Models.Find(id);
        //    if (model == null)
        //    {
        //        return NotFound();
        //    }
        //    _db.Models.Remove(model);
        //    _db.SaveChanges();
        //    return RedirectToAction(nameof(Index));
        //}




    }
}
