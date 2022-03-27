using BackendWebApi.Models;

namespace BackendWebApi.Interfaces
{
    public interface IProductService
    {
        public List<Product> getProducts();
        public Product getProduct(int Id);
        public Response insertProduct(Product prod);
        public Response updateProduct(Product prod);
        public Response deleteProduct(int id);


    }
}
