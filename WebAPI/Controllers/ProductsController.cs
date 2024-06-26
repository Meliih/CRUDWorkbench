﻿using Business.Abstract;
using Business.Constants;
using Entities.Concrete;
using Entities.Concrete.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("getall")]
        public IActionResult GetList()
        {
            var result = _productService.GetList();
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
            
        }

        [HttpGet("getlistbycategory")]
        public IActionResult GetListByCategory(int categoryId)
        {
            var result = _productService.GetListByCategory(categoryId);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);

        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int productId)
        {
            var result = _productService.GetById(productId);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
            
        }

        [HttpPost("add")]
        public IActionResult Add(Product product)
        {
            var result = _productService.Add(product);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("update")]
        public IActionResult Update(Product product)
        {
            var existingProduct = _productService.GetById(product.Id);

            if (existingProduct == null)
            {
                return BadRequest(Messages.NoProduct);

            }
            var result = _productService.Update(product);

            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("delete")]
        public IActionResult Delete(Product product)
        {
            var existingProduct = _productService.GetById(product.Id);
            if (existingProduct == null)
            {
                return BadRequest(Messages.NoProduct);

            }
            var result = _productService.Delete(product);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("searchProducts")]
        public IActionResult SearchProducts(string searchKeyword)
        {
            // Arama anahtar kelimesine göre ürünleri filtreleme işlemi
            var filteredProducts = _productService.SearchProductsByKeyword(searchKeyword);
            return Ok(filteredProducts.Data);
        }

        [HttpGet("getProductsByStockRange")]
        public IActionResult GetProductsByStockRange(int? min, int? max)
        {
            var result = _productService.GetProductsByStockRange(min, max);
            return Ok(result.Data);
        }
    }
}
