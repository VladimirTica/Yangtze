using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient.DataClassification;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yangtze.BLL.Models;
using Yangtze.BLL.Services;
using Yangtze.DAL.Entities;
using Yangtze.DAL.Repositories;

namespace Yangtze.API.Controllers
{
    [ApiController]
    [Route("api/{userId}/products")]
    public class ProductController : ControllerBase
    {
        private readonly IYangtzeService _service;
        public ProductController(IYangtzeService service)
        {
            _service = service;
        }

        [HttpGet(Name ="GetAllProducts")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts(int userId, [FromQuery] int? category)
        {
            IEnumerable<ProductDto> result;
            if (category != null)
            {
                result = await _service.GetProductsByCategoryAsync(userId, category.Value);
            }
            else
            {
                result = await _service.GetProductsAsync(userId);
            }

            return Ok(result);
        }

        [HttpGet("{productId}", Name = "GetProductById")]
        public async Task<ActionResult<ProductDto>> GetProduct(int userId, int productId)
        {
            var result = await _service.GetProductByIdAsync(userId, productId);
            if (result == null)
            {
                return NotFound();
            }
           
            return Ok(result);
        }

        [HttpPost(Name ="CreateProduct")]
        public async Task<ActionResult<ProductDto>> CreateProduct(int userId, [FromBody] ProductForUpdateDto product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _service.AddProductAsync(userId, product);

            if (result == null)
            {
                return BadRequest("Failed to create product!");
            }

            return CreatedAtAction(nameof(GetProduct), new {userId= result.UserId, productId = result.ProductId }, result);
        }

        [HttpPut("{productId}", Name = "UpdatePoduct")]
        public async Task<ActionResult<ProductDto>> UpdateProduct(int userId, int productId, [FromBody] ProductForUpdateDto product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _service.UpdateProductAsync(userId, productId, product);
            if (result == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Product cannot be updated");
            }

            return Ok(result);
        }

        [HttpDelete("{productId}", Name ="Delete product")]
        public async Task<ActionResult<ProductDto>> DeleteProduct (int userId, int productId)
        {
            var result = await _service.DeleteProductAsync(userId, productId);
            if(result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

    }
}
