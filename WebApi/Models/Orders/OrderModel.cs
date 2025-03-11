using System.Collections.Generic;

namespace WebApi.Models.Orders
{
    public class OrderModel
    {
        public List<OrderItemModel> Items { get; set; } = new List<OrderItemModel>();
    }
}