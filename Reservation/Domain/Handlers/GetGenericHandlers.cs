using Domain.Interface;
using Domain.Queries;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Handlers
{
    public class GetGenericHandler<T> : IRequestHandler<GetGenericQuery<T>, IEnumerable<T>> where T : class
    {
        private readonly IGenericRepository<T> _repository;

        public GetGenericHandler(IGenericRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<T>> Handle(GetGenericQuery<T> request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync();
        }
    }
}
