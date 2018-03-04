using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer.Entities
{
    public class ProductEntity
    {
        /// <summary>
        /// Product Id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Product Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Product Description
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Product Price
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Delivery Price
        /// </summary>
        public decimal DeliveryPrice { get; set; }
    }
    public class ProductOptionEntity
    {
        /// <summary>
        /// Product option id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Product id
        /// </summary>
        public Guid ProductId { get; set; }
        /// <summary>
        /// Product option name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Product option description
        /// </summary>
        public string Description { get; set; }
    }
}
