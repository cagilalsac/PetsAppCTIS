using Business.Models;
using DataAccess.Contexts;
using DataAccess.Entities;
using DataAccess.Results;
using DataAccess.Results.Bases;
using Microsoft.EntityFrameworkCore;

namespace Business.Services
{
    public interface ISpeciesService
    {
        IQueryable<SpeciesModel> Query();
        Result Add(SpeciesModel model);
        Result Update(SpeciesModel model);
        Result Delete(int id);
    }

    public class SpeciesService : ISpeciesService
    {
        private readonly Db _db;

        public SpeciesService(Db db)
        {
            _db = db;
        }

        // Read
        public IQueryable<SpeciesModel> Query() // ToList, SingleOrDefault, FirstOrDefault, Where, Any, etc.
        {
            return _db.Species.Include(s => s.Pets).OrderBy(s => s.Name).Select(s => new SpeciesModel()
            {
                Id = s.Id,
                Name = s.Name,

                PetCountOutput = s.Pets.Count,
                PetNamesOutput = string.Join("<br />", s.Pets.OrderByDescending(p => p.IsAdopted).ThenByDescending(p => p.BirthDate).ThenBy(p => p.Name).Select(p => p.Name))
            });
        }

        // Create
        public Result Add(SpeciesModel model)
        {
            if (_db.Species.Any(s => s.Name.ToLower() == model.Name.ToLower().Trim())) // Dog == dog (case insensitive)
                return new ErrorResult("Species with the same name exists!");
            Species entity = new Species()
            {
                Name = model.Name.Trim()
            };
            _db.Species.Add(entity);
            _db.SaveChanges();
            return new SuccessResult("Species added successfully.");
        }

        // Update
        public Result Update(SpeciesModel model)
        {
            if (_db.Species.Any(s => s.Id != model.Id && s.Name.ToLower() == model.Name.ToLower().Trim()))
                return new ErrorResult("Species with the same name exists!");
            // Way 1:
            //Species entity = _db.Species.SingleOrDefault(s => s.Id == model.Id);
            // Way 2:
            Species entity = _db.Species.Find(model.Id);
            if (entity is null)
                return new ErrorResult("Species not found!");
            entity.Name = model.Name.Trim();
            _db.Species.Update(entity);
            _db.SaveChanges();
            return new SuccessResult("Species updated successfully.");
        }

        // Delete
        public Result Delete(int id)
        {
            Species entity = _db.Species.Include(s => s.Pets).SingleOrDefault(s => s.Id == id);
            if (entity is null)
                return new ErrorResult("Species not found!");
            if (entity.Pets is not null && entity.Pets.Any()) // if (entity.Pets is not null && entity.Pets.Count > 0) 
                return new ErrorResult("Species can't be deleted because it has relational pets!");
            _db.Species.Remove(entity);
            _db.SaveChanges();
            return new SuccessResult("Species deleted successfully.");
        }
    }
}
