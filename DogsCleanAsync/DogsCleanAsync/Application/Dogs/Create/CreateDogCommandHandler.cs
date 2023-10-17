using Application.Data;
using Domain.Dogs;
using MediatR;

namespace Application.Dogs.Create
{
    internal class CreateDogCommandHandler : IRequestHandler<CreateDogCommand>
    {
        private readonly IDogRepository _dogRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateDogCommandHandler(
            IDogRepository dogRepository,
            IUnitOfWork unitOfWork)
        {
            _dogRepository = dogRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(CreateDogCommand request, CancellationToken cancellationToken) 
        {
            var dog = new Dog(
                new DogId(Guid.NewGuid()),
                request.Name,
                request.Color,
                request.TailLength,
                request.Weight);
            _dogRepository.Add(dog);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
