using Business.Models;
using DataAccess.Contexts;
using DataAccess.Entities;
using DataAccess.Results;
using DataAccess.Results.Bases;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Business.Services
{
    public interface IPetService
    {
        IQueryable<PetModel> Query();
        Result Add(PetModel model);
        Result Update(PetModel model);
        Result Delete(int id);

        List<PetModel> GetList();
        PetModel GetItem(int id);
    }

    public class PetService : IPetService
    {
        private readonly Db _db;

        public PetService(Db db)
        {
            _db = db;
        }

        public IQueryable<PetModel> Query()
        {
            return _db.Pets.Include(p => p.Species).OrderBy(p => p.Name).ThenBy(p => p.BirthDate).Select(p => new PetModel()
            {
                // entity properties
                Id = p.Id,
                Name = p.Name,
                BirthDate = p.BirthDate,
                Sex = p.Sex,
                IsAdopted = p.IsAdopted,
                Height = p.Height,
                Weight = p.Weight,
                SpeciesId = p.SpeciesId,

                // extra properties
                // Way 1:
                //HeightOutput = p.Height != null ? p.Height.Value.ToString("N1", new CultureInfo("en-US")) + " m." : string.Empty,
                // Way 2:
                //HeightOutput = (p.Height ?? 0).ToString("N1", new CultureInfo("en-US")) + " m.",
                // Way 3:
                HeightOutput = p.Height.HasValue ? p.Height.Value.ToString("N1", new CultureInfo("en-US")) + " m." : string.Empty,

                WeightOutput = p.Weight.HasValue ? p.Weight.Value.ToString("N1", new CultureInfo("en-US")) + " kg." : string.Empty,
                BirthDateOutput = p.BirthDate.ToString("MM/dd/yyyy"),
                IsAdoptedOutput = p.IsAdopted ? "Yes" : "No",
                SpeciesOutput = p.Species.Name
            });
        }

        public Result Add(PetModel model)
        {
            Pet entity = new Pet()
            {
                Name = model.Name.Trim(),
                BirthDate = model.BirthDate,
                Sex = model.Sex,
                IsAdopted = model.IsAdopted,
                Height = model.Height,
                Weight = model.Weight,

                // Way 1:
                //SpeciesId = model.SpeciesId ?? 0,
                // Way 2:
                SpeciesId = model.SpeciesId.Value
            };

            // Way 1:
            //_db.Pets.Add(entity);
            // Way 2:
            _db.Add(entity);

            _db.SaveChanges();

            model.Id = entity.Id;

            return new SuccessResult();
        }

        public Result Update(PetModel model)
        {
            Pet entity = _db.Pets.Find(model.Id);
            if (entity is null)
                return new ErrorResult("Pet not found!");
            entity.Name = model.Name.Trim();
            entity.BirthDate = model.BirthDate;
            entity.Sex = model.Sex;
            entity.IsAdopted = model.IsAdopted;
            entity.Height = model.Height;
            entity.Weight = model.Weight;
            entity.SpeciesId = model.SpeciesId.Value;

            // Way 1:
            //_db.Pets.Update(entity);
            // Way 2:
            _db.Update(entity);

            _db.SaveChanges();
            return new SuccessResult();
        }

        public Result Delete(int id)
        {
            Pet entity = _db.Pets.Find(id);
            if (entity is null)
                return new ErrorResult("Pet not found!");

            // Way 1:
            //_db.Pets.Remove(entity);
            // Way 2:
            _db.Remove(entity);

            _db.SaveChanges();
            return new SuccessResult();
        }

        public List<PetModel> GetList() => Query().ToList();

        public PetModel GetItem(int id) => Query().SingleOrDefault(q => q.Id == id);
    }
}
