using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Dogs;
using MediatR;

namespace Application.Dogs.Update
{
    public record UpdateDogCommand(
        DogId DogId,
        String Name,
        String Color,
        int TailLength,
        int Weight) : IRequest;

    public record UpdateDogRequest(
        String Name,
        String Color,
        int TailLength,
        int Weight);

}
