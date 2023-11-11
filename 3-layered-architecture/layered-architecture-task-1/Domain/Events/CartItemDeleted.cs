using MediatR;

namespace Domain.Events
{
    public class CartItemDeleted : INotification
    {
        public Guid CartId { get; private set; }

        public CartItemDeleted(Guid cartId)
        {
            this.CartId = cartId;
        }
    }
}
