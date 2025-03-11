using BusinessEntities;
using BusinessEntities.Sales;
using Common;
using Common.Exceptions;
using Core.Dtos;
using Core.Factories;
using Data.Repositories;
using Infrastructure.Services;
using System;
using System.Collections.Generic;

namespace Core.Services.Products
{
    [AutoRegister]
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IIdObjectFactory<Product> _idFactory;
        private readonly IDateTimeProvider _dateTimeProvider;

        public ProductService(IProductRepository repository, IIdObjectFactory<Product> idFactory, IDateTimeProvider dateTimeProvider)
        {
            _repository = repository;
            _idFactory = idFactory;
            _dateTimeProvider = dateTimeProvider;
        }

        public Product GetProduct(Guid productId)
        {
            return _repository.Get(productId);
        }

        public IEnumerable<Product> GetProducts(ProductFilterDto filter)
        {
            return _repository.GetProducts(filter.Skip, filter.Take, filter.Name, filter.MinPrice, filter.MaxPrice, filter.MinStock, filter.MaxStock, filter.IncludeDeleted);
        }

        public Product CreateProduct(Guid id, string name, string description, decimal price, int stock)
        {
            var existingProduct = _repository.Get(id);
            if (existingProduct != null)
                throw new EntityAlreadyExistsException(nameof(Product), id.ToString());

            var product = _idFactory.Create(id);
            SaveProduct(product, name, description, price, stock);
            return product;
        }

        public Product UpdateProduct(Guid id, string name, string description, decimal price, int stock)
        {
            var product = _repository.Get(id);
            if (product == null)
                throw new EntityNotFoundException(nameof(Product), id.ToString());

            SaveProduct(product, name, description, price, stock);
            return product;
        }

        public void SoftDeleteProduct(Guid id)
        {
            var product = _repository.Get(id) ?? throw new EntityNotFoundException(nameof(Product), id.ToString());
            // ignore if product already deleted
            if (product.DeletedDate.HasValue)
                return;

            product.DeletedDate = _dateTimeProvider.UtcNow;
            _repository.Save(product);
        }

        public void RestoreDeleteDProduct(Guid id)
        {
            var product = _repository.Get(id) ?? throw new EntityNotFoundException(nameof(Product), id.ToString());
            // ignore if product already deleted
            if (!product.DeletedDate.HasValue)
                return;
            product.DeletedDate = null;
            _repository.Save(product);
        }

        private void SaveProduct(Product product, string name, string description, decimal price, int stock)
        {
            product.SetName(name);
            product.Description = description;
            product.Price = new Money(price);
            product.Stock = stock;
            _repository.Save(product);
        }
    }
}
