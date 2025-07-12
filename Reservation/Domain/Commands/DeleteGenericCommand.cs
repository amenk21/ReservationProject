using MediatR;

namespace Domain.Commands
{
    public class DeleteGenericCommand<T> : IRequest<Unit> where T : class
    {
        public Guid Id { get; }

        public DeleteGenericCommand(Guid id) => Id = id;
    }
}
