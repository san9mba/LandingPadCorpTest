using BusinessEntities.Sales;
using WebApi.Models.Products;

namespace WebApi.Models.Orders
{
    public class OrderItemData : IdObjectData
    {
        public OrderItemData(OrderItem item) : base(item)
        {
            Quantity = item.Quantity;
            UnitPrice = item.UnitPrice.Amount;
            TotalPrice = item.TotalPrice.Amount;
            Product = new ProductItemData(item.Product);
        }

        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public ProductItemData Product { get; set; }
    }
}