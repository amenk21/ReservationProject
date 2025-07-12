using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Commands
{
    public class AddGenericCommand<T> : IRequest<T> where T : class
    {
        public T Entity { get; }

        public AddGenericCommand(T entity)
        {
            Entity = entity ?? throw new ArgumentNullException(nameof(entity));
        }
    }
}
