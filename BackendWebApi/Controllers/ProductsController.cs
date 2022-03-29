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
        private readonly IProductService _productInterface;

        public ProductsController(IConfiguration configuration, IProductService productService)
        {
            _configuration = configuration;
            _productInterface = productService;
        }
        ///listado de productos
        [HttpGet]
        public JsonResult GetProducts()
        {
            var products = new List<Product>();
            products = _productInterface.getProducts().ToList();

            if (products == null)
            {
                return Json(new Response()
                {
                    Name = "Incorrecto",
                    Mensaje = "Datos vacios"
                });
            }

            return Json(products);
        }

            
        ///lista producto detalle
        [Route("{pid}")]
        [HttpGet]
        public JsonResult GetProduct(int pid)
        {
            var product = new Product();

            if (pid.Equals(""))
            {
                return Json(new Response()
                {
                    Name = "Incorrecto",
                    Mensaje = "Id vacio"
                });

            }
            product = _productInterface.getProduct(pid);
            if (product == null)
            {
                return Json(new Response()
                {
                    Name = "Incorrecto",
                    Mensaje = "Datos vacios"
                });
            }

            return Json(product);
        }
        ///inserta producto
        [HttpPost]
        public JsonResult insertProducts(Product product)
        {
            var Response = new Response();

            if (product.Name.Equals(""))
            {
                return Json(new Response()
                {
                    Name = "Incorrecto",
                    Mensaje = "Datos vacios"
                });

            }
            
            Response = _productInterface.insertProduct( product);

            return Json(Response);
            

        }
        ///actualiza producto
        [Route("{pid}")]
        [HttpPut]
        public JsonResult updatetProducts(Product product)
        {
            var Response = new Response();

            if (product.Id == null || product.Name.Equals("") )
            {
                return Json(new Response()
                {
                    Name = "Incorrecto",
                    Mensaje = "Datos vacios"
                });

            }

            Response = _productInterface.updateProduct(product);

            return Json(Response);

        }
        ///elimina producto
        [Route("{pid}")]
        [HttpDelete]
        public JsonResult deleteProducts(int pid)
        {
            var Response = new Response();

            if (pid.Equals(""))
            {
                return Json(new Response()
                {
                    Name = "Incorrecto",
                    Mensaje = "Datos vacios"
                });

            }

            Response = _productInterface.deleteProduct(pid);

            return Json(Response);

        }
    }
}
