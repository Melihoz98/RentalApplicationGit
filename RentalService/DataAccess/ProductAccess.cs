using Microsoft.Data.SqlClient;
using RentalService.Models;


namespace RentalService.DataAccess
{
    public class ProductAccess : IProductAccess
    {
        private readonly string _connectionString;

        public ProductAccess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("RentalConnection");
            if (string.IsNullOrEmpty(_connectionString))
            {
                throw new InvalidOperationException("Database connection string is not configured.");
            }
        }

        public List<Product> GetProductAll()
        {
            List<Product> foundProducts = new List<Product>();

            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    string queryString = "SELECT productID, productName, description, hourlyPrice, inventory, categoryID FROM Products";
                    using (SqlCommand command = new SqlCommand(queryString, con))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Product product = GetProductFromReader(reader);
                                foundProducts.Add(product);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception (log, return error response, etc.)
                Console.WriteLine($"Error retrieving products: {ex.Message}");
                throw;
            }

            return foundProducts;
        }

        public Product GetProductById(int id)
        {
            Product foundProduct = null;

            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    string queryString = "SELECT productID, productName, description, hourlyPrice, inventory, categoryID FROM Products WHERE productID = @Id";
                    using (SqlCommand command = new SqlCommand(queryString, con))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                foundProduct = GetProductFromReader(reader);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception (log, return error response, etc.)
                Console.WriteLine($"Error retrieving product by ID: {ex.Message}");
                throw;
            }

            return foundProduct;
        }

        public void AddProduct(Product product)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    string queryString = "INSERT INTO Products (productName, description, hourlyPrice, inventory, categoryID) VALUES (@ProductName, @Description, @HourlyPrice, @Inventory, @CategoryID)";
                    using (SqlCommand command = new SqlCommand(queryString, con))
                    {
                        command.Parameters.AddWithValue("@ProductName", product.ProductName);
                        command.Parameters.AddWithValue("@Description", (object)product.Description ?? DBNull.Value);
                        command.Parameters.AddWithValue("@HourlyPrice", product.HourlyPrice);
                        command.Parameters.AddWithValue("@Inventory", product.Inventory);
                        command.Parameters.AddWithValue("@CategoryID", (object)product.CategoryID ?? DBNull.Value);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine($"Error adding product: {ex.Message}");
                throw;
            }
        }

        public void UpdateProduct(Product product)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    string queryString = "UPDATE Products SET productName = @ProductName, description = @Description, hourlyPrice = @HourlyPrice, inventory = @Inventory, categoryID = @CategoryID WHERE productID = @ProductId";
                    using (SqlCommand command = new SqlCommand(queryString, con))
                    {
                        command.Parameters.AddWithValue("@ProductName", product.ProductName);
                        command.Parameters.AddWithValue("@Description", (object)product.Description ?? DBNull.Value);
                        command.Parameters.AddWithValue("@HourlyPrice", product.HourlyPrice);
                        command.Parameters.AddWithValue("@Inventory", product.Inventory);
                        command.Parameters.AddWithValue("@CategoryID", (object)product.CategoryID ?? DBNull.Value);
                        command.Parameters.AddWithValue("@ProductId", product.ProductID);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine($"Error updating product: {ex.Message}");
                throw;
            }
        }

        public void DeleteProduct(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    string queryString = "DELETE FROM Products WHERE productID = @ProductId";
                    using (SqlCommand command = new SqlCommand(queryString, con))
                    {
                        command.Parameters.AddWithValue("@ProductId", id);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine($"Error deleting product: {ex.Message}");
                throw;
            }
        }


        private Product GetProductFromReader(SqlDataReader reader)
        {
            int productId = reader.GetInt32(reader.GetOrdinal("productID"));
            string productName = reader.GetString(reader.GetOrdinal("productName"));
            string description = reader.IsDBNull(reader.GetOrdinal("description")) ? null : reader.GetString(reader.GetOrdinal("description"));
            decimal hourlyPrice = reader.GetDecimal(reader.GetOrdinal("hourlyPrice"));
            int inventory = reader.GetInt32(reader.GetOrdinal("inventory"));
            int? categoryID = reader.IsDBNull(reader.GetOrdinal("categoryID")) ? null : (int?)reader.GetInt32(reader.GetOrdinal("categoryID"));

            return new Product(productId, productName, description, hourlyPrice, inventory, categoryID);
        }
    }
}
