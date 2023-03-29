using DogGo.Models;
using DogGo.Models.ViewModels;
using DogGo.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace DogGo.Controllers
{
    public class OwnersController : Controller
    {
        private readonly IOwnerRepository _ownerRepo;
        private readonly IDogRepository _dogRepo;
        private readonly IWalkerRepository _walkerRepo;
        private readonly INeighborhoodRepository _neighborhoodRepo;


        public OwnersController(
            IOwnerRepository ownerRepository,
            IDogRepository dogRepository,
            IWalkerRepository walkerRepository,
            INeighborhoodRepository neighborhoodRepository)
        {
            _ownerRepo = ownerRepository;
            _dogRepo = dogRepository;
            _walkerRepo = walkerRepository;
            _neighborhoodRepo = neighborhoodRepository;
        }

        public ActionResult Index()
        {
            List<Owner> owners = _ownerRepo.GetAllOwners();
            return View(owners);
        }

        public ActionResult Details(int id)
        {
            ProfileViewModel vm = new ProfileViewModel();
            Owner owner = _ownerRepo.GetOwnerById(id);
            List<Walker> walkers = new List<Walker>();
            List<Dog> dogs = new List<Dog>();
            vm.Owner = owner;
            vm.Walkers = _walkerRepo.GetAllWalkers();
            vm.Dogs = _dogRepo.GetAllDogs();
            if (owner == null) 
            {
                return NotFound();
            }
            return View(vm);
        }
        // GET: Owners/Create
        public ActionResult Create()
        {
            List<Neighborhood> neighborhoods = _neighborhoodRepo.GetAll();

            OwnerFormViewModel vm = new OwnerFormViewModel()
            {
                Owner = new Owner(),
                Neighborhoods = neighborhoods
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // POST: Owners/Create
        public ActionResult Create(Owner owner)
        {
            try
            {
                _ownerRepo.AddOwner(owner);

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View(owner);
            }
        }
        public ActionResult Edit(int id)
        {
            Owner owner = _ownerRepo.GetOwnerById(id);
            List<Neighborhood> neighborhoods = _neighborhoodRepo.GetAll();
            ProfileViewModel vm = new ProfileViewModel()
            {
                Owner = owner,
                Neighborhoods = neighborhoods
            };
            if (owner == null)
            {
                return NotFound(); 
            }
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Owner owner)
        {
            try
            {
                _ownerRepo.UpdateOwner(owner);
                return RedirectToAction("Index");
            }
            catch(Exception)
            {
                return View(owner);
            }
        }
        public ActionResult Delete(int id)
        {
            Owner owner = _ownerRepo.GetOwnerById(id);
            return View(owner);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Owner owner)
        {
            try
            {
                _ownerRepo.DeleteOwner(id);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception)
            {
                return View(owner);
            }
        }

        
    }
}
