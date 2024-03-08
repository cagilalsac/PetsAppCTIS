using DataAccess.Contexts;
using DataAccess.Entities;
using DataAccess.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Text;

namespace MVC.Controllers
{
    public class DbController : Controller
    {
        private readonly Db _db;

        public DbController(Db db)
        {
            _db = db;
        }

        // GET: Db/Seed
        public IActionResult Seed()
        {
            // delete operations
            var pets = _db.Pets.ToList();
            _db.Pets.RemoveRange(pets);
            var species = _db.Species.ToList();
            _db.Species.RemoveRange(species);

            // insert operations
            _db.Species.Add(new Species()
            {
                Name = "Dog",
                Pets = new List<Pet>()
                {
                    new Pet()
                    {
                        BirthDate = new DateTime(2022, 9, 29),
                        Height = 0.45m,
                        Weight = 11.5M,
                        IsAdopted = true,
                        Name = "Luna",
                        Sex = Sex.Female
                    },
                    new Pet()
                    {
                        BirthDate = new DateTime(2022, 10, 7),
                        Height = 0.4m,
                        Weight = 10.5m,
                        IsAdopted = true,
                        Name = "Leo",
                        Sex = Sex.Male
                    }
                }
            });
            _db.Species.Add(new Species()
            {
                Name = "Cat",
                Pets = new List<Pet>()
                {
                    new Pet()
                    {
                        BirthDate = DateTime.Parse("09/19/2020", new CultureInfo("en-US")),
                        Height = 0.3m,
                        Weight = 8,
                        IsAdopted = false,
                        Name = "Lotus",
                        Sex = Sex.Male
                    }
                }
            });

            _db.SaveChanges();

            return Content("<label style=\"color:red;\">Database seed successful.</label>", "text/html", Encoding.UTF8); 
            // Encoding.UTF8 should be used if there are any Turkish character display problems.
        }
    }
}
