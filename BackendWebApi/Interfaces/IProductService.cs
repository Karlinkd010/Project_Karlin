using BackendWebApi.Models;

namespace BackendWebApi.Interfaces
{
    public interface IProductService
    {
        public List<Product> getProducts();
        public Response insertProduct(Product prod);
        public Response updateProduct(int id);
    }
}
