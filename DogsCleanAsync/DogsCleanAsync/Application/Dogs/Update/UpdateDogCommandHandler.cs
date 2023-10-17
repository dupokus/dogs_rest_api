using Application.Data;
using Domain.Dogs;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dogs.Update
{
    internal sealed class UpdateDogCommandHandler : IRequestHandler<UpdateDogCommand>
    {
        private readonly IDogRepository _dogRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateDogCommandHandler(IDogRepository dogRepository, IUnitOfWork unitOfWork)
        {
            _dogRepository = dogRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UpdateDogCommand request, CancellationToken cancellationToken) 
        {
            var dog = await _dogRepository.GetByIdAsync(request.DogId);
            if (dog is null) 
            {
                throw new DogNotFoundException(request.DogId);
            }
            dog.Update(request.Name, request.Color, request.TailLength, request.Weight);

            _dogRepository.Update(dog);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
