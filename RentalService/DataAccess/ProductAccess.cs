using Microsoft.Data.SqlClient;
using RentalService.DTO;
using RentalService.Models;
using System;


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
            List<Product> foundProducts;

            try
            {
                string queryString = "SELECT productID, productName, description, hourlyPrice, inventory, categoryID FROM Products";

                using (SqlConnection con = new SqlConnection(_connectionString))
                using (SqlCommand readCommand = new SqlCommand(queryString, con))
                {
                    con.Open();

                    SqlDataReader productReader = readCommand.ExecuteReader();

                    foundProducts = new List<Product>();

                    while (productReader.Read())
                    {
                        Product product = GetProductFromReader(productReader);
                        foundProducts.Add(product);
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


        public Product GetProductById(int findId)
        {
            Product foundProduct;
            try
            {
                string queryString = "SELECT productID, productName, description, hourlyPrice, inventory, categoryID FROM Products WHERE productID = @Id";

                using (SqlConnection con = new SqlConnection(_connectionString))
                using (SqlCommand readCommand = new SqlCommand(queryString, con))
                {
                    SqlParameter idParam = new SqlParameter("@Id", findId);
                    readCommand.Parameters.Add(idParam);

                    con.Open();

                    SqlDataReader personReader = readCommand.ExecuteReader();
                    foundProduct = new Product();
                    while (personReader.Read())
                    {
                        foundProduct = GetProductFromReader(personReader);
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



        public int AddProduct(Product product)
        {
            int insertedId = -1;

            try
            {
                string insertString = "INSERT INTO Products (productName, description, hourlyPrice, inventory, categoryID) OUTPUT INSERTED.productID VALUES (@ProductName, @Description, @HourlyPrice, @Inventory, @CategoryID)";

                using (SqlConnection con = new SqlConnection(_connectionString))
                using (SqlCommand CreateCommand = new SqlCommand(insertString, con))
                {                    
                    // Prepare SQL
                    SqlParameter productName = new("@ProductName", product.ProductName);
                    CreateCommand.Parameters.Add(productName);
                    SqlParameter productDescription = new("@Description", product.Description);
                    CreateCommand.Parameters.Add(productDescription);
                    SqlParameter productHourlyPrice = new("@HourlyPrice", product.HourlyPrice);
                    CreateCommand.Parameters.Add(productHourlyPrice);
                    SqlParameter productInventory = new("@Inventory", product.Inventory);
                    CreateCommand.Parameters.Add(productInventory);
                    SqlParameter productCategoryID = new("@CategoryID", product.CategoryID);
                    CreateCommand.Parameters.Add(productCategoryID);


                    con.Open();
                    // Execute save and read generated key (ID)
                    insertedId = (int)CreateCommand.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine($"Error adding product: {ex.Message}");
                throw;
            }

            return insertedId;
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
                        command.Parameters.AddWithValue("@Description", product.Description);
                        command.Parameters.AddWithValue("@HourlyPrice", product.HourlyPrice);
                        command.Parameters.AddWithValue("@Inventory", product.Inventory);
                        command.Parameters.AddWithValue("@CategoryID", product.CategoryID);
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


        private Product GetProductFromReader(SqlDataReader productReader)
        {
            Product foundProduct;
            int productId = productReader.GetInt32(productReader.GetOrdinal("productID"));
            string productName = productReader.GetString(productReader.GetOrdinal("productName"));
            string description = productReader.IsDBNull(productReader.GetOrdinal("description")) ? null : productReader.GetString(productReader.GetOrdinal("description"));
            decimal hourlyPrice = productReader.GetDecimal(productReader.GetOrdinal("hourlyPrice"));
            int inventory = productReader.GetInt32(productReader.GetOrdinal("inventory"));
            int? categoryID = productReader.IsDBNull(productReader.GetOrdinal("categoryID")) ? null : (int?)productReader.GetInt32(productReader.GetOrdinal("categoryID"));

            foundProduct = new Product(productId, productName, description, hourlyPrice, inventory, categoryID);

            return foundProduct;

        }
  

    }
}
