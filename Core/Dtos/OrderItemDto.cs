using System;

namespace Core.Dtos
{
    public class OrderItemDto
    {
        public OrderItemDto(Guid productId, int quantity)
        {
            ProductId = productId;
            Quantity = quantity;
        }

        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
