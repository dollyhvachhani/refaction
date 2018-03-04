using BusinessAccessLayer.Entities;
using DataAccessLayer;
using DataAccessLayer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace BusinessAccessLayer
{
    /// <summary>
    /// Offers services for product specific CRUD operations
    /// </summary>
    public class ProductService : IProductService
    {
        #region Private member variables
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructor 
        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Services
        /// <summary>
        /// Fetches product details by id
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public ProductEntity GetProductById(Guid productId)
        {
            var product = _unitOfWork.ProductRepository.GetByID(productId);
            if (product != null)
            {
                Mapper.CreateMap<Product, ProductEntity>();
                var productModel = Mapper.Map<Product, ProductEntity>(product);
                return productModel;
            }
            return null;
        }
        /// <summary>
        /// Fetches product details by name
        /// </summary>
        /// <param name="productName"></param>
        /// <returns></returns>
        public ProductEntity GetProductByName(string productName)
        {
            var product = _unitOfWork.ProductRepository.Get(e => e.Name.Equals(productName));
            if (product != null)
            {
                Mapper.CreateMap<Product, ProductEntity>();
                var productModel = Mapper.Map<Product, ProductEntity>(product);
                return productModel;
            }
            return null;
        }

        /// <summary>
        /// Fetches all the products.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ProductEntity> GetAllProducts()
        {
            var products = _unitOfWork.ProductRepository.GetAll().ToList();
            if (products.Any())
            {
                Mapper.CreateMap<Product, ProductEntity>();
                var productsModel = Mapper.Map<List<Product>, List<ProductEntity>>(products);
                return productsModel;
            }
            return null;
        }

        /// <summary>
        /// Creates a product
        /// </summary>
        /// <param name="productEntity"></param>
        /// <returns></returns>
        public Guid AddProduct(ProductEntity productEntity)
        {
            using (var scope = new TransactionScope())
            {
                var product = new Product
                {
                    Id = Guid.NewGuid(),
                    Name = productEntity.Name,
                    Description = productEntity.Description,
                    Price = productEntity.Price,
                    DeliveryPrice = productEntity.DeliveryPrice
                };
                _unitOfWork.ProductRepository.Insert(product);
                _unitOfWork.Save();
                scope.Complete();
                return product.Id;
            }
        }
        /// <summary>
        /// Updates a product
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="productEntity"></param>
        /// <returns></returns>
        public bool UpdateProduct(Guid productId, ProductEntity productEntity)
        {
            var success = false;
            if (productEntity != null)
            {
                using (var scope = new TransactionScope())
                {
                    var product = _unitOfWork.ProductRepository.GetByID(productId);
                    if (product != null)
                    {
                        product.Name = productEntity.Name;
                        product.Price = productEntity.Price;
                        product.DeliveryPrice = productEntity.DeliveryPrice;
                        _unitOfWork.ProductRepository.Update(product);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        /// <summary>
        /// Deletes a particular product
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public bool DeleteProduct(Guid productId)
        {
            var success = false;
            if (productId != null)
            {
                // delete product-options associated to the product before deleting a product.
                var productOption = _unitOfWork.ProductOptionRepository.GetMany(e => e.ProductId.Equals(productId)).ToList();
                if (productOption != null)
                {
                    if (productOption.Count > 0)
                        _unitOfWork.ProductOptionRepository.Delete(e => e.ProductId.Equals(productId));
                }
                using (var scope = new TransactionScope())
                {
                    var product = _unitOfWork.ProductRepository.GetByID(productId);
                    if (product != null)
                    {
                        _unitOfWork.ProductRepository.Delete(product);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }
        /// <summary>
        /// Gets all options for a specified product.
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public IEnumerable<ProductOptionEntity> GetProductOptionsByProductId(Guid productId)
        {
            var productOption = _unitOfWork.ProductOptionRepository.GetMany(e => e.ProductId.Equals(productId)).ToList();
            if (productOption != null)
            {
                Mapper.CreateMap<ProductOption, ProductOptionEntity>();
                var productModel = Mapper.Map<List<ProductOption>, List<ProductOptionEntity>>(productOption);
                return productModel;
            }
            return null;
        }
        /// <summary>
        /// Gets the specified product option for the specified product.
        /// </summary>
        /// <param name="productOptionId"></param>
        /// <returns></returns>
        public ProductOptionEntity GetOptionIdByProductId(Guid productOptionId)
        {
            var productOption = _unitOfWork.ProductOptionRepository.GetByID(productOptionId);
            if (productOption != null)
            {
                Mapper.CreateMap<ProductOption, ProductOptionEntity>();
                var productModel = Mapper.Map<ProductOption, ProductOptionEntity>(productOption);
                return productModel;
            }
            return null;
        }
        /// <summary>
        /// adds a new product option to the specified product.
        /// </summary>
        /// <param name="productOptionEntity"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        public Guid AddProductOption(ProductOptionEntity productOptionEntity, Guid productId)
        {
            using (var scope = new TransactionScope())
            {
                var productOption = new ProductOption
                {
                    Id = Guid.NewGuid(),
                    Name = productOptionEntity.Name,
                    Description = productOptionEntity.Description,
                    ProductId = productId
                };
                _unitOfWork.ProductOptionRepository.Insert(productOption);
                _unitOfWork.Save();
                scope.Complete();
                return productOption.Id;
            }
        }
        /// <summary>
        /// updates the specified product option.
        /// </summary>
        /// <param name="productOptionId"></param>
        /// <param name="productOptionEntity"></param>
        /// <returns></returns>
        public bool UpdateProductOption(Guid productOptionId, ProductOptionEntity productOptionEntity)
        {
            var success = false;
            if (productOptionEntity != null)
            {
                using (var scope = new TransactionScope())
                {
                    var productOption = _unitOfWork.ProductOptionRepository.GetByID(productOptionId);
                    if (productOption != null)
                    {
                        productOption.Name = productOptionEntity.Name;
                        productOption.Description = productOptionEntity.Description;
                        _unitOfWork.ProductOptionRepository.Update(productOption);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }
        /// <summary>
        /// deletes the specified product option.
        /// </summary>
        /// <param name="productOptionId"></param>
        /// <returns></returns>
        public bool DeleteProductOption(Guid productOptionId)
        {
            var success = false;
            if (productOptionId != null)
            {
                using (var scope = new TransactionScope())
                {
                    var productOption = _unitOfWork.ProductOptionRepository.GetByID(productOptionId);
                    if (productOption != null)
                    {
                        _unitOfWork.ProductOptionRepository.Delete(productOption);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        /// <summary>
        /// deletes the all options of a specified product.
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public bool DeleteProductOptions(Guid productId)
        {
            var success = false;
            if (productId != null)
            {
                using (var scope = new TransactionScope())
                {
                    var productOptions = _unitOfWork.ProductOptionRepository.GetMany(e => e.ProductId.Equals(productId)).ToList();
                    if (productOptions != null)
                    {
                        _unitOfWork.ProductOptionRepository.Delete(e => e.ProductId.Equals(productId));
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }
        #endregion
    }
}
