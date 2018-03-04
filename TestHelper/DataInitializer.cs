using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestHelper
{
    /// <summary>
    /// Data initializer for unit tests
    /// </summary>
    public class DataInitializer
    {
        /// <summary>
        /// Dummy products
        /// </summary>
        /// <returns></returns>
        public static List<Product> GetAllProducts()
        {
            var products = new List<Product>
            {
                new Product() {Id=Guid.NewGuid(), Name = "Samsung Galaxy"},
                new Product() {Id=Guid.NewGuid(), Name = "Sony Xperia"},
                new Product() {Id=Guid.NewGuid(), Name= "IPhone x"},
                new Product() {Id=Guid.NewGuid(), Name= "Xiomi Mi 4"}
            };
            return products;
        }
        public static List<ProductOption> GetAllProductOptions()
        {
            var productOptions = new List<ProductOption>
            {
                new ProductOption() { Id=Guid.NewGuid(), Name="White Samsung Galaxy S7", Description="White Samsung Galaxy S7",ProductId =GetAllProducts().FirstOrDefault().Id},
                new ProductOption() { Id=Guid.NewGuid(), Name="Gold Samsung Galaxy S7", Description="Gold Samsung Galaxy S7", ProductId = GetAllProducts().FirstOrDefault().Id},
                new ProductOption() { Id=Guid.NewGuid(), Name="Blue Samsung Galaxy S7", Description="BlueSamsung Galaxy S7", ProductId = GetAllProducts().FirstOrDefault().Id}
            };
            return productOptions;
        }
        /// <summary>
        /// Dummy users
        /// </summary>
        /// <returns></returns>
        public static List<User> GetAllUsers()
        {
            var users = new List<User>
            {
                new User()
                {
                    UserName = "dolly",
                    Password = "dolly",
                    Name = "Dolly Vachhani",
                },
                new User()
                {
                    UserName = "john",
                    Password = "john",
                    Name = "John Mars",
                },
                new User()
                {
                    UserName = "apiuser",
                    Password = "apiuser",
                    Name = "API User",
                }
            };
            return users;
        }

    }
}

