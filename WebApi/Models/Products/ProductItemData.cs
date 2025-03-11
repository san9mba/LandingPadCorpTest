using BusinessEntities.Sales;

namespace WebApi.Models.Products
{
    public class ProductItemData: IdObjectData
    {
        public ProductItemData(Product product) : base(product)
        {
            Name = product.Name;
            Description = product.Description;
        }

        public string Name { get; set; }
        public string Description { get; set; }
    }
}