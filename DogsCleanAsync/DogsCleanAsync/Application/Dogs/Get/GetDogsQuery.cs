using Application.Dogs.GetById;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dogs.Get
{
    public record GetDogsQuery(
        string? SearchTerm,
        string? SortColumn,
        string? SortOrder,
        int Page,
        int PageSize) : IRequest<List<DogResponse>>;
    
}
