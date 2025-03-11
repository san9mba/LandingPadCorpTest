using BusinessEntities.Sales;
using System.Collections.Generic;

namespace Data.Repositories
{
    public interface IProductRepository: IRepository<Product>
    {
        IEnumerable<Product> GetProducts(int skip, int take,
          string name = null,
          decimal? minPrice = null,
          decimal? maxPrice = null,
          int? minStock = null,
          int? maxStock = null,
          bool includeDeleted = false);
    }
}
