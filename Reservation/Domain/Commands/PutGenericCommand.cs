using MediatR;

namespace Domain.Commands
{
    public class PutGenericCommand<T> : IRequest<T> where T : class
    {
        public Guid Id { get; }
        public T UpdatedFields { get; }

        public PutGenericCommand(Guid id, T updatedFields)
        {
            Id = id;
            UpdatedFields = updatedFields;
        }
    }
}
