using Application.Data;
using Application.Dogs.GetById;
using Domain.Dogs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dogs.Get
{
    internal sealed class GetDogsQueryHandler 
        : IRequestHandler<GetDogsQuery, List<DogResponse>>
    {
        private readonly IApplicationDbContext _context;

        public GetDogsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<DogResponse>> Handle(GetDogsQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Dog> dogsQuery = _context.Dogs;
            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                dogsQuery = dogsQuery.Where(d =>
                    d.Name.Contains(request.SearchTerm));
            }

            if (request.SortOrder?.ToLower() == "desc")
            {
                dogsQuery = dogsQuery.OrderByDescending(GetSortProperty(request));
            }
            else
            {
                dogsQuery = dogsQuery.OrderBy(GetSortProperty(request));

            }

            var dogs = await dogsQuery
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(d => new DogResponse(
                    d.Id.Value,
                    d.Name,
                    d.Color,
                    d.TailLength,
                    d.Weight))
                .ToListAsync(cancellationToken);

            return dogs;
        }

        private static Expression<Func<Dog, object>> GetSortProperty(GetDogsQuery request) =>
            request.SortColumn?.ToLower() switch
            {
            "name" => dog => dog.Name,
            "weight" => dog => dog.Weight,
            _ => dog => dog.Id
            };
    }
}
