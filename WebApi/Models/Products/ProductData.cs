using BusinessEntities.Sales;
using System;

namespace WebApi.Models.Products
{
    public class ProductData : IdObjectData
    {
        public ProductData(Product product) : base(product)
        {
            Name = product.Name;
            Description = product.Description;
            Price = product.Price.Amount;
            Stock = product.Stock;
            DeletedDate = product.DeletedDate;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}