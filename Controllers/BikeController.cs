using BikeShop.AppDbContext;
using BikeShop.Models;
using BikeShop.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.IO;
using Microsoft.AspNetCore.Hosting.Internal;

namespace BikeShop.Controllers
{
    [Authorize(Roles = "Admin,Executive")]
    public class BikeController : Controller
    {
        // 
        private readonly BikeShopDbContext _db;
        //for image
        private readonly HostingEnvironment _hostingEnvironment;

        //when post or retrive it automatically bind the property
        //dont have to exclusively pass it an object on the association method

        [BindProperty]
        public BikeViewModel BikeVM { get; set; }
        public BikeController(BikeShopDbContext db,HostingEnvironment hostingEnvironment)
        {
            _db = db;

            //LINQ


            BikeVM = new BikeViewModel()
            {
                Makes = _db.Makes.ToList(),
                Models = _db.Models.ToList(),
                Bike = new Models.Bike()

            };
            _hostingEnvironment= hostingEnvironment;
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
                //add it for for nvalid not doing anything and just return in the BikeVM
                BikeVM.Makes = _db.Makes.ToList();
                BikeVM.Models = _db.Models.ToList();

                return View(BikeVM);
            }
            _db.Bikes.Add(BikeVM.Bike);
            // UploadImageIfAvailable();

            _db.SaveChanges();



            ////////////////////////bike logic//////////////////////
            ////////////////////////////////////////////////////////



            // Get BikeID we have saved in database
            var BikeID = BikeVM.Bike.Id;

            //Get wwrootPath to save the file on server
            string wwrootPath = _hostingEnvironment.WebRootPath;

            //Get the Uploaded files
            var files = HttpContext.Request.Form.Files;

            //Get the reference of DBSet for the bike we have saved in our database
            var SavedBike = _db.Bikes.Find(BikeID);


            //Upload the file on server and save the path in database if user have submitted file
            if (files.Count != 0)
            {
                var ImagePath = @"images\bike\";
                //Extract the extension of submitted file
                var Extension = Path.GetExtension(files[0].FileName);

                //Create the relative image path to be saved in database table 
                var RelativeImagePath = ImagePath + BikeID + Extension;

                //Create absolute image path to upload the physical file on server
                var AbsImagePath = Path.Combine(wwrootPath, RelativeImagePath);


                //Upload the file on server using Absolute Path
                using (var filestream = new FileStream(AbsImagePath, FileMode.Create))
                {
                    files[0].CopyTo(filestream);
                }

                //Set the path in database
                SavedBike.ImagePath = RelativeImagePath;
                _db.SaveChanges();

               

            }
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
        [HttpPost]
        public IActionResult Delete(int id)
        {
            //why Bike is type here?
            Bike bike = _db.Bikes.Find(id);
            if (bike == null)
            {
                return NotFound();
            }
            _db.Bikes.Remove(bike);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }



  




    }
}
