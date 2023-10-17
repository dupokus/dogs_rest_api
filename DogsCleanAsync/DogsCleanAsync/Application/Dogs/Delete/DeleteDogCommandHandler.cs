using Application.Data;
using Domain.Dogs;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dogs.Delete
{
    internal sealed class DeleteDogCommandHandler : IRequestHandler<DeleteDogCommand>
    {
        private readonly IDogRepository _dogRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteDogCommandHandler(IDogRepository dogRepository, IUnitOfWork unitOfWork) 
        { 
            _dogRepository = dogRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(DeleteDogCommand request, CancellationToken cancellationToken) 
        {
            var dog = await _dogRepository.GetByIdAsync(request.DogId);

            if (dog is null)
            {
                throw new DogNotFoundException(request.DogId);
            }

            _dogRepository.Remove(dog);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
