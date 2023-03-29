using DogGo.Models;
using DogGo.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace DogGo.Controllers
{
    public class DogsController : Controller
    {
        private IDogRepository _dogRepo;

        public DogsController(IDogRepository dogRepository)
        {
            _dogRepo = dogRepository;
        }

        public ActionResult Index()
        {
            List<Dog> dogs = _dogRepo.GetAllDogs();
            return View(dogs);
        }
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Dog dog)
        {
            try
            {
                _dogRepo.AddDog(dog);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(dog);
            }
        }

        public ActionResult Edit(int id)
        {
            Dog dog = _dogRepo.GetDogById(id);
            if (dog == null) 
            {
                return NotFound();
            }
            return View(dog);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit(int id, Dog dog)
        {
            try
            {
                _dogRepo.UpdateDog(dog);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(dog);
            }
        }

        public ActionResult Delete(int id)
        {
            Dog dog = _dogRepo.GetDogById(id);
            return View(dog);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Dog dog)
        {
            try
            {
                _dogRepo.DeleteDog(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(dog);
            }
        }
    }
}
