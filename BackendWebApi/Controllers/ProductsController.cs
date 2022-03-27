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
        //listado de productos
        [HttpGet]
        public JsonResult GetProducts()
        {
            var products = new List<Product>();
            products = _productService.getProducts().ToList();
            
            if(products == null)
            {
                return Json(new Response()
                {
                    Name = "Incorrecto",
                    Mensaje = "Datos vacios"
                });
            }

            return Json(products);
        }

            
        //lista producto detalle
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
            product = _productService.getProduct(pid);
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
        //inserta producto
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
        //actualiza producto
        [Route("{pid}")]
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
        //elimina producto
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
                    Mensaje = "datos vacios"
                });

            }

            Response = _productService.deleteProduct(pid);

            if (Response.Mensaje!= null && Response.Mensaje.Equals("Correcto"))
            {
                return Json(new Response()
                {
                    Name = Response.Mensaje,
                    Mensaje = "Registro eliminado correctamente"
                });
            }
            return Json(new Response()
            {
                Name = "Incorrecto",
                Mensaje = "Registro no eliminado"
            });

        }
    }
}
