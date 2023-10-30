using System;
using RestfulWebApi.Domain.Constants;
using RestfulWebApi.Domain.Exceptions;
using RestfulWebApi.Domain.ValueObjects;

namespace RestfulWebApi.Domain.Entities
{
    public class BaseEntity
    {
        private Guid _id;

        private string _name = string.Empty;

        public Guid Id
        {
            get
            {
                return _id;
            }
            set
            {
                if (value.Equals(Guid.Empty))
                {
                    throw new Exception("No empty GUIDs are allowed.");
                }

                _id = value;
            }
        }

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
