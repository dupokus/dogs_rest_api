using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsRestApi.Controllers
{
    public record CreateDogCommand(
        string Name,
        string Color,
        int TailLength,
        int Weight) : IRequest;
}