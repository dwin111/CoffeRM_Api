using CoffeCRMBeck.Controllers;
using CoffeCRMBeck.Model;
using CoffeCRMBeck.Services;
using FakeItEasy;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeCRMBeck.Test.Controller
{
    public class ProductCatalogControllerTest
    {
        private readonly ProductCatalogService _productCatalogService; 
        public ProductCatalogControllerTest()
        {
            _productCatalogService = A.Fake<ProductCatalogService>();
        }

        [Fact]
        public void ProductCatalogController_GetAll_ReturnOk()
        {
            //Arrange
            var productCatalogs = A.Fake<ICollection<ProductCatalog>>();
            var productCatalogList = A.Fake<List<ProductCatalog>>();
            var controller = new ProductCatalogController(_productCatalogService);

            //Act

            var result = controller.GetAll();

            //Assert

            result.Should().NotBeNull();
        }
    }
}
