using Domain.Interface;
using Domain.Queries;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Handlers
{
    public class GetByIdGenericHandler<T> : IRequestHandler<GetByIdGenericQuery<T>, T> where T : class
    {
        private readonly IGenericRepository<T> _repository;

        public GetByIdGenericHandler(IGenericRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task<T> Handle(GetByIdGenericQuery<T> request, CancellationToken cancellationToken)
        {
            return await _repository.GetByIdAsync(request.Id);
        }
    }
}
