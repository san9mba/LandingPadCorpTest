using BusinessEntities.Enums;
using Core.Dtos;
using Infrastructure.Repositories;
using System;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models.Orders;
using WebApi.Validators;

namespace WebApi.Controllers.ApiConrollers
{
    [RoutePrefix("orders")]
    public class OrderApiController : BaseApiController
    {
        private readonly IOrderService _orderService;

        public OrderApiController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [Route("{orderId:guid}")]
        [HttpGet]
        public HttpResponseMessage GetOrder(Guid orderId)
        {
            var order = _orderService.GerOrder(orderId);
            if (order == null)
                return DoesNotExist();
            return Found(new OrderData(order));
        }

        [Route("{orderId:guid}/create")]
        [HttpPost]
        public HttpResponseMessage CreateOrder(Guid orderId, [FromBody] OrderModel model)
        {
            return ExecuteWitValidation<CreateOrderModelValidator, OrderModel>(model, () =>
            {
                var order = _orderService.CreateOrder(orderId, model.Items.Select(x => new OrderItemDto(x.ProductId, x.Quantity)));
                return Found(new OrderData(order));
            });
        }

        [Route("{orderId:guid}/status")]
        [HttpPost]
        public HttpResponseMessage UpdateOrderStatus(Guid orderId, [FromUri] OrderStatus status)
        {
            return ExecuteWithTryCatch(() =>
            {
                var order = _orderService.UpdateOrderStatus(orderId, status);
                return Found(new OrderData(order));
            });
        }

        [Route("{orderId:guid}/delete")]
        [HttpDelete]
        public HttpResponseMessage DeleteOrder(Guid orderId)
        {
            return ExecuteWithTryCatch(() =>
            {
                _orderService.DeleteOrder(orderId);
                return Found();
            });
        }

        [Route("list")]
        [HttpGet]
        public HttpResponseMessage GetOrders([FromUri]OrderFilterDto filter)
        {
            var orders = _orderService.GetOrders(filter).Select(q => new OrderData(q)).ToList();
            return Found(orders);
        }
    }
}