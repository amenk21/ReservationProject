using Domain.Commands;
using Domain.Interface;
using MediatR;
using System.Reflection;

namespace Domain.Handlers
{
    public class PutGenericHandler<T> : IRequestHandler<PutGenericCommand<T>, T> where T : class
    {
        private readonly IGenericRepository<T> _repository;

        public PutGenericHandler(IGenericRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task<T> Handle(PutGenericCommand<T> request, CancellationToken cancellationToken)
        {
            var existingEntity = await _repository.GetByIdAsync(request.Id);
            if (existingEntity == null) throw new KeyNotFoundException("Entity not found.");

            // Reflectively update only non-null fields
            foreach (var prop in typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                var newValue = prop.GetValue(request.UpdatedFields);
                if (newValue != null)
                {
                    prop.SetValue(existingEntity, newValue);
                }
            }

            await _repository.UpdateAsync(existingEntity);
            return existingEntity;
        }
    }
}
