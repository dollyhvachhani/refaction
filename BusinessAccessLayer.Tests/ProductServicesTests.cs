using DataAccessLayer;
using DataAccessLayer.GenericRepository;
using DataAccessLayer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Moq;
using NUnit.Framework;
using TestHelper;
using BusinessAccessLayer.Entities;

namespace BusinessAccessLayer.Tests
{
    public class ProductServicesTests
    {
        #region Variables
        private IProductService _productService;
        private IUnitOfWork _unitOfWork;
        private List<Product> _products;
        private List<ProductOption> _productOptions;
        private GenericRepository<Product> _productRepository;
        private WebApiDBEntities _dbEntities;
        #endregion

        #region Test fixture setup

        /// <summary>
        /// Initial setup for tests
        /// </summary>
        [TestFixtureSetUp]
        public void Setup()
        {
            _products = SetUpProducts();
            _productOptions = SetUpProductOptions();
        }

        #endregion

        #region Setup
        /// <summary>
        /// Re-initializes test.
        /// </summary>
        [SetUp]
        public void ReInitializeTest()
        {
            _dbEntities = new Mock<WebApiDBEntities>().Object;
            _productRepository = SetUpProductRepository();
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.SetupGet(s => s.ProductRepository).Returns(_productRepository);
            _unitOfWork = unitOfWork.Object;
            _productService = new ProductService(_unitOfWork);
        }

        #endregion

        #region Private member methods
        private GenericRepository<Product> SetUpProductRepository()
        {

            // Initialise repository
            var mockRepo = new Mock<GenericRepository<Product>>(MockBehavior.Default, _dbEntities);

            // Setup mocking behavior
            mockRepo.Setup(p => p.GetAll()).Returns(_products);

            mockRepo.Setup(p => p.GetByID(It.IsAny<Guid>()))
            .Returns(new Func<int, Product>(
            id => _products.Find(p => p.Id.Equals(id))));

            mockRepo.Setup(p => p.Insert((It.IsAny<Product>())))
            .Callback(new Action<Product>(newProduct =>
            {
                dynamic maxProductID = _products.Last().Id;
                dynamic nextProductID = Guid.NewGuid();
                newProduct.Id = nextProductID;
                _products.Add(newProduct);
            }));

            mockRepo.Setup(p => p.Update(It.IsAny<Product>()))
            .Callback(new Action<Product>(prod =>
            {
                var oldProduct = _products.Find(a => a.Id == prod.Id);
                oldProduct = prod;
            }));

            mockRepo.Setup(p => p.Delete(It.IsAny<Product>()))
            .Callback(new Action<Product>(prod =>
            {
                var productToRemove = _products.Find(a => a.Id == prod.Id);

                if (productToRemove != null)
                    _products.Remove(productToRemove);
            }));

            // Return mock implementation object
            return mockRepo.Object;
        }

        private static List<Product> SetUpProducts()
        {
            var products = DataInitializer.GetAllProducts();
            return products;
        }
        private static List<ProductOption> SetUpProductOptions()
        {
            var productOptions = DataInitializer.GetAllProductOptions();
            return productOptions;
        }
        #endregion

        #region Unit Tests

        /// <summary>
        /// Service should return all the products
        /// </summary>
        [Test]
        public void GetAllProductsTest()
        {
            var products = _productService.GetAllProducts();
            if (products != null)
            {
                var productList =
                    products.Select(
                        productEntity =>
                        new Product { Id = productEntity.Id, Name = productEntity.Name, Description = productEntity.Description, DeliveryPrice = productEntity.DeliveryPrice, Price = productEntity.Price }).
                        ToList();
                var comparer = new ProductComparer();
                CollectionAssert.AreEqual(
                    productList.OrderBy(product => product, comparer),
                    _products.OrderBy(product => product, comparer), comparer);
            }
        }

        /// <summary>
        /// Service should return null
        /// </summary>
        [Test]
        public void GetAllProductsTestForNull()
        {
            _products.Clear();
            var products = _productService.GetAllProducts();
            Assert.Null(products);
            SetUpProducts();
        }

        /// <summary>
        /// Service should return product if correct id is supplied
        /// </summary>
        [Test]
        public void GetProductByRightIdTest()
        {
            var mobileProduct = _productService.GetProductById(Guid.Empty);
            if (mobileProduct != null)
            {
                Mapper.CreateMap<ProductEntity, Product>();
                var productModel = Mapper.Map<ProductEntity, Product>(mobileProduct);
                AssertObjects.PropertyValuesAreEquals(productModel,
                                                      _products.Find(a => a.Name.Contains("Mobile")));
            }
        }

        /// <summary>
        /// Service should return null
        /// </summary>
        [Test]
        public void GetProductByWrongIdTest()
        {
            var product = _productService.GetProductById(Guid.Empty);
            Assert.Null(product);
        }

        /// <summary>
        /// Add new product test
        /// </summary>
        [Test]
        public void AddNewProductTest()
        {
            var newProduct = new ProductEntity()
            {
                Id = Guid.NewGuid(),
                Name = "Dell XPS 15 laptop",
                Description = "Dell XPS 15 laptop",
                Price = 3000,
                DeliveryPrice = 100.00M
            };

            var maxProductIDBeforeAdd = _products.Max(a => a.Id);
            newProduct.Id = Guid.NewGuid();
            _productService.AddProduct(newProduct);
            var addedproduct = new Product() { Name = newProduct.Name, Id = newProduct.Id };
            AssertObjects.PropertyValuesAreEquals(addedproduct, _products.Last());
            Assert.That(newProduct.Id, Is.EqualTo(_products.Last().Id));
        }

        /// <summary>
        /// Update product test
        /// </summary>
        [Test]
        public void UpdateProductTest()
        {
            var firstProduct = _products.First();
            firstProduct.Name = "Phone Updated";
            var updatedProduct = new ProductEntity()
            { Name = firstProduct.Name, Id = firstProduct.Id };
            _productService.UpdateProduct(firstProduct.Id, updatedProduct);
            Assert.That(firstProduct.Name, Is.EqualTo("Phone updated")); // Product name changed
        }

        /// <summary>
        /// Delete product test
        /// </summary>
        [Test]
        public void DeleteProductTest()
        {
            var count = _products.Count; // Before removal
            var lastProduct = _products.Last();

            // Remove last Product
            _productService.DeleteProduct(lastProduct.Id);
            Assert.That(_products.Count, Is.LessThan(count));
        }
        /// <summary>
        /// Gets all product options by product id test
        /// </summary>
        [Test]
        public void GetAllProductOptionsByProductIdTest()
        {
            var firstProductOption = _productOptions.First();
            var productOptions = _productService.GetProductOptionsByProductId(firstProductOption.ProductId);
            if (productOptions != null)
            {
                var productOptionsList =
                    productOptions.Select(
                        productOptionEntity =>
                        new ProductOption { Id = productOptionEntity.Id, Name = productOptionEntity.Name, Description = productOptionEntity.Description }).
                        ToList();
                var comparer = new ProductOptionsComparer();
                CollectionAssert.AreEqual(
                    productOptionsList.OrderBy(productOption => productOption, comparer),
                    _productOptions.OrderBy(productOption => productOption, comparer), comparer);
            }

        }
        /// <summary>
        /// Gets all product option id by product id test
        /// </summary>
        [Test]
        public void GetProductOptionIdByProductIdTest()
        {
            var mobileProduct = _productService.GetOptionIdByProductId(Guid.Empty);
            if (mobileProduct != null)
            {
                Mapper.CreateMap<ProductOptionEntity, ProductOptionEntity>();
                var productModel = Mapper.Map<ProductOptionEntity, ProductOptionEntity>(mobileProduct);
                AssertObjects.PropertyValuesAreEquals(productModel,
                                                      _products.Find(a => a.Name.Contains("Samsung")));
            }

        }
        /// <summary>
        /// Add product option test
        /// </summary>
        [Test]
        public void AddProductOption()
        {
            var firstProduct = _products.First();
            var newProductOption = new ProductOptionEntity()
            {
                Id = Guid.NewGuid(),
                Name = "iPhone X",
                Description = "New iPhoneX",
            };

            var count = _productOptions.Count;
            _productService.AddProductOption(newProductOption,firstProduct.Id);
            var addedproductOption = new ProductOption() { Name = newProductOption.Name, Id = newProductOption.Id };
            Assert.That(_productOptions.Count, Is.GreaterThan(count));
        }
        /// <summary>
        /// Updates product option test
        /// </summary>
        [Test]
        public void UpdateProductOption()
        {
            var firstProductOption = _productOptions.First();
            firstProductOption.Name = "Phone Updated";
            var updatedProduct = new ProductOptionEntity()
            { Name = firstProductOption.Name, Id = firstProductOption.Id };
            _productService.UpdateProductOption(firstProductOption.Id, updatedProduct);
            Assert.That(firstProductOption.Name, Is.EqualTo("Phone updated")); // Product name changed
        }
        /// <summary>
        /// Deletes product option test
        /// </summary>
        [Test]
        public void DeleteProductOption()
        {
            var count = _productOptions.Count;
            var lastProductOption = _productOptions.Last();

            // Remove last Product
            _productService.DeleteProductOption(lastProductOption.Id);
            Assert.That(_productOptions.Count, Is.LessThan(count)); // Max id reduced by 1
        }

        #endregion


        #region Tear Down
        /// <summary>
        /// Tears down each test data
        /// </summary>
        [TearDown]
        public void DisposeTest()
        {
            _productService = null;
            _unitOfWork = null;
            _productRepository = null;
            if (_dbEntities != null)
                _dbEntities.Dispose();
        }
        #endregion

        #region TestFixture TearDown.

        /// <summary>
        /// TestFixture teardown
        /// </summary>
        [TestFixtureTearDown]
        public void DisposeAllObjects()
        {
            _products = null;
        }

        #endregion
    }
}
