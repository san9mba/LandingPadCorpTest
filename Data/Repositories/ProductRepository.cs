using BusinessEntities.Sales;
using Common;
using Infrastructure.DataProviders;
using Infrastructure.Repositories;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System;
using Common.Extensions;

namespace Data.Repositories
{
    [AutoRegister]
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(IDataProvider<Product> dataProvider) : base(dataProvider)
        {
        }

        public IEnumerable<Product> GetProducts(int skip, int take,
            string name = null,
            decimal? minPrice = null,
            decimal? maxPrice = null,
            int? minStock = null,
            int? maxStock = null,
            bool includeDeleted = false)
        {
            var filter = BuildFilterExpression(name, minPrice, maxPrice, minStock, maxStock, includeDeleted);
            return GetByExpression(skip, take, filter).ToList();
        }

        private Expression<Func<Product, bool>> BuildFilterExpression(string name, decimal? minPrice, decimal? maxPrice, int? minStock, int? maxStock, bool includeDeleted)
        {
            Expression<Func<Product, bool>> filter = p => true;

            if (!string.IsNullOrEmpty(name))
                filter = filter.AndAlso(p => p.Name.ToLower().Contains(name.ToLower()));

            if (minPrice.HasValue)
                filter = filter.AndAlso(p => p.Price.Amount >= minPrice.Value);

            if (maxPrice.HasValue)
                filter = filter.AndAlso(p => p.Price.Amount <= maxPrice.Value);

            if (minStock.HasValue)
                filter = filter.AndAlso(p => p.Stock >= minStock.Value);

            if (maxStock.HasValue)
                filter = filter.AndAlso(p => p.Stock <= maxStock.Value);

            if (!includeDeleted)
                filter = filter.AndAlso(p => p.DeletedDate == null);

            return filter;
        }

      
    }
}
