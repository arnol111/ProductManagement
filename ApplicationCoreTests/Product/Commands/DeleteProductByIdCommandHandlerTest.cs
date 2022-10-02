using ApplicationCore.Exceptions;
using ApplicationCore.Product.Commands.CreateProduct;
using Domain.Entities;
using NUnit.Framework;
using static ApplicationCoreTests.ConfigurationTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using ApplicationCore.Product.Commands.DeleteProduct;
using NUnit.Framework.Interfaces;
using ApplicationCore.Product.Commands.HardDeleteProduct;

namespace ApplicationCoreTests.Product.Commands
{
    [TestFixture]
    public class DeleteProductByIdCommandHandlerTest
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
        public async Task DeleteExistingProductBy_ShouldDeleteTheProduct()
        {

            var command = new DeleteProductByIdCommand()
            {
                ProductId = 1
            };

            await SendAsync(command);

            var product = await FindAsync<Products>(command.ProductId);

            product.Should().NotBeNull();
            product.State.Should().Be("Inactivo");
        }

        [Test]
        public Task DeleteNotExistingProductBy_ShouldThrowNotFoundException()
        {

            var command = new DeleteProductByIdCommand()
            {
                ProductId = 99
            };

            Assert.ThrowsAsync<NotFoundException>(() => SendAsync(command));
            return Task.CompletedTask;
        }

    }
}
