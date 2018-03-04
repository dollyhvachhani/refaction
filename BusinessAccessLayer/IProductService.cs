using BusinessAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer
{
    public interface IProductService
    {
        /// <summary>
        /// Gets product by id
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        ProductEntity GetProductById(Guid productId);
        /// <summary>
        /// Gets product by its name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        ProductEntity GetProductByName(string name);
        /// <summary>
        /// Gets all products
        /// </summary>
        /// <returns></returns>
        IEnumerable<ProductEntity> GetAllProducts();
        /// <summary>
        /// creates a new product
        /// </summary>
        /// <param name="productEntity"></param>
        /// <returns></returns>
        Guid AddProduct(ProductEntity productEntity);
        /// <summary>
        /// Updates a specified product
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="productEntity"></param>
        /// <returns></returns>
        bool UpdateProduct(Guid productId, ProductEntity productEntity);
        /// <summary>
        /// Deletes a specified product
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        bool DeleteProduct(Guid productId);
        /// <summary>
        /// Gets all product options by product id
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        IEnumerable<ProductOptionEntity> GetProductOptionsByProductId(Guid productId);
        /// <summary>
        /// Gets the specified product option for the specified product.
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        ProductOptionEntity GetOptionIdByProductId(Guid productId);
        /// <summary>
        /// Creates a new product option to the specified product.
        /// </summary>
        /// <param name="productEntity"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        Guid AddProductOption(ProductOptionEntity productOptionEntity, Guid productId);
        /// <summary>
        /// updates the specified product option.
        /// </summary>
        /// <param name="productOptionId"></param>
        /// <param name="productOptionEntity"></param>
        /// <returns></returns>
        bool UpdateProductOption(Guid productOptionId, ProductOptionEntity productOptionEntity);
        /// <summary>
        /// deletes the specified product option.
        /// </summary>
        /// <param name="productOptionId"></param>
        /// <returns></returns>
        bool DeleteProductOption(Guid productOptionId);
        /// <summary>
        /// deletes multiple product options of a product.
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        bool DeleteProductOptions(Guid productId);
    }
}
