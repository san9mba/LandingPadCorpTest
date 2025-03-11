using System;

namespace Common.Exceptions
{
    public class OutOfStockException:Exception
    {
        public OutOfStockException(string productName) : base($"Product {productName} is out of stock.") { }
    }
}
