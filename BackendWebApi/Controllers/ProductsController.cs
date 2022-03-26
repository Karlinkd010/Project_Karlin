using BackendWebApi.Interfaces;
using BackendWebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace BackendWebApi.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class ProductsController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IProductService _productService;

        public ProductsController(IConfiguration configuration, IProductService productService)
        {
            _configuration = configuration;
            _productService = productService;   
        }

        [HttpGet]
        public List<Product> GetProducts()
        {
            var products = new List<Product>();
            products = _productService.getProducts().ToList();
            return products;
        }
        [HttpPut]
        public IActionResult insertProducts(Product product)
        {
            string result = string.Empty;
            result = _productService.insertProduct( product);
            return Ok(result);
        }
    }
}
