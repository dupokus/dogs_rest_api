using DogsRestApi.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DogsRestApi.Model
{
    public class DbHelper
    {
        private readonly DogDbContext _context;

        public DbHelper(DogDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DoesDogWithNameExistAsync(string name)
        {
            var dogs = await _context.Dogs.ToListAsync(); // Retrieve all dogs from the database
            return dogs.Any(d => d.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public bool IsValidTailLength(double tailLength)
        {
            return tailLength >= 0;
        }
        public bool IsValidWeight(double weight)
        {
            return weight >= 0;
        }
        public async Task<List<DogModel>> GetDogsAsync(string? searchTerm, string? sortColumn, string? sortOrder, int page, int pageSize)
        {
            IQueryable<Dog> dogsQuery = _context.Dogs.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                dogsQuery = dogsQuery.Where(d =>
                    d.Name.Contains(searchTerm));
            }

            if (sortOrder?.ToLower() == "desc")
            {
                dogsQuery = dogsQuery.OrderByDescending(GetSortProperty(sortColumn));
            }
            else
            {
                dogsQuery = dogsQuery.OrderBy(GetSortProperty(sortColumn));
            }

            var dogs = await dogsQuery
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(d => new DogModel(
                    d.Id,
                    d.Name,
                    d.Color,
                    d.TailLength,
                    d.Weight))
                .ToListAsync();

            return dogs;
        }

        public async Task<DogModel> GetDogByIdAsync(int Id)
        {
            var row = await _context.Dogs.FirstOrDefaultAsync(d => d.Id == Id);
            if (row != null)
            {
                return new DogModel(row.Id, row.Name, row.Color, row.TailLength, row.Weight);
            }

            return null;
        }

        public async Task SaveDogAsync(DogModel dogModel)
        {
            Dog dbTable = new Dog
            {
                Name = dogModel.Name,
                Color = dogModel.Color,
                TailLength = dogModel.TailLength,
                Weight = dogModel.Weight
            };

            if (dogModel.Id > 0)
            {
                var existingDog = await _context.Dogs.FirstOrDefaultAsync(d => d.Id == dogModel.Id);
                if (existingDog != null)
                {
                    existingDog.Name = dogModel.Name;
                    existingDog.Color = dogModel.Color;
                    existingDog.TailLength = dogModel.TailLength;
                    existingDog.Weight = dogModel.Weight;
                }
            }
            else
            {
                _context.Dogs.Add(dbTable);
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteDogAsync(int Id)
        {
            var dog = await _context.Dogs.FirstOrDefaultAsync(d => d.Id == Id);
            if (dog != null)
            {
                _context.Dogs.Remove(dog);
                await _context.SaveChangesAsync();
            }
        }

        private static Expression<Func<Dog, object>> GetSortProperty(string sortColumn) =>
            sortColumn?.ToLower() switch
            {
                "name" => dog => dog.Name,
                "weight" => dog => dog.Weight,
                _ => dog => dog.Id
            };
    }
}
