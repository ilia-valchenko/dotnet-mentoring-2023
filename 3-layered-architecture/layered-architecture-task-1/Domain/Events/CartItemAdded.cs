using MediatR;

namespace Domain.Events
{
    public class CartItemAdded : INotification
    {
        public Guid CartId { get; private set; }

        public CartItemAdded(Guid cartId)
        {
            this.CartId = cartId;
        }
    }
}
