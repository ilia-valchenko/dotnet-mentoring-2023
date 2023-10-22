using System;
using LayeredArchitecture.Domain.Constants;
using LayeredArchitecture.Domain.Exceptions;
using LayeredArchitecture.Domain.ValueObjects;

namespace LayeredArchitecture.Domain.Entities
{
    public class BaseEntity
    {
        private readonly Guid _id;

        private string _name = string.Empty;

        public BaseEntity(Guid id)
        {
            if (id.Equals(Guid.Empty))
            {
                throw new Exception("No empty GUIDs are allowed.");
            }

            _id = id;
        }

        public Guid Id => _id;

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new InvalidNameException("The provided value is null or a white-space.");
                }

                if (value.Length > Limits.NameLengthLimit)
                {
                    throw new InvalidNameException($"The length of the provided name is greater than {Limits.NameLengthLimit} characters.");
                }

                _name = value;
            }
        }

        public Url ImageUrl { get; set; }
    }
}
