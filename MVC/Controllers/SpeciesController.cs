#nullable disable
using Business.Models;
using Business.Services;
using DataAccess.Results.Bases;
using Microsoft.AspNetCore.Mvc;

//Generated from Custom Template.
namespace MVC.Controllers
{
    public class SpeciesController : Controller
    {
        // TODO: Add service injections here
        private readonly ISpeciesService _speciesService;

        public SpeciesController(ISpeciesService speciesService)
        {
            _speciesService = speciesService;
        }

        // GET: Species
        public IActionResult Index()
        {
            List<SpeciesModel> speciesList = _speciesService.Query().ToList(); // TODO: Add get collection service logic here
            return View(speciesList);
        }

        // GET: Species/Details/5
        public IActionResult Details(int id)
        {
            SpeciesModel species = _speciesService.Query().SingleOrDefault(s => s.Id == id); // TODO: Add get item service logic here
            if (species == null)
            {
                return NotFound(); // 404 HTTP Status Code
            }
            return View(species);
        }

        // GET: Species/Create
        public IActionResult Create()
        {
            // TODO: Add get related items service logic here to set ViewData if necessary
            return View();
        }

        // POST: Species/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SpeciesModel species)
        {
            if (ModelState.IsValid)
            {
                // TODO: Add insert service logic here
                Result result = _speciesService.Add(species);
                if (result.IsSuccessful)
                {
                    // Way 1:
                    //return RedirectToAction("Index", "Species");
                    // Way 2:
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Index));
                }
                // Way 1:
                //ViewData["ViewMessage"] = result.Message;
                // Way 2:
                //ViewBag.ViewMessage = result.Message;
                ModelState.AddModelError("", result.Message);
            }
            // TODO: Add get related items service logic here to set ViewData if necessary
            return View(species);
        }

        // GET: Species/Edit/5
        public IActionResult Edit(int id)
        {
            SpeciesModel species = _speciesService.Query().SingleOrDefault(s => s.Id == id); // TODO: Add get item service logic here
            if (species == null)
            {
                return NotFound();
            }
            // TODO: Add get related items service logic here to set ViewData if necessary
            return View(species);
        }

        // POST: Species/Edit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(SpeciesModel species)
        {
            if (ModelState.IsValid)
            {
                // TODO: Add update service logic here
                Result result = _speciesService.Update(species);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = species.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            // TODO: Add get related items service logic here to set ViewData if necessary
            return View(species);
        }

        // GET: Species/Delete/5
        public IActionResult Delete(int id)
        {
            SpeciesModel species = _speciesService.Query().SingleOrDefault(s => s.Id == id); // TODO: Add get item service logic here
            if (species == null)
            {
                return NotFound();
            }
            return View(species);
        }

        // POST: Species/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // TODO: Add delete service logic here
            Result result = _speciesService.Delete(id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
	}
}
