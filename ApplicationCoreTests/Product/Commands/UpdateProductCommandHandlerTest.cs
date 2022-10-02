using ApplicationCore.Exceptions;
using ApplicationCore.Product;
using ApplicationCore.Product.Commands.CreateProduct;
using ApplicationCore.Product.Commands.HardDeleteProduct;
using ApplicationCore.Product.Commands.UpdateProduct;
using Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using static ApplicationCoreTests.ConfigurationTesting;

namespace ApplicationCoreTests.Product.Commands
{
    [TestFixture]
    public class UpdateProductCommandHandlerTest
    {

        [OneTimeSetUp]
        public void SetUp()
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
                State = "Activo",
            };
            var productId = Task.Run(() => SendAsync(command)).Result;
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _ = ResetState();
        }

        [Test]        
        public async Task UpdateProduct_ShouldAlterTheEntityWhitNewValues()
        {
            var command = new UpdateProductCommand()
            {
                ProductToUpdate = new ProductDto()
                {
                    Description = "YUNTENG Cube Twist - Rompecabezas mágico de 3 x 3 pulgadas, cubo de velocidad de colores vivos",
                    ProductCode = "YJ8262",
                    ExpirationDate = new DateTime(2022, 12, 31),
                    ManufacturingDate = DateTime.Now,
                    ProviderCode = "B079J1TKTR",
                    ProviderDescription = "Cuberspeed",
                    ProviderPhone = 312545214,
                    State = "Activo",
                    ProductId = 2
                }
            };

            await SendAsync(command);

            var product = await FindAsync<Products>(command.ProductToUpdate.ProductId);

            product.Should().NotBeNull();
            product.Description.Should().Be(command.ProductToUpdate.Description);
        }

        [Test]
        public Task UpdateProductWithOutValidData_ShouldShouldThrowInvalidDateProductException()
        {
            var command = new UpdateProductCommand()
            {
                ProductToUpdate = new ProductDto()
                {
                    Description = "YUNTENG Cube Twist - Rompecabezas mágico de 3 x 3 pulgadas, cubo de velocidad de colores vivos",
                    ProductCode = "YJ8262",
                    ExpirationDate = DateTime.Now,
                    ManufacturingDate = DateTime.Now,
                    ProviderCode = "B079J1TKTR",
                    ProviderDescription = "Cuberspeed",
                    ProviderPhone = 312545214,
                    State = "Activo",
                    ProductId = 2
                }
            };
            Assert.ThrowsAsync<InvalidDateProductException>(() => SendAsync(command));
            return Task.CompletedTask;            
        }
    }
}
