using System;
using System.Net;
using System.Web.Http;
using BusinessAccessLayer;
using System.Net.Http;
using BusinessAccessLayer.Entities;
using System.Linq;
using System.Collections.Generic;
using refactor_me.Filters;
using refactor_me.ErrorHelper;
//using AttributeRouting.Web.Http;

namespace refactor_me.Controllers
{
    [ApiAuthenticationFilter]
    [RoutePrefix("products")]
    public class ProductsController : ApiController
    {
        #region private member variables
        private readonly IProductService _productServices;
        #endregion
        #region Public Constructor
        /// <summary>
        /// Public constructor to initialize product service instance
        /// </summary>
        public ProductsController(IProductService productService)
        {
            _productServices = productService;
        }

        #endregion
        [Route]
        [Route("allproducts")]
        [HttpGet]
        public HttpResponseMessage Get()
        {
            var products = _productServices.GetAllProducts();
            if (products != null)
            {
                var productEntities = products as List<ProductEntity> ?? products.ToList();
                if (productEntities.Any())
                    return Request.CreateResponse(HttpStatusCode.OK, productEntities);
            }
            throw new ApiDataException(1000, "Products not found", HttpStatusCode.NotFound);
        }

        [Route("productname/{name}")]
        [HttpGet]
        public HttpResponseMessage SearchByName(string name)
        {
            var product = _productServices.GetProductByName(name);
            if (product != null)
                return Request.CreateResponse(HttpStatusCode.OK, product);
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No product found for this name");
        }

        [Route("productid/{id}")]
        [HttpGet]
        public HttpResponseMessage Get(Guid id)
        {
            if (id != null)
            {
                var product = _productServices.GetProductById(id);
                if (product != null)
                    return Request.CreateResponse(HttpStatusCode.OK, product);
                throw new ApiDataException(1001, "No product found for this id.", HttpStatusCode.NotFound);
            }
            throw new ApiException() { ErrorCode = (int)HttpStatusCode.BadRequest, ErrorDescription = "Bad Request..." };
        }

        [Route]
        [HttpPost]
        public HttpResponseMessage Create([FromBody] ProductEntity productEntity)
        {
            if (productEntity != null)
            {
                var id = _productServices.AddProduct(productEntity);
                if (id != null)
                    return Request.CreateResponse(HttpStatusCode.OK, id);
                throw new ApiDataException(1001, "Unable to create new product", HttpStatusCode.InternalServerError);
            }
            throw new ApiException() { ErrorCode = (int)HttpStatusCode.BadRequest, ErrorDescription = "Bad Request..." };
        }

        [Route("{id}")]
        [HttpPut]
        public bool Update(Guid id, [FromBody]ProductEntity productEntity)
        {
            if (id != null)
            {
                var isSuccess = _productServices.UpdateProduct(id, productEntity);
                if (isSuccess)
                    return true;
                throw new ApiDataException(1002, "Unable to update product", HttpStatusCode.NotModified);
            }
            throw new ApiException() { ErrorCode = (int)HttpStatusCode.BadRequest, ErrorDescription = "Bad Request..." };
        }

        [Route("{id}")]
        [HttpDelete]
        public bool Delete(Guid id)
        {
            //check for product options before deleting a product
            var productOptions = _productServices.GetProductOptionsByProductId(id);
            if (productOptions != null)
            {
                _productServices.DeleteProductOptions(id);
            }
            if (id != null)
            {
                var isSuccess = _productServices.DeleteProduct(id);
                if (isSuccess)
                    return true;
                throw new ApiDataException(1002, "Product is already deleted or not exist in system.", HttpStatusCode.NoContent);
            }
            throw new ApiException() { ErrorCode = (int)HttpStatusCode.BadRequest, ErrorDescription = "Bad Request..." };
        }

        [Route("{productId}/options")]
        [HttpGet]
        public HttpResponseMessage GetOptions(Guid productId)
        {
            var productOptions = _productServices.GetProductOptionsByProductId(productId).ToList();
            if (productOptions != null)
            {
                var productOptionEntities = productOptions as List<ProductOptionEntity> ?? productOptions.ToList();
                if (productOptionEntities.Any())
                    return Request.CreateResponse(HttpStatusCode.OK, productOptionEntities);
                throw new ApiDataException(1001, "No product found for this id.", HttpStatusCode.NotFound);
            }
            throw new ApiException() { ErrorCode = (int)HttpStatusCode.BadRequest, ErrorDescription = "Bad Request..." };
        }

        [Route("{productId}/options/{id}")]
        [HttpGet]
        public HttpResponseMessage GetOption(Guid id)
        {
            var productOptions = _productServices.GetOptionIdByProductId(id);
            if (productOptions != null)
            {
                var productOptionEntities = productOptions as ProductOptionEntity ?? productOptions;
                if (productOptionEntities != null)
                    return Request.CreateResponse(HttpStatusCode.OK, productOptionEntities);
                throw new ApiDataException(1001, "No product found for this id.", HttpStatusCode.NotFound);
            }
            throw new ApiException() { ErrorCode = (int)HttpStatusCode.BadRequest, ErrorDescription = "Bad Request..." };
        }

        [Route("{productId}/options")]
        [HttpPost]
        public HttpResponseMessage CreateOption(Guid productId, [FromBody] ProductOptionEntity productOptionEntity)
        {
            if (productOptionEntity != null)
            {
                var id = _productServices.AddProductOption(productOptionEntity, productId);
                if (id != null)
                    return Request.CreateResponse(HttpStatusCode.OK, id);
                throw new ApiDataException(1001, "Unable to create new product", HttpStatusCode.InternalServerError);
            }
            throw new ApiException() { ErrorCode = (int)HttpStatusCode.BadRequest, ErrorDescription = "Bad Request..." };
        }

        [Route("{productId}/options/{id}")]
        [HttpPut]
        public bool UpdateOption(Guid id, [FromBody] ProductOptionEntity productOptionEntity)
        {
            if (id != null)
            {
                var isSuccess = _productServices.UpdateProductOption(id, productOptionEntity);
                if (isSuccess)
                    return true;
                throw new ApiDataException(1002, "Unable to update product", HttpStatusCode.NotModified);
            }
            throw new ApiException() { ErrorCode = (int)HttpStatusCode.BadRequest, ErrorDescription = "Bad Request..." };
        }

        [Route("{productId}/options/{id}")]
        [HttpDelete]
        public bool DeleteOption(Guid id)
        {
            if (id != null)
            {
                var isSuccess = _productServices.DeleteProductOption(id);
                if (isSuccess)
                    return true;
                throw new ApiDataException(1002, "Product is already deleted or not exist in system.", HttpStatusCode.NoContent);
            }
            throw new ApiException() { ErrorCode = (int)HttpStatusCode.BadRequest, ErrorDescription = "Bad Request..." };
        }
    }
}
