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

     
    }
    
}
