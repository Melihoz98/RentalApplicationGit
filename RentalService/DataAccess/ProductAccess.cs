using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using RentalService.Models;
using System;
using System.Collections.Generic;

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
                string queryString = "SELECT productID, productName, description, hourlyPrice, categoryID, imagePath FROM Products";

                using (SqlConnection con = new SqlConnection(_connectionString))
                using (SqlCommand readCommand = new SqlCommand(queryString, con))
                {
                    con.Open();

                    SqlDataReader productReader = readCommand.ExecuteReader();

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
            Product foundProduct = null;

            try
            {
                string queryString = "SELECT productID, productName, description, hourlyPrice, categoryID, imagePath FROM Products WHERE productID = @Id";

                using (SqlConnection con = new SqlConnection(_connectionString))
                using (SqlCommand readCommand = new SqlCommand(queryString, con))
                {
                    SqlParameter idParam = new SqlParameter("@Id", findId);
                    readCommand.Parameters.Add(idParam);

                    con.Open();

                    SqlDataReader productReader = readCommand.ExecuteReader();

                    if (productReader.Read())
                    {
                        foundProduct = GetProductFromReader(productReader);
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
                string insertString = "INSERT INTO Products (productName, description, hourlyPrice, categoryID, imagePath) OUTPUT INSERTED.productID VALUES (@ProductName, @Description, @HourlyPrice, @CategoryID, @ImagePath)";

                using (SqlConnection con = new SqlConnection(_connectionString))
                using (SqlCommand createCommand = new SqlCommand(insertString, con))
                {
                    createCommand.Parameters.AddWithValue("@ProductName", product.ProductName);
                    createCommand.Parameters.AddWithValue("@Description", product.Description);
                    createCommand.Parameters.AddWithValue("@HourlyPrice", product.HourlyPrice);
                    createCommand.Parameters.AddWithValue("@CategoryID", product.CategoryID ?? (object)DBNull.Value);
                    createCommand.Parameters.AddWithValue("@ImagePath", product.ImagePath);

                    con.Open();

                    insertedId = (int)createCommand.ExecuteScalar();
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
                using (SqlCommand command = con.CreateCommand())
                {
                    command.CommandText = "UPDATE Products SET productName = @ProductName, description = @Description, hourlyPrice = @HourlyPrice, categoryID = @CategoryID, imagePath = @ImagePath WHERE productID = @ProductId";
                    command.Parameters.AddWithValue("@ProductName", product.ProductName);
                    command.Parameters.AddWithValue("@Description", product.Description);
                    command.Parameters.AddWithValue("@HourlyPrice", product.HourlyPrice);
                    command.Parameters.AddWithValue("@CategoryID", product.CategoryID ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@ImagePath", product.ImagePath);
                    command.Parameters.AddWithValue("@ProductId", product.ProductID);

                    con.Open();
                    command.ExecuteNonQuery();
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
                using (SqlCommand command = con.CreateCommand())
                {
                    command.CommandText = "DELETE FROM Products WHERE productID = @ProductId";
                    command.Parameters.AddWithValue("@ProductId", id);

                    con.Open();
                    command.ExecuteNonQuery();
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
            int productId = productReader.GetInt32(productReader.GetOrdinal("productID"));
            string productName = productReader.GetString(productReader.GetOrdinal("productName"));
            string description = productReader.IsDBNull(productReader.GetOrdinal("description")) ? null : productReader.GetString(productReader.GetOrdinal("description"));
            decimal hourlyPrice = productReader.GetDecimal(productReader.GetOrdinal("hourlyPrice"));
            int? categoryID = productReader.IsDBNull(productReader.GetOrdinal("categoryID")) ? null : (int?)productReader.GetInt32(productReader.GetOrdinal("categoryID"));
            string imagePath = productReader.IsDBNull(productReader.GetOrdinal("imagePath")) ? null : productReader.GetString(productReader.GetOrdinal("imagePath"));

            return new Product(productId, productName, description, hourlyPrice, categoryID, imagePath);
        }
    }
}
