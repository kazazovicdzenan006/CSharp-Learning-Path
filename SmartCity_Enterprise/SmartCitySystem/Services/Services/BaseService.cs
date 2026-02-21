using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Services
{
    public abstract class BaseService
    {
        protected readonly IServiceProvider _serviceProvider;

        protected BaseService(IServiceProvider provider)
        {
            _serviceProvider = provider;
        }


        protected async Task ValidateAsync<T>(T dto)
        {
            var validator = _serviceProvider.GetService(typeof(IValidator<T>)) as IValidator<T>;
            if (validator == null)
            {
                throw new InvalidOperationException($"No validator found for type {typeof(T).Name}");
            }
            var validationResult = await validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                var errors = string.Join(Environment.NewLine, validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException($"Validation failed for {typeof(T).Name}:{Environment.NewLine}{errors}");
            }
        }


    }
}
