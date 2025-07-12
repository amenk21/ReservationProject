using MediatR;
using System.Collections.Generic;

namespace Domain.Queries
{
    public class GetGenericQuery<T> : IRequest<IEnumerable<T>> where T : class
    {
    }
}
