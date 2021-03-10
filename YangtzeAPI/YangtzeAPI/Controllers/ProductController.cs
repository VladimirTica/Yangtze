﻿using Microsoft.AspNetCore.Authorization;
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
using YangtzeAPI.Helper;

namespace Yangtze.API.Controllers
{
    [ApiController]
    [Route("api/{userId}/products")]
    public class ProductController : ResponseWrapper
    {
        private readonly IProductService _service;
        public ProductController(IProductService service)
        {
            _service = service;
        }

        [HttpGet(Name ="GetAllProducts")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts(int userId, [FromQuery] int? category)
        {
            if (category != null)
            {
                var result = await _service.GetProductsByCategoryAsync(userId, category.Value);
                return ResponseGet(result);
            }
            else
            {
                var result = await _service.GetProductsAsync(userId);
                return ResponseGet(result);
            }
        }

        [HttpGet("{productId}", Name = "GetProductById")]
        public async Task<ActionResult<ProductDto>> GetProduct(int userId, int productId)
        {
            var result = await _service.GetProductByIdAsync(userId, productId);

            if (result.Value == null)
            {
                return ResponseGet(result, $"Product with id of {productId} does not exist for user with id of {userId}");
            }
           
            return ResponseGet(result);
        }

        [HttpPost(Name ="CreateProduct")]
        public async Task<ActionResult<ProductDto>> CreateProduct(int userId, [FromBody] ProductForUpdateDto product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _service.AddProductAsync(userId, product);

            if (result.Value == null)
            {
                return ResponseGet(result, "Failed to create product!");
            }

            return ResponseGet(result);
        }

        [HttpPut("{productId}", Name = "UpdatePoduct")]
        public async Task<ActionResult<ProductDto>> UpdateProduct(int userId, int productId, [FromBody] ProductForUpdateDto product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _service.UpdateProductAsync(userId, productId, product);
            if (result.Value == null)
            {
                return ResponseGet(result, $"Product with id of {productId} does not exist for user with id of {userId}");
            }

            return ResponseGet(result);
        }

        [HttpDelete("{productId}", Name ="Delete product")]
        public async Task<ActionResult<ProductDto>> DeleteProduct (int userId, int productId)
        {
            var result = await _service.DeleteProductAsync(userId, productId);
            if(result.Value == null)
            {
                return ResponseGet(result, $"Product with id of {productId} does not exist for user with id of {userId}");
            }

            return ResponseGet(result);
        }

    }
}