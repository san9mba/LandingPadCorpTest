using BusinessEntities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessEntities.Sales
{
    public class Order : IdObject
    {
        private OrderStatus _status;
        private DateTime _dateCreated;
        private readonly List<OrderItem> _items = new List<OrderItem>();

        public Order()
        {
            Status = OrderStatus.Pending;
            TotalPrice = new Money(0);
        }

        public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();
        public Money TotalPrice { get; set; }
        public DateTime DateCreated
        {
            get => _dateCreated;
            set
            {
                if (_dateCreated != DateTime.MinValue)
                    throw new InvalidOperationException("Cannot change Creation Date.");
                _dateCreated = value;
            }
        }

        public void AddItem(Product product, int quantity, Money unitPrice)
        {
            if (Status != OrderStatus.Pending)
                throw new InvalidOperationException("Cannot modify an order that is not pending.");

            var orderItem = new OrderItem(product, quantity, unitPrice);
            TotalPrice += orderItem.TotalPrice;
            _items.Add(orderItem);
        }

        public void Checkout()
        {
            if (!_items.Any()) throw new InvalidOperationException("Order must have at least one item.");
            Status = OrderStatus.Confirmed;
        }

        public OrderStatus Status
        {
            get => _status;
            set
            {
                if (Status == OrderStatus.Completed || Status == OrderStatus.Canceled)
                    throw new InvalidOperationException($"Order can't be changed. Status is {Status}");
                _status = value;
            }
        }
    }
}
