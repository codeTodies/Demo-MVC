using Demo_MVC.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Demo_MVC.Controllers
{
   
    public class UploadController : Controller
    {
        EV_DBEntities database=new EV_DBEntities();
        // GET: Upload
        public ActionResult Index()
        {
            return View(database.Plants.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Plant plant)
        {
            string fileName = Path.GetFileNameWithoutExtension(plant.UploadImage.FileName);
            string extend=Path.GetExtension(plant.UploadImage.FileName);
            fileName = fileName + extend;
            plant.image = "~/images/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/images/"), fileName);
            plant.UploadImage.SaveAs(fileName);
            database.Plants.Add(plant);
            database.SaveChanges();
            ModelState.Clear();
            return RedirectToAction("Index");
        }
        public ActionResult Details(int id)
        {
            return View(database.Plants.Where(s => s.id == id).FirstOrDefault());
        }
        public ActionResult Edit(int id)
        {
            return View(database.Plants.Where(s => s.id == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Edit(int id, Plant plant)
        {
            if (ModelState.IsValid)
            {
                string fileName = Path.GetFileNameWithoutExtension(plant.UploadImage.FileName);
                string extend = Path.GetExtension(plant.UploadImage.FileName);
                fileName = fileName + extend;
                plant.image = "~/images/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/images/"), fileName);
                plant.UploadImage.SaveAs(fileName);
                database.Entry(plant).State = System.Data.Entity.EntityState.Modified;
                database.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            return View(database.Plants.Where(s => s.id == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Delete(int id, Plant plant)
        {
            try
            {
                plant = database.Plants.Where(s => s.id == id).FirstOrDefault();
                database.Plants.Remove(plant);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Error Delete");
            }
        }

    }
}