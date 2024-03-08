#nullable disable
using Business.Models;
using Business.Services;
using DataAccess.Results.Bases;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC.Controllers.Bases;

//Generated from Custom Template.
namespace MVC.Controllers
{
    public class PetsController : MvcControllerBase
    {
        // TODO: Add service injections here
        private readonly IPetService _petService;
        private readonly ISpeciesService _speciesService;

        public PetsController(IPetService petService, ISpeciesService speciesService)
        {
            _petService = petService;
            _speciesService = speciesService;
        }

        // GET: Pets
        public IActionResult Index()
        {
            List<PetModel> petList = _petService.GetList(); // TODO: Add get collection service logic here
            return View(petList);
        }

        // GET: Pets/Details/5
        public IActionResult Details(int id)
        {
            PetModel pet = _petService.GetItem(id); // TODO: Add get item service logic here
            if (pet == null)
            {
                // Way 1:
                //return NotFound();
                // Way 2:
                return View("Error", "Pet not found!");
            }
            return View(pet);
        }

        // GET: Pets/Create
        public IActionResult Create()
        {
            // TODO: Add get related items service logic here to set ViewData if necessary
            ViewData["SpeciesId"] = new SelectList(_speciesService.Query().ToList(), "Id", "Name");
            PetModel model = new PetModel()
            {
                BirthDate = DateTime.Today // without time part, alternative: DateTime.Now with time part
            };
            return View(model);
        }

        // POST: Pets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PetModel pet)
        {
            if (ModelState.IsValid)
            {
                // TODO: Add insert service logic here
                _petService.Add(pet);
                return RedirectToAction(nameof(Details), new { id = pet.Id });
            }
            // TODO: Add get related items service logic here to set ViewData if necessary
            ViewData["SpeciesId"] = new SelectList(_speciesService.Query().ToList(), "Id", "Name");
            return View(pet);
        }

        // GET: Pets/Edit/5
        public IActionResult Edit(int id)
        {
            PetModel pet = _petService.GetItem(id); ; // TODO: Add get item service logic here
            if (pet == null)
            {
                return View("Error", "Pet not found!");
            }
            // TODO: Add get related items service logic here to set ViewData if necessary
            ViewData["SpeciesId"] = new SelectList(_speciesService.Query().ToList(), "Id", "Name");
            return View(pet);
        }

        // POST: Pets/Edit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PetModel pet)
        {
            if (ModelState.IsValid)
            {
                // TODO: Add update service logic here
                Result result = _petService.Update(pet);
                if (result.IsSuccessful)
                    return RedirectToAction(nameof(Details), new { id = pet.Id });
                ModelState.AddModelError("", result.Message);
            }
            // TODO: Add get related items service logic here to set ViewData if necessary
            ViewData["SpeciesId"] = new SelectList(_speciesService.Query().ToList(), "Id", "Name");
            return View(pet);
        }

        // GET: Pets/Delete/5
        public IActionResult Delete(int id)
        {
            PetModel pet = _petService.GetItem(id); // TODO: Add get item service logic here
            if (pet == null)
            {
                return View("Error", "Pet not found!");
            }
            return View(pet);
        }

        // POST: Pets/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // TODO: Add delete service logic here
            Result result = _petService.Delete(id);
            if (result.IsSuccessful)
                return RedirectToAction(nameof(Index));
            return View("Error", result.Message);
        }
	}
}
