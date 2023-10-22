using System;
using System.Collections.Generic;
using System.Linq;

namespace LayeredArchitecture.UseCase
{
    public class ValidationResult
    {
        private readonly IList<Exception> _exceptions;

        public ValidationResult()
        {
            _exceptions = new List<Exception>();
        }

        public ValidationResult(IList<Exception> exceptions)
        {
            if (exceptions == null)
            {
                throw new ArgumentNullException(nameof(exceptions));
            }

            _exceptions = exceptions;
        }

        public IList<Exception> Exceptions => _exceptions;
        public bool IsValid => !_exceptions.Any();
    }
}
