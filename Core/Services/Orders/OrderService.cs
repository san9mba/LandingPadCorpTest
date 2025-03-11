using BusinessEntities.Enums;
using BusinessEntities.Sales;
using Common;
using Common.Exceptions;
using Core.Dtos;
using Core.Factories;
using Infrastructure.Repositories;
using Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Services.Orders
{
    [AutoRegister]
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;
        private readonly IProductRepository _productRepository;
        private readonly IIdObjectFactory<Order> _idFactory;
        private readonly IDateTimeProvider _dateTimeProvider;

        public OrderService(IOrderRepository repository,
            IProductRepository productRepository,
            IIdObjectFactory<Order> idFactory,
            IDateTimeProvider dateTimeProvider)
        {
            _repository = repository;
            _productRepository = productRepository;
            _idFactory = idFactory;
            _dateTimeProvider = dateTimeProvider;
        }

        public Order GerOrder(Guid orderId)
        {
            return _repository.Get(orderId);
        }

        public IEnumerable<Order> GetOrders(OrderFilterDto filter)
        {
            return _repository.GetOrders(filter.Skip, filter.Take, (int?)filter.Status, filter.StartDate, filter.EndDate, filter.MinTotalPrice, filter.MaxTotalPrice);
        }

        public Order CreateOrder(Guid id, IEnumerable<OrderItemDto> items)
        {
            var order = _repository.Get(id);
            if (order != null)
                throw new EntityAlreadyExistsException(nameof(Order), id.ToString());

            // check for duplicate products
            var groupedItems = items
                  .GroupBy(i => i.ProductId)
                  .Select(g => new OrderItemDto(g.Key, g.Sum(i => i.Quantity)))
                  .ToList();

            order = _idFactory.Create(id);
            order.DateCreated = _dateTimeProvider.UtcNow;
            var productsCountUpdate = new List<Product>();
            foreach (var item in items)
            {
                var product = _productRepository.Get(item.ProductId);
                if (product == null || product.DeletedDate.HasValue)
                    throw new EntityNotFoundException(nameof(Product), item.ProductId.ToString());
                if (product.Stock < item.Quantity)
                    throw new OutOfStockException(product.Name);

                product.Stock -= item.Quantity;
                productsCountUpdate.Add(product);
                // could be able to add discount or promotions
                order.AddItem(product, item.Quantity, product.Price);
            }

            _repository.Save(order);
            // update stock availability
            // could be bulk update, but for this project this is fine
            foreach(var product in productsCountUpdate)
                _productRepository.Save(product);
            return order;
        }

        public Order UpdateOrderStatus(Guid orderId, OrderStatus status)
        {
            var order = _repository.Get(orderId);
            if (order == null)
                throw new EntityNotFoundException(nameof(Order), orderId.ToString());
            order.Status = status;
            return order;
        }

        public void DeleteOrder(Guid orderId)
        {
            var order = _repository.Get(orderId);
            if (order == null)
                throw new EntityNotFoundException(nameof(Order), orderId.ToString());
            if (order.Status == OrderStatus.Completed)
                throw new InvalidOperationException("Cannot delete completed order.");

            _repository.Delete(order);
        }
    }
}
