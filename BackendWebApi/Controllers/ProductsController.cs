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
        //Get
        [HttpGet]
        public List<Product> GetProducts()
        {
            var products = new List<Product>();
            products = _productService.getProducts().ToList();
            return products;
        }
        //Insert
        [HttpPost]
        public JsonResult insertProducts(Product product)
        {
            var Response = new Response();
            //if (ModelState.IsValid)
            //{
            //    return 
            //}

            if (product.Name.Equals(""))
            {
                return Json(new Response()
                {
                    Name = "Incorrecto",
                    Mensaje = "datos vacios"
                });

            }
            
            Response = _productService.insertProduct( product);

            if (Response.Mensaje != null && Response.Name != null)
            {
                return Json(new Response()
                {
                    Name =Response.Mensaje,
                    Mensaje = "Registro "+ Response .Name+ " guardado correctamente"
                });
            }
            return Json(new Response()
            {
                Name = "Incorrecto",
                Mensaje = "Registro no guardado"
            });

        }

        [HttpPut]
        public JsonResult updatetProducts(Product product)
        {
            var Response = new Response();

            if (product.Id.Equals("") && product.Name.Equals("") && product.Price.Equals("") )
            {
                return Json(new Response()
                {
                    Name = "Incorrecto",
                    Mensaje = "datos vacios"
                });

            }

            Response = _productService.updateProduct(product);

            if (Response.Mensaje != null && Response.Name != null)
            {
                return Json(new Response()
                {
                    Name = Response.Mensaje,
                    Mensaje = "Registro " + Response.Name + " actualizado correctamente"
                });
            }
            return Json(new Response()
            {
                Name = "Incorrecto",
                Mensaje = "Registro no actualizado"
            });

        }
    }
}
