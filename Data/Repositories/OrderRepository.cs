using BusinessEntities.Sales;
using Common;
using Infrastructure.DataProviders;
using Infrastructure.Repositories;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using Common.Extensions;
using System.Linq;

namespace Data.Repositories
{
    [AutoRegister]
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(IDataProvider<Order> dataProvider) : base(dataProvider)
        {
        }

        public IEnumerable<Order> GetOrders(int skip, int take,
          int? status = null,
          DateTime? startDate = null,
          DateTime? endDate = null,
          decimal? minTotalPrice = null,
          decimal? maxTotalPrice = null)
        {
            var filter = BuildFilterExpression(status, startDate, endDate, minTotalPrice, maxTotalPrice);
            return GetByExpression(skip, take, filter).ToList();
        }

        private Expression<Func<Order, bool>> BuildFilterExpression(int? status = null,
          DateTime? startDate = null,
          DateTime? endDate = null,
          decimal? minTotalPrice = null,
          decimal? maxTotalPrice = null)
        {
            Expression<Func<Order, bool>> filter = p => true;

            if (status.HasValue)
                filter = filter.AndAlso(p => (int)p.Status == status);

            if (startDate.HasValue)
                filter = filter.AndAlso(p => p.DateCreated >= startDate.Value);

            if (endDate.HasValue)
                filter = filter.AndAlso(p => p.DateCreated <= endDate);

            if (minTotalPrice.HasValue)
                filter = filter.AndAlso(p => p.TotalPrice.Amount >= minTotalPrice.Value);

            if (maxTotalPrice.HasValue)
                filter = filter.AndAlso(p => p.TotalPrice.Amount <= maxTotalPrice.Value);

            return filter;
        }
    }
}
