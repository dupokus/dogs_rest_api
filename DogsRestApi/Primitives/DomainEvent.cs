using MediatR;

namespace DogsRestApi.Primitives;

public record DomainEvent(Guid Id) : INotification;
