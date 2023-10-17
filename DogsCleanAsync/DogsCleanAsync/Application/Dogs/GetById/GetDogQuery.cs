using Domain.Dogs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dogs.GetById
{
    public record GetDogQuery(DogId DogId) : IRequest<DogResponse>;
    
    public record DogResponse(
        Guid Id, 
        string Name,
        string Color,
        int TailLength,
        int Weight);
    
}
