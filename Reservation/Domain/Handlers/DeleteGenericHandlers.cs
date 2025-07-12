using Domain.Commands;
using Domain.Interface;
using MediatR;

public class DeleteGenericHandler<T> : IRequestHandler<DeleteGenericCommand<T>, Unit> where T : class
{
    private readonly IGenericRepository<T> _repository;

    public DeleteGenericHandler(IGenericRepository<T> repository) => _repository = repository;

    public async Task<Unit> Handle(DeleteGenericCommand<T> request, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(request.Id);
        return Unit.Value;
    }
}
