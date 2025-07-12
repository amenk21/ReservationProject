using MediatR;
using System;

namespace Domain.Queries
{
    public class GetByIdGenericQuery<T> : IRequest<T> where T : class
    {
        public Guid Id { get; }

        public GetByIdGenericQuery(Guid id)
        {
            Id = id;
        }
    }
}
