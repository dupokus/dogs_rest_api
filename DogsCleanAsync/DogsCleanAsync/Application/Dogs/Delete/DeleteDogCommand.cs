using Domain.Dogs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dogs.Delete
{
    public record DeleteDogCommand(DogId DogId) : IRequest;
    
}
