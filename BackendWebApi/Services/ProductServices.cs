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

        public string insertProduct( Product prod)
        {
          string result = String.Empty;
            try
            {
                using (IDbConnection db = Connection)
                {
                    db.Open();
                    var product= db.Query<Product>("sp_insertProduct", new {Name= prod.Name, Price =prod.Price }, commandType: CommandType.StoredProcedure);
                    if(product != null)
                    {
                        result = "1";
                    }
                    db.Close();

                    return result;
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return error;
            }
        }


    }
    
}
