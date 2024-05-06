using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using RentalService.Models;
using System;
using System.Collections.Generic;

namespace RentalService.DataAccess
{
    public class ProductCopyAccess : IProductCopyAccess
    {
        private readonly string _connectionString;

        public ProductCopyAccess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("RentalConnection");
            if (string.IsNullOrEmpty(_connectionString))
            {
                throw new InvalidOperationException("Database connection string is not configured.");
            }
        }

        public List<ProductCopy> GetProductCopyAll()
        {
            List<ProductCopy> foundProductCopies = new List<ProductCopy>();

            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    string queryString = "SELECT productID, serialNumber FROM ProductCopies";
                    using (SqlCommand command = new SqlCommand(queryString, con))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ProductCopy productCopy = GetProductCopyFromReader(reader);
                                foundProductCopies.Add(productCopy);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception (log, return error response, etc.)
                Console.WriteLine($"Error retrieving product copies: {ex.Message}");
                throw;
            }

            return foundProductCopies;
        }

        public ProductCopy GetProductCopyBySerialNumber(string serialNumber)
        {
            ProductCopy foundProductCopy = null;

            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    string queryString = "SELECT productID, serialNumber FROM ProductCopies WHERE serialNumber = @SerialNumber";
                    using (SqlCommand command = new SqlCommand(queryString, con))
                    {
                        command.Parameters.AddWithValue("@SerialNumber", serialNumber);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                foundProductCopy = GetProductCopyFromReader(reader);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception (log, return error response, etc.)
                Console.WriteLine($"Error retrieving product copy by serial number: {ex.Message}");
                throw;
            }

            return foundProductCopy;
        }

        public List<ProductCopy> GetAllProductCopiesByProductID(int productID)
        {
            List<ProductCopy> foundProductCopies = new List<ProductCopy>();

            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    string queryString = "SELECT productID, serialNumber FROM ProductCopies WHERE productID = @productID AND rented = 1";
                    using (SqlCommand command = new SqlCommand(queryString, con))
                    {
                        command.Parameters.AddWithValue("@productID", productID);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ProductCopy productCopy = GetProductCopyFromReader(reader);
                                foundProductCopies.Add(productCopy);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception (log, return error response, etc.)
                Console.WriteLine($"Error retrieving product copies by product ID: {ex.Message}");
                throw;
            }

            return foundProductCopies;
        }



        public void AddProductCopy(ProductCopy productCopy)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    string queryString = "INSERT INTO ProductCopies (productID, serialNumber) VALUES (@ProductId, @SerialNumber)";
                    using (SqlCommand command = new SqlCommand(queryString, con))
                    {
                        command.Parameters.AddWithValue("@ProductId", productCopy.ProductID);
                        command.Parameters.AddWithValue("@SerialNumber", productCopy.SerialNumber);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine($"Error adding product copy: {ex.Message}");
                throw;
            }
        }

        public void UpdateProductCopy(ProductCopy productCopy)
        {
            // Implement update logic if needed
            throw new NotImplementedException();
        }

        public void DeleteProductCopy(string serialNumber)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    string queryString = "DELETE FROM ProductCopies WHERE serialNumber = @SerialNumber";
                    using (SqlCommand command = new SqlCommand(queryString, con))
                    {
                        command.Parameters.AddWithValue("@SerialNumber", serialNumber);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine($"Error deleting product copy: {ex.Message}");
                throw;
            }
        }

        private ProductCopy GetProductCopyFromReader(SqlDataReader reader)
        {
            int productID = reader.GetInt32(reader.GetOrdinal("productID"));
            string serialNumber = reader.GetString(reader.GetOrdinal("serialNumber"));
            return new ProductCopy(productID, serialNumber);
        }
    }
}
