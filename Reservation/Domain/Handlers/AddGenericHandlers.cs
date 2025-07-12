using MediatR;
using Domain.Commands;
using Domain.Interface;

namespace Domain.Handlers
{
    public class AddGenericHandler<T> : IRequestHandler<AddGenericCommand<T>, T>
       where T : class
    {
        private readonly IGenericRepository<T> _repository;

        public AddGenericHandler(IGenericRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task<T> Handle(AddGenericCommand<T> request, CancellationToken cancellationToken)
        {
            return await _repository.AddAsync(request.Entity);
        }
    }

}
