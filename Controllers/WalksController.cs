using System;
using System.Collections.Generic;
using DogGo.Models;
using DogGo.Models.ViewModels;
using DogGo.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DogGo.Controllers
{
    public class WalksController : Controller
    {
        private readonly IWalkRepository _walksRepo;
        private readonly IDogRepository _dogRepo;
        private readonly IWalkerRepository _walkerRepo;

        
        public WalksController(IWalkRepository walksRepository, IDogRepository dogRepository, IWalkerRepository walkerRepository)
        {
            _walksRepo = walksRepository;
            _dogRepo = dogRepository;
            _walkerRepo = walkerRepository;
        }
        // GET
        public ActionResult Index()
        {
            List<Walks> walks = _walksRepo.GetAllWalks();

            return View(walks);
        }

        // GET
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET
        public ActionResult Create()
        {
            List<Dog> dogs = _dogRepo.GetAllDogs();
            List<Walker> walkers = _walkerRepo.GetAllWalkers();

            WalkFormViewModel vm = new WalkFormViewModel()
            {
                Walk = new Walks(),
                Dogs = dogs,
                Walkers = walkers
            };

            return View(vm);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Walks walk)
        {
            try
            {
                _walksRepo.AddWalk(walk);

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View(walk);
            }
        }

        // GET
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET
        public ActionResult Delete(int id)
        {
            Walks walk = _walksRepo.GetWalkById(id);

            return View(walk);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Walks walk)
        {
            try
            {
                _walksRepo.DeleteWalk(id);

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View(walk);
            }
        }
    }
}