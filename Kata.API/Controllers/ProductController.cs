using Kata.API.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kata.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<BasketController> _logger;

        public ProductController(
            ILogger<BasketController> logger)
        {
            _logger = logger;
        }

        [HttpGet("/products/get")]
        public IActionResult GetProducts()
        {
            return Ok(Products.Items);
        }
    }
}
