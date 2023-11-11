namespace Domain.Models
{
    public abstract class BaseDomainModel
    {
        private readonly Guid _id;

        protected BaseDomainModel() : this(Guid.NewGuid())
        {
        }

        protected BaseDomainModel(Guid id)
        {
            if (id.Equals(Guid.Empty))
            {
                throw new ArgumentException("No empty GUIDs are allowed.");
            }

            _id = id;
        }

        public Guid Id => _id;
    }
}
