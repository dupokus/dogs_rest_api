using Application.Data;
using Domain.Dogs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dogs.GetById
{
    internal sealed class GetDogQueryHandler : IRequestHandler<GetDogQuery, DogResponse>
    {
        private readonly IApplicationDbContext _context;

        public GetDogQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DogResponse> Handle(GetDogQuery request, CancellationToken cancellationToken) 
        {
            var dog = await _context
                .Dogs
                .Where(d => d.Id == request.DogId)
                .Select(d => new DogResponse(
                    d.Id.Value,
                    d.Name,
                    d.Color,
                    d.TailLength,
                    d.Weight))
                .FirstOrDefaultAsync(cancellationToken);
            
            if (dog is null)
            {
                throw new DogNotFoundException(request.DogId);
            }

            return dog;
        }

    }
}
