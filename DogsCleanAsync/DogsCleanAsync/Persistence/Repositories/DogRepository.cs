using Domain.Dogs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    internal sealed class DogRepository : IDogRepository
    {
        private readonly ApplicationDbContext _context;
        public DogRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<Dog?> GetByIdAsync(DogId id)
        {
            return _context.Dogs
                .SingleOrDefaultAsync(p => p.Id == id);
        }

        public void Add(Dog dog)
        {
            _context.Dogs.Add(dog);
        }

        public void Update(Dog dog)
        {
            _context.Dogs.Update(dog);
        }

        public void Remove(Dog dog)
        {
            _context.Dogs.Remove(dog);
        }
    }
}
