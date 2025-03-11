using BusinessEntities.Sales;
using Core.Dtos;
using System;
using System.Collections.Generic;

namespace Core.Services.Products
{
    public interface IProductService
    {
        Product GetProduct(Guid productId);

        IEnumerable<Product> GetProducts(ProductFilterDto filter);

        Product CreateProduct(Guid id, string name, string description, decimal price, int stock);

        Product UpdateProduct(Guid id, string name, string description, decimal price, int stock);

        void SoftDeleteProduct(Guid id);
        void RestoreDeleteDProduct(Guid id);
    }
}
