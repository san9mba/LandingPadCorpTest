using System;

namespace BusinessEntities.Sales
{
    public class OrderItem : IdObject
    {
        private Guid _productId;
        private int _quantity;

        public OrderItem(Product product, int quantity, Money? unitPrice = null)
        {
            if (product == null)
                throw new ArgumentNullException("Product cannot be null");

            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.");

            ProductId = product.Id;
            Product = product;
            Quantity = quantity;
            UnitPrice = unitPrice ?? product.Price;
        }

        public Guid ProductId
        {
            get => _productId;
            set
            {
                if (value == Guid.Empty)
                    throw new ArgumentException("ProductId cannot be null or empty.");
                _productId = value;
            }
        }

        public int Quantity
        {
            get => _quantity;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Quantity must be greater than 0.");
                _quantity = value;
            }
        }

        public Product Product { get; set; }
        public Money UnitPrice { get; set; }
        public Money TotalPrice => UnitPrice * Quantity;
    }
}
