using BusinessEntities.Sales;
using System.Collections.Generic;
using System;

namespace Data.Repositories
{
    public interface IOrderRepository: IRepository<Order>
    {
        IEnumerable<Order> GetOrders(int skip, int take,
          int? status = null,
          DateTime? startDate = null,
          DateTime? endDate = null,
          decimal? minTotalPrice = null,
          decimal? maxTotalPrice = null);
    }
}
