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

        public Product getProduct(int pid)
        {
            var products = new Product();
            try
            {
                using (IDbConnection db = Connection)
                {
                    db.Open();
                    products = db.Query<Product>("sp_getProduct", new { idProduct = pid }, commandType: CommandType.StoredProcedure).FirstOrDefault();
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
            var response = new Product();
            try
            {
                using (IDbConnection db = Connection)
                {
                    db.Open();
                    response = db.Query<Product>("sp_insertProduct", new { Name = prod.Name, Price = prod.Price }, commandType: CommandType.StoredProcedure).FirstOrDefault();

                    if (response == null)
                    {
                        return result = new Response()
                        {
                            Name = "Incorrecto",
                            Mensaje = "Registro no se pudo guardar correctamente"
                        };

                    }
                    if (response.Id == 0)
                    {
                        return result = new Response()
                        {
                            Name = "Error al guardar",
                            Mensaje = "Registro " + prod.Name + " ya existe en el base de datos!"
                        };
                    }
                    if (response.Name !=null && response.Creation != null)
                    {
                        return result = new Response()
                        {
                            Name = "Correcto",
                            Mensaje = "Registro " + response.Name + " guardado correctamente" 
                        };
                    }
                    

                    db.Close();

                    return result;
                }
            }
            catch (Exception ex)
            {
                
                return new Response()
                {                
                    Name = "Error",
                    Mensaje = ex.Message  
                };
            }
        }

        public Response updateProduct(Product prod)
        {
            var result = new Response();
            var response = new Product();
            try
            {
                using (IDbConnection db = Connection)
                {
                    db.Open();
                    response = db.Query<Product>("sp_updateProduct", new { Id=prod.Id, Name= prod.Name, Price= prod.Price }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                   
                    if (response == null)
                    {
                        return result = new Response()
                        {
                            Name = "Incorrecto",
                            Mensaje = "Registro no se pudo editar correctamente"
                        };

                    }
                    if (response.Id == 0)
                    {
                        return result = new Response()
                        {
                            Name = "Error al editar",
                            Mensaje = "No se pudo encontrar un registro con el ID: " + prod.Id
                        };
                    }
                    if (response.Name != null && response.Modification != null)
                    {
                        return result = new Response()
                        {
                            Name = "Correcto",
                            Mensaje = "Registro " + response.Name + " actualizado correctamente"
                        };
                    }
                    db.Close();

                    return result;
                }
            }
            catch (Exception ex)
            {
                return new Response()
                {
                    Name = "Error",
                    Mensaje = ex.Message
                };
            }
        }

        public Response deleteProduct(int id)
        {
            var result = new Response();
            try
            {
                using (IDbConnection db = Connection)
                {
                    db.Open();
                    result = db.Query<Response>("sp_deleteProduct", new { Id = id}, commandType: CommandType.StoredProcedure).FirstOrDefault();


                    if (result.Mensaje.Equals("1"))
                    {
                        return result = new Response()
                        {
                            Name = "Correcto",
                            Mensaje = "Registro eliminado correctamente"
                        };
                    }
                    if (result.Mensaje.Equals("0"))
                    {
                        return result = new Response()
                        {
                            Name = "Incorrecto",
                            Mensaje = "No se pudo encontrar un registro con el ID: " + id
                        };

                    }
                    db.Close();

                    return result;
                }
            }
            catch (Exception ex)
            {
                return new Response()
                {
                    Name = "Error",
                    Mensaje = ex.Message
                };
            }
        }


    }
    
}
