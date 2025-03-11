using BusinessEntities.Sales;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApi.Models.Orders
{
    public class OrderData : IdObjectData
    {
        public OrderData(Order order) : base(order.Id)
        {
            Status = new EnumData(order.Status);
            DateCreated = order.DateCreated;
            TotalPrice = order.TotalPrice.Amount;
            Items = order.Items.Select(x => new OrderItemData(x)).ToList();
        }

        public EnumData Status { get; set; }
        public DateTime DateCreated { get; set; }
        public decimal TotalPrice { get; set; }
        public List<OrderItemData> Items { get; set; }
    }
}