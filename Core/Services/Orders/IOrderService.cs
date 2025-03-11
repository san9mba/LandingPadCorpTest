using BusinessEntities.Enums;
using BusinessEntities.Sales;
using Core.Dtos;
using System;
using System.Collections.Generic;

namespace Infrastructure.Repositories
{
    public interface IOrderService
    {
        Order GerOrder(Guid orderId);
        IEnumerable<Order> GetOrders(OrderFilterDto filter);
        Order CreateOrder(Guid id, IEnumerable<OrderItemDto> items);
        Order UpdateOrderStatus(Guid orderId, OrderStatus status);
        void DeleteOrder(Guid orderId);
    }
}
