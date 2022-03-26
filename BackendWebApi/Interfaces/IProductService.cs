using BackendWebApi.Models;

namespace BackendWebApi.Interfaces
{
    public interface IProductService
    {
        public List<Product> getProducts();
    }
}
