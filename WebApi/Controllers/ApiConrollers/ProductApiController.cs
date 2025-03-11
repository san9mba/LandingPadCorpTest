using Core.Dtos;
using Core.Services.Products;
using System;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models.Products;
using WebApi.Validators;

namespace WebApi.Controllers
{
    [RoutePrefix("products")]

    public class ProductApiController : BaseApiController
    {
        private readonly IProductService _productService;
        public ProductApiController(IProductService productService)
        {
            _productService = productService;
        }

        [Route("{productId:guid}")]
        [HttpGet]
        public HttpResponseMessage GetProduct(Guid productId)
        {
            var product = _productService.GetProduct(productId);
            if (product == null)
                return DoesNotExist();
            return Found(new ProductData(product));
        }

        [Route("{productId:guid}/create")]
        [HttpPost]
        public HttpResponseMessage CreateProduct(Guid productId, [FromBody] ProductModel model)
        {
            return ExecuteWitValidation<ProductModelValidator, ProductModel>(model, () =>
            {
                var product = _productService.CreateProduct(productId, model.Name, model.Description, model.Price, model.Stock);
                return Found(new ProductData(product));
            });
        }

        [Route("{productId:guid}/update")]
        [HttpPost]
        public HttpResponseMessage UpdateProduct(Guid productId, [FromBody] ProductModel model)
        {
            return ExecuteWitValidation<ProductModelValidator, ProductModel>(model, () =>
            {
                var product = _productService.UpdateProduct(productId, model.Name, model.Description, model.Price, model.Stock);
                return Found(new ProductData(product));
            });
        }

        [Route("{productId:guid}/delete")]
        [HttpDelete]
        public HttpResponseMessage DeleteProdct(Guid productId)
        {
            return ExecuteWithTryCatch(() =>
            {
                _productService.SoftDeleteProduct(productId);
                return Found();
            });
        }

        [Route("{productId:guid}/restore")]
        [HttpPut]
        public HttpResponseMessage RestoreProdct(Guid productId)
        {
            return ExecuteWithTryCatch(() =>
            {
                _productService.RestoreDeleteDProduct(productId);
                return Found();
            });
        }

        [Route("list")]
        [HttpGet]
        public HttpResponseMessage GetProducts([FromUri] ProductFilterDto filter)
        {
            var products = _productService.GetProducts(filter ?? new ProductFilterDto()).Select(q => new ProductData(q)).ToList();
            return Found(products);
        }
    }
}