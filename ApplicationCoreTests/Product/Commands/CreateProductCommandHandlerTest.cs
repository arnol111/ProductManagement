using ApplicationCore.Product.Commands.CreateProduct;
using Domain.Entities;
using NUnit.Framework;
using System;
using FluentAssertions;
using System.Threading.Tasks;
using static ApplicationCoreTests.ConfigurationTesting;
using ApplicationCore.Exceptions;
using ApplicationCore.Product.Commands.HardDeleteProduct;
using FluentAssertions.Execution;

namespace ApplicationCoreTests.Product.Commands
{
    [TestFixture]
    public class CreateProductCommandHandlerTest
    {
        [SetUp]
        public void SetUp()
        {

        }
    
        [TearDown]
        public void TearDown()
        {
            _ = ResetState();
        }


        [Test]
        public async Task CreateNewProductWithValidData_ShouldReturnTheProductId()
        {
            var command = new CreateProductCommand()
            {                
                Description = "CuberSpeed Cubo de velocidad Twist 3x3 stickerelss",
                ProductCode = "YJ8262",
                ExpirationDate = new DateTime(2022, 12, 31),
                ManufacturingDate = DateTime.Now,
                ProviderCode = "B079J1TKTR",
                ProviderDescription = "Cuberspeed",
                ProviderPhone = 312545214,
                State = "Activo"
            };
            var productId = await SendAsync(command);

            var product = await FindAsync<Products>(productId);
           
            product.Should().NotBeNull();
            product.Id.Should().BePositive();
            Assert.That(command.Description, Is.EqualTo(product.Description));
            product.Description.Should().Be(command.Description);
        }

        [Test]
        public Task CreateNewProduct_WithOutValidData_ShouldThrowInvalidDateProductException()
        {
            var command = new CreateProductCommand()
            {
                Description = "CuberSpeed Cubo de velocidad Twist 3x3 stickerelss",
                ProductCode = "YJ8262",
                ExpirationDate = DateTime.Now,
                ManufacturingDate = DateTime.Now,
                ProviderCode = "B079J1TKTR",
                ProviderDescription = "Cuberspeed",
                ProviderPhone = 312545214,
                State = "Activo"
            };
           
            Assert.ThrowsAsync<InvalidDateProductException>(() => SendAsync(command));
            return Task.CompletedTask;
        }
    }
}
