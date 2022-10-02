using ApplicationCore.Exceptions;
using ApplicationCore.Product;
using ApplicationCore.Product.Commands.CreateProduct;
using ApplicationCore.Product.Commands.DeleteProduct;
using ApplicationCore.Product.Commands.UpdateProduct;
using ApplicationCore.Product.Queries.GetByCode;
using ApplicationCore.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WebApi.Controllers
{
    public class ProductController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<ProductDto>> GetProductByCode([FromQuery] GetProductByCodeQuery query)
        {
            var product = new ProductDto();
            try
            {
                product = await Mediator.Send(query);
            }
            catch(NotFoundException e)
            {
                return NotFound(e.Message);
            }
            return Ok(product);
        }
        
        [HttpPost]
        public async Task<ActionResult<int>> CreateProduct([FromQuery] CreateProductCommand command)
        {
            int productId;
            try
            {
                productId = await Mediator.Send(command);
            }
            catch (InvalidDateProductException e)
            {
                return Conflict(e.Message);
            }
            return Ok(productId);
        }

        [HttpDelete("{productId}")]
        public async Task<ActionResult> DeleteProductById([FromBody] int productId)
        {
            try
            {
                var command = new DeleteProductByIdCommand() { ProductId = productId};
                var product = await Mediator.Send(command);

            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (InvalidDateProductException e)
            {
                return BadRequest(e.Message);
            }
            return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateProduct([FromQuery] UpdateProductCommand command)
        {
            try
            {               
                var product = await Mediator.Send(command);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (InvalidDateProductException e)
            {
                return BadRequest(e.Message);
            }
            return NoContent();
        }
    }
}
