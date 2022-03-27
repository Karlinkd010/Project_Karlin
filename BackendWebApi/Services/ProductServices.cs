using BackendWebApi.Interfaces;
using BackendWebApi.Models;
using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace BackendWebApi.Services
{
    public class ProductServices : IProductService
    {
        private readonly IConfiguration _configuration;
        public string ConnectionString { get; set; }
        public string ProviderName { get; set; }

        public ProductServices(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionString = _configuration.GetConnectionString("AppConnection");
            ProviderName = "System.Data.SqlClient";
        }

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(ConnectionString);
            }

        }

        //listado de productos
        public List<Product> getProducts()
        {
            List < Product > products = new List< Product >();
            try
            {
                using (IDbConnection db = Connection)
                {
                    db.Open();
                    products = db.Query<Product>("sp_getProducts", commandType: CommandType.StoredProcedure).ToList();
                    db.Close();

                    return products;
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return products;
            }  
        }

        public Response insertProduct( Product prod)
        {
            var result = new Response ();
            try
            {
                using (IDbConnection db = Connection)
                {
                    db.Open();
                    var response= db.Query<Product>("sp_insertProduct", new {Name= prod.Name, Price =prod.Price }, commandType: CommandType.StoredProcedure).ToList();
                    if (response.FirstOrDefault().Id !=null && response.FirstOrDefault().Name != null)
                    {
                        result = new Response()
                        {
                            Name = response.FirstOrDefault().Name,
                            Mensaje = "Correcto"
                        };
                    }
                    db.Close();

                    return result;
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return result;
            }
        }

        public Response updateProduct(Product prod)
        {
            var result = new Response();
            try
            {
                using (IDbConnection db = Connection)
                {
                    db.Open();
                    var response = db.Query<Product>("sp_updateProduct", new { Id=prod.Id, Name= prod.Name, Price= prod.Price }, commandType: CommandType.StoredProcedure).ToList();
                    if (response.FirstOrDefault().Id != null && response.FirstOrDefault().Name != null)
                    {
                        result = new Response()
                        {
                            Name = response.FirstOrDefault().Name,
                            Mensaje = "Correcto"
                        };
                    }
                    db.Close();

                    return result;
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return result;
            }
        }


    }
    
}
